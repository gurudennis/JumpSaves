using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
    public interface IRootEditor
    {
        ISaveMetadata SaveMetadata { get; }

        bool IsDirty { get; set; }
    }

    public abstract class Editor
    {
        protected Editor(IRootEditor rootEditor)
        {
            RootEditor = rootEditor;
        }

        protected IRootEditor RootEditor { get; private set; }
    }

    public abstract class SaveEditor : IRootEditor
    {
        internal SaveEditor()
        {
            OpenedTime = DateTime.Now;
        }

        public abstract string Path { get; }

        public DateTime OpenedTime { get; private set; }

        public abstract DateTime LastEditTime { get; }

        public ISaveMetadata SaveMetadata
        {
            get
            {
                return File?.State?.SaveMetadata;
            }
        }

        public bool IsDirty { get; set; }

        public ResourceEditor Resources
        {
            get
            {
                return new ResourceEditor(File.State.Resources, this);
            }
        }

        public MajorItemListEditor StoredMajorItems
        {
            get
            {
                return new StoredMajorItemListEditor(File.State, this);
            }
        }

        public MajorItemListEditor RecentMajorItems
        {
            get
            {
                return new RecentMajorItemListEditor(File.State, this);
            }
        }

        public abstract void Save();

        protected SaveFile File { get; set; }
    }

    public class SaveFileEditor : SaveEditor
    {
        internal SaveFileEditor(string path)
        {
            File = new SaveFile(path);
        }

        public override string Path
        {
            get
            {
                return File.Path;
            }
        }

        public override DateTime LastEditTime
        {
            get
            {
                FileInfo info = new FileInfo(Path);
                return info.LastWriteTime;
            }
        }

        public override void Save()
        {
            File.Save();
            IsDirty = false;
        }
    }

    public class SaveDirEditor : SaveEditor
    {
        internal SaveDirEditor(string path)
        {
            dir_ = new SaveDir(path);
            File = dir_.SaveFile;
        }

        public override string Path
        {
            get
            {
                return dir_.Path;
            }
        }

        public override DateTime LastEditTime
        {
            get
            {
                DateTime latest = DateTime.MinValue;
                foreach (string name in SaveDir.FileNames)
                {
                    FileInfo info = new FileInfo(System.IO.Path.Combine(Path, name));
                    DateTime lastWrite = info.LastWriteTime;
                    if (lastWrite > latest)
                    {
                        latest = lastWrite;
                    }
                }

                return latest;
            }
        }

        public override void Save()
        {
            dir_.Save(File);
            IsDirty = false;
        }

        private SaveDir dir_;
    }

    public class EditorFactory
    {
        public static SaveEditor OpenSave(string path)
        {
            if (Directory.Exists(path))
            {
                return new SaveDirEditor(path);
            }
            else if (File.Exists(path))
            {
                return new SaveFileEditor(path);
            }

            throw new Exception($"The path is invalid: {path}");
        }

        public static LibraryMajorItemListEditor OpenLibrary(string path)
        {
            return new LibraryMajorItemListEditor(new Library(path), new LibraryRootEditor());
        }

        public static LibraryMajorItemListEditor OpenLibrary(Library library)
        {
            return new LibraryMajorItemListEditor(library, new LibraryRootEditor());
        }

        private class LibraryRootEditor : IRootEditor
        {
            public ISaveMetadata SaveMetadata
            {
                get
                {
                    return metadata_;
                }
            }

            public bool IsDirty
            {
                get
                {
                    return false;
                }
                set
                {
                    // Does nothing.
                }
            }

            private class LibrarySaveMetadata : ISaveMetadata
            {
                public int SaveVersion
                {
                    get
                    {
                        return 56; // the version that the library currently implements
                    }
                }

                public string PlayerID
                {
                    get
                    {
                        return string.Empty;
                    }
                }
            }

            private LibrarySaveMetadata metadata_ = new LibrarySaveMetadata();
        }
    }
}
