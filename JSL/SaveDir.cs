using Microsoft.Win32;
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
                string steamDir = GetSteamDirectory();
                if (string.IsNullOrEmpty(steamDir))
                {
                    return null;
                }

                string userRoot = System.IO.Path.Combine(steamDir, "userdata");
                if (!Directory.Exists(userRoot))
                {
                    return null;
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

            if (string.IsNullOrEmpty(SaveFilePath))
            {
                throw new FileNotFoundException($"No save files found in directory: {path}");
            }
        }

        public string SaveFilePath
        {
            get
            {
                DirectoryInfo directory = new DirectoryInfo(Path);
                FileInfo newestSaveFile = directory.GetFiles(FileMask).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (newestSaveFile == null || !FileNames.Contains(newestSaveFile.Name))
                {
                    return null;
                }

                return newestSaveFile.FullName;
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

        private static string GetSteamDirectory()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                if (key != null)
                {
                    string val = key.GetValue("SteamPath") as string;
                    if (!string.IsNullOrEmpty(val))
                    {
                        val = val.Replace('/', System.IO.Path.DirectorySeparatorChar);
                        if (Directory.Exists(val))
                        {
                            return val;
                        }
                    }
                }
            }
            catch { }

            string[] hklmKeys = { @"SOFTWARE\Valve\Steam", @"SOFTWARE\Wow6432Node\Valve\Steam" };
            foreach (var subKey in hklmKeys)
            {
                try
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(subKey);
                    string val = key?.GetValue("InstallPath") as string;
                    if (!string.IsNullOrEmpty(val) && Directory.Exists(val))
                    {
                        val = val.Replace('/', System.IO.Path.DirectorySeparatorChar);
                        if (Directory.Exists(val))
                        {
                            return val;
                        }
                    }
                }
                catch { }
            }

            foreach (string drive in new List<string> { "C:", "D:", "E:", "F:", "G:", "H:" })
            {
                string val = $"{drive}\\Program Files (x86)\\Steam";
                if (Directory.Exists(val))
                {
                    return val;
                }
            }

            return null;
        }

        private const string FileMask = "persistent_user_data.bin*";
    }
}
