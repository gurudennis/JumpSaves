using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JSL
{
    public class Backup
    {
        internal Backup(string path)
        {
            Path = path;
            Load();
        }

        internal Backup(string path, string originalPath)
        {
            Path = path;
            Save(originalPath);
        }

        public string Path { get; private set; }

        public string OriginalPath
        {
            get
            {
                return metadata_?.OriginalPath;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return metadata_?.Timestamp ?? DateTime.MinValue;
            }
        }

        public void Restore(string originalPath = null)
        {
            if (originalPath == null)
            {
                originalPath = OriginalPath;
            }

            if (Directory.Exists(originalPath))
            {
                SaveDir dir = new SaveDir(originalPath);
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
            public string OriginalPath { get; set; }

            public DateTime Timestamp { get; set; }
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

        private void Save(string originalPath)
        {
            Directory.CreateDirectory(Path);

            string originalFilePath = originalPath;
            if (Directory.Exists(originalPath))
            {
                SaveDir dir = new SaveDir(originalPath);
                originalFilePath = dir.SaveFilePath;
            }

            File.Copy(originalFilePath, SaveFilePath);
            File.WriteAllText(MetadataFilePath, JsonSerializer.Serialize(metadata_));
        }

        private static readonly string MetadataFileName = "metadata.json";
        private static readonly string SaveFileName = "save.bin";
        private Metadata metadata_;
    }

    public class BackupStore
    {
        public BackupStore(string path)
        {
            Path = path;
            Load();
        }

        public string Path { get; private set; }

        public EventHandler<EventArgs> Changed;

        public IReadOnlyCollection<Backup> Backups
        {
            get
            {
                return backups_;
            }
        }

        public void Reload()
        {
            backups_.Clear();
            failedPaths_ = new List<string>();
            Load();
        }

        public Backup Add(string originalPath)
        {
            string fileName = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_Backup";
            Backup backup = new Backup(System.IO.Path.Combine(Path, fileName), originalPath);
            backups_.Add(backup);
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

        public IReadOnlyCollection<string> TakeFailedPaths()
        {
            IReadOnlyCollection<string> paths = failedPaths_;
            failedPaths_ = new List<string>();
            return paths;
        }

        private void Load()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Path);
                foreach (DirectoryInfo backupDir in dir.EnumerateDirectories())
                {
                    try
                    {
                        backups_.Add(new Backup(backupDir.FullName));
                    }
                    catch
                    {
                        failedPaths_.Add(backupDir.FullName);
                    }
                }
            }
            catch
            {
                failedPaths_.Add(Path);
            }

            Changed?.Invoke(this, EventArgs.Empty);
        }

        private List<Backup> backups_ = new List<Backup>();
        private List<string> failedPaths_ = new List<string>();
    }
}
