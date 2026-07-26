using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JSL
{
    public class SaveDir
    {
        public static SaveDir Default // a bit of a heuristic, obviously
        {
            get
            {
                foreach (string drive in new List<string> { "C:", "D:", "E:" })
                {
                    string userRoot = $"{drive}\\Program Files (x86)\\Steam\\userdata";
                    if (!Directory.Exists(userRoot))
                    {
                        continue;
                    }

                    DirectoryInfo userRootDir = new DirectoryInfo(userRoot);
                    foreach (DirectoryInfo userDir in userRootDir.GetDirectories())
                    {
                        string saveRoot = System.IO.Path.Combine(userDir.FullName, "1757300", "remote");
                        if (Directory.Exists(saveRoot))
                        {
                            return new SaveDir(saveRoot);
                        }
                    }
                }

                return null;
            }
        }

        public SaveDir(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Directory not found: {path}");
            }

            Path = path;
        }

        public string SaveFilePath
        {
            get
            {
                DirectoryInfo directory = new DirectoryInfo(Path);
                FileInfo newestSaveFile = directory.GetFiles(FileMask).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (!FileNames.Contains(newestSaveFile.Name))
                {
                    return null;
                }

                return newestSaveFile != null ? newestSaveFile.FullName : null;
            }
        }

        public SaveFile SaveFile
        {
            get
            {
                string path = SaveFilePath;
                return string.IsNullOrEmpty(path) ? null : new SaveFile(path);
            }
        }

        public void Save(SaveFile file)
        {
            file.Save();

            foreach (string name in FileNames)
            {
                string saveName = System.IO.Path.GetFileName(file.Path);
                if (string.Equals(saveName, name, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string destinationPath = System.IO.Path.Combine(Path, name);
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                File.Copy(file.Path, destinationPath);
            }
        }

        public void Save(string file)
        {
            foreach (string name in FileNames)
            {
                string destinationPath = System.IO.Path.Combine(Path, name);
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                File.Copy(file, destinationPath);
            }
        }

        public string Path { get; private set; }

        public static readonly List<string> FileNames = new List<string> { "persistent_user_data.bin", "persistent_user_data.bin.bak1", "persistent_user_data.bin.bak2" };

        private const string FileMask = "persistent_user_data.bin*";
    }
}
