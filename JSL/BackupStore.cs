using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JSL
{
    public class Backup
    {
        internal Backup(BackupStore store, string path)
        {
            store_ = store;
            Path = path;
            Load();
        }

        internal Backup(BackupStore store, string path, string originalPath, bool experimental, string name)
        {
            store_ = store;
            Path = path;
            Save(originalPath, experimental, name);
        }

        public static string MakeDirName(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Backup";
            }

            return $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")} {name}";
        }

        public string Name
        {
            get
            {
                return metadata_.Name ?? string.Empty;
            }
            set
            {
                if (value != metadata_.Name)
                {
                    metadata_.Name = value;
                    File.WriteAllText(MetadataFilePath, JsonSerializer.Serialize(metadata_));

                    string newPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), MakeDirName(metadata_.Name));
                    try
                    {
                        if (Path != newPath)
                        {
                            Directory.Move(Path, newPath);
                            Path = newPath;
                        }
                    }
                    catch { }

                    store_.Changed?.Invoke(store_, EventArgs.Empty);
                }
            }
        }

        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(Name) ? "(unnamed)" : Name;
            }
            set
            {
                if (value == "(unnamed)")
                {
                    value = null;
                }

                Name = value;
            }
        }

        public string Path { get; private set; }

        public string OriginalPath
        {
            get
            {
                return metadata_.OriginalPath;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return metadata_.Timestamp;
            }
        }

        public bool IsExperimental
        {
            get
            {
                return metadata_.IsExperimental;
            }
        }

        public void Restore(string originalPath = null, bool? experimental = null)
        {
            if (originalPath == null)
            {
                originalPath = OriginalPath;
            }

            if (Directory.Exists(originalPath))
            {
                SaveDir dir = new SaveDir(originalPath, experimental == null ? metadata_.IsExperimental : experimental.Value);
                dir.Save(SaveFilePath);
            }
            else
            {
                if (File.Exists(originalPath))
                {
                    File.Delete(originalPath);
                    File.Copy(SaveFilePath, originalPath);
                }
            }
        }

        private class Metadata
        {
            public string Name { get; set; }

            public string OriginalPath { get; set; }

            public DateTime Timestamp { get; set; }

            public bool IsExperimental { get; set; }
        }

        private string MetadataFilePath
        {
            get
            {
                return System.IO.Path.Combine(Path, MetadataFileName);
            }
        }

        private string SaveFilePath
        {
            get
            {
                return System.IO.Path.Combine(Path, SaveFileName);
            }
        }

        private void Load()
        {
            metadata_ = JsonSerializer.Deserialize<Metadata>(File.ReadAllText(MetadataFilePath));
            if (!File.Exists(SaveFilePath))
            {
                throw new Exception($"{SaveFilePath} not found");
            }
        }

        private void Save(string originalPath, bool experimental, string name)
        {
            metadata_ = new Metadata();
            metadata_.Name = name;
            metadata_.Timestamp = DateTime.Now;
            metadata_.IsExperimental = experimental;
            metadata_.OriginalPath = originalPath;

            Directory.CreateDirectory(Path);

            string originalFilePath = originalPath;
            if (Directory.Exists(originalPath))
            {
                SaveDir dir = new SaveDir(originalPath, experimental);
                originalFilePath = dir.SaveFilePath;
            }

            File.Copy(originalFilePath, SaveFilePath);
            File.WriteAllText(MetadataFilePath, JsonSerializer.Serialize(metadata_));
        }

        private static readonly string MetadataFileName = "metadata.json";
        private static readonly string SaveFileName = "save.bin";
        private readonly BackupStore store_;
        private Metadata metadata_;
    }

    public class BackupStore
    {
        public BackupStore(string path, int maxBackups)
        {
            Path = path;
            MaxBackups = maxBackups;
            Load();
        }

        public string Path { get; private set; }

        public EventHandler<EventArgs> Changed;

        public IReadOnlyList<Backup> Backups
        {
            get
            {
                return backups_;
            }
        }

        public int MaxBackups { get; private set; }

        public void Reload()
        {
            backups_.Clear();
            failedPaths_ = new List<string>();
            Load();
        }

        public Backup Add(string originalPath, bool experimental, string name = null)
        {
            Backup backup = new Backup(this, System.IO.Path.Combine(Path, Backup.MakeDirName()), originalPath, experimental, name);
            backups_.Insert(0, backup);
            Prune();
            Changed?.Invoke(this, EventArgs.Empty);
            return backup;
        }

        public void Remove(int index)
        {
            string backupPath = backups_[index].Path;
            backups_.RemoveAt(index);
            Directory.Delete(backupPath, true);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void Remove(Backup backup)
        {
            Remove(backups_.IndexOf(backup));
        }

        public IReadOnlyList<string> TakeFailedPaths()
        {
            IReadOnlyList<string> paths = failedPaths_;
            failedPaths_ = new List<string>();
            return paths;
        }

        private void Load()
        {
            Directory.CreateDirectory(Path);

            try
            {
                DirectoryInfo dir = new DirectoryInfo(Path);
                foreach (DirectoryInfo backupDir in dir.EnumerateDirectories())
                {
                    try
                    {
                        backups_.Add(new Backup(this, backupDir.FullName));
                    }
                    catch
                    {
                        failedPaths_.Add(backupDir.FullName);
                    }
                }

                backups_.Sort((l, r) => l.Timestamp.CompareTo(r.Timestamp) * -1);

                Prune();
            }
            catch
            {
                failedPaths_.Add(Path);
            }
        }

        private void Prune()
        {
            if (MaxBackups <= 0)
            {
                return;
            }

            int toRemove = backups_.Count - MaxBackups;
            if (toRemove <= 0)
            {
                return;
            }

            for (int i = 0; i < toRemove; ++i)
            {
                Remove(backups_.Count - 1);
            }
        }

        private List<Backup> backups_ = new List<Backup>();
        private List<string> failedPaths_ = new List<string>();
    }
}
