using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace JSL
{
    public class Library
    {
        public Library(string path)
        {
            Path = path;
            Load();
        }

        public string Path { get; private set; }

        public class Entry
        {
            public string FileName { get; internal set; }

            public LibraryMajorItem Item { get; internal set; }
        }

        public IReadOnlyList<Entry> Entries
        {
            get
            {
                return entries_;
            }
        }

        public IReadOnlyList<string> TakeFailedFiles()
        {
            IReadOnlyList<string> prev = failedFiles_;
            failedFiles_ = new List<string>();
            return prev;
        }

        public enum ConflictBehavior
        {
            Error,
            Overwrite,
        }

        public void AddEntry(LibraryMajorItem item, ConflictBehavior onConflict)
        {
            AddEntry(item, item.Bytes, MakeHash(item), onConflict);
        }

        public void ReplaceEntry(int index, LibraryMajorItem item)
        {
            if (item == null || ContainsItem(item))
            {
                throw new ArgumentException("Invalid or duplicate item");
            }

            // Prepare everything so as to make it less likely that we remove and then fail to add
            byte[] serialized = item.Bytes;
            string hash = MakeHash(item);
            string fileName = MakeFileName(item.Blueprint.Name, hash);

            // There can be an identical item only if it's the one being replaced
            Entry previous = FindByFileName(fileName);
            if (previous != null && entries_.IndexOf(previous) != index)
            {
                throw new Exception($"Intended to replace {entries_[index].FileName} but actually would replace {previous.FileName}. Skipping!");
            }

            RemoveEntry(index);

            AddEntry(item, serialized, hash, ConflictBehavior.Overwrite);
        }

        public void RemoveEntry(int index)
        {
            if (index < 0 || index > entries_.Count)
            {
                throw new ArgumentOutOfRangeException($"Invalid index {index} for a collection of {entries_.Count} entries");
            }

            string path = MakeFilePath(entries_[index].FileName);
            if (File.Exists(path)) // the alternative is odd but permissible
            {
                File.Delete(path);
            }

            entries_.RemoveAt(index);
        }

        private void Load()
        {
            DirectoryInfo dir = new DirectoryInfo(Path);
            if (!dir.Exists)
            {
                Directory.CreateDirectory(Path);
            }

            foreach (FileInfo file in dir.GetFiles("*.jsi"))
            {
                try
                {
                    entries_.Add(ReadEntry(file));
                }
                catch
                {
                    AddFailure(file.FullName);
                }
            }
        }

        private void AddEntry(LibraryMajorItem item, byte[] serialized, string hash, ConflictBehavior onConflict)
        {
            if (item == null || ContainsItem(item))
            {
                throw new ArgumentException("Invalid or duplicate item");
            }

            Entry entry = new Entry();
            entry.FileName = MakeFileName(item.Blueprint.Name, hash);
            entry.Item = item;

            WriteEntry(entry, serialized, onConflict);

            entries_.Add(entry);
        }

        private Entry ReadEntry(FileInfo fileInfo)
        {
            Entry entry = new Entry();
            entry.FileName = fileInfo.Name;

            using (FileStream file = File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                if (file.Length <= HeaderSize)
                {
                    throw new Exception($"Library file too small to be valid at {file.Length} bytes");
                }

                byte[] header = new byte[HeaderSize];
                file.Read(header, 0, header.Length);

                uint version = BitConverter.ToUInt32(header, 0);
                if (version > Version)
                {
                    throw new Exception($"Library file versioned as {version} but this software can only interpret versions up to {Version}");
                }

                uint dataLength = BitConverter.ToUInt32(header, VersionSize);
                if (dataLength != file.Length - HeaderSize)
                {
                    throw new Exception($"Corrupted library file claims to contain {dataLength} bytes of data, actually contains {file.Length - HeaderSize} bytes of data.");
                }

                byte[] data = new byte[dataLength];
                file.Read(data, 0, data.Length);

                entry.Item = new LibraryMajorItem(data);
            }

            return entry;
        }

        private void WriteEntry(Entry entry, byte[] serialized, ConflictBehavior onConflict)
        {
            string path = MakeFilePath(entry.FileName);
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Invalid entry path");
            }

            Entry previous = FindByFileName(entry.FileName);
            if (previous != null)
            {
                if (onConflict == ConflictBehavior.Error)
                {
                    throw new Exception($"Would overwrite an existing item at {previous.FileName}, skipping!");
                }
                else // if (onConflict == ConflictBehavior.Overwrite)
                {
                    RemoveEntry(previous);
                }
            }

            byte[] versionBytes = BitConverter.GetBytes(Version);
            if (versionBytes.Length != VersionSize)
            {
                throw new Exception($"Expected new version object to be {VersionSize} bytes, came out as {versionBytes.Length}");
            }

            byte[] dataLengthBytes = BitConverter.GetBytes(serialized.Length);
            if (dataLengthBytes.Length != DataLengthSize)
            {
                throw new Exception($"Expected new data length object to be {DataLengthSize} bytes, came out as {dataLengthBytes.Length}");
            }

            using (FileStream file = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                file.Write(versionBytes, 0, versionBytes.Length);
                file.Write(dataLengthBytes, 0, dataLengthBytes.Length);
                file.Write(serialized, 0, serialized.Length);
            }
        }

        private void RemoveEntry(Entry entry)
        {
            RemoveEntry(entries_.IndexOf(entry));
        }

        private bool ContainsItem(ArrayBasedObject item)
        {
            return entries_.Any(e => e.Item == item);
        }

        private Entry FindByFileName(string fileName)
        {
            return entries_.Where(e => e.FileName == fileName).FirstOrDefault();
        }

        private void AddFailure(string path)
        {
            failedFiles_.Add(path);
        }

        private string MakeSafeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            return new string(name.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
        }

        private string MakeFileName(string name, string hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                throw new ArgumentNullException("Invalid hash");
            }

            string nameSuffix = name == null ? String.Empty : $" {MakeSafeName(name)}";
            return $"{hash}{nameSuffix}.jsi";
        }

        private string MakeFilePath(string fileName)
        {
            return System.IO.Path.Combine(Path, fileName);
        }

        private static string MakeHash(LibraryMajorItem item)
        {
            // Clone, and reset all volatile values
            LibraryMajorItem clone = item.Clone();
            clone.Timestamp = DateTime.MinValue;
            clone.HasEverBeenStored = false;
            clone.Blueprint.OwningPlayerID = string.Empty;
            clone.Blueprint.ResetActivePips();

            // Serialize, and compute the hash
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(clone.Bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        private static readonly int VersionSize = 4;
        private static readonly int DataLengthSize = 4;
        private static readonly int HeaderSize = VersionSize + DataLengthSize;
        private static readonly uint Version = 1; // version of the format of the library itself

        private List<Entry> entries_ = new List<Entry>();
        private List<string> failedFiles_ = new List<string>();
    }
}
