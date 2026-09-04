using System;
using System.IO;

namespace JSL
{
    public interface IRootEditor
    {
        ISaveMetadata SaveMetadata { get; }

        IMajorItemSlotLimits MajorItemSlotLimits { get; }

        bool IsDirty { get; set; }
    }

    public abstract class RootEditor : IRootEditor
    {
        public abstract ISaveMetadata SaveMetadata { get; }

        public abstract IMajorItemSlotLimits MajorItemSlotLimits { get; }

        public virtual bool IsDirty
        {
            get
            {
                return isDirty_;
            }
            set
            {
                if (value != isDirty_)
                {
                    isDirty_ = value;
                    DirtyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public EventHandler<EventArgs> DirtyChanged;

        private bool isDirty_ = false;
    }

    public abstract class Editor
    {
        protected Editor(IRootEditor rootEditor)
        {
            RootEditor = rootEditor;
        }

        internal bool IsOrphaned { get; set; }

        internal void SetDirtyIfNecessary()
        {
            if (!IsOrphaned)
            {
                RootEditor.IsDirty = true;
            }
        }

        protected IRootEditor RootEditor { get; private set; }
    }

    public abstract class SaveEditor : RootEditor, IMajorItemSlotLimits
    {
        internal SaveEditor()
        {
            OpenedTime = DateTime.Now;
        }

        public abstract string Path { get; }

        public DateTime OpenedTime { get; private set; }

        public abstract DateTime LastEditTime { get; }

        public abstract bool IsExperimental { get; }

        public override ISaveMetadata SaveMetadata
        {
            get
            {
                return File?.State?.SaveMetadata;
            }
        }

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

        public int DefaultMinSlotCount
        {
            get
            {
                return File.State.MajorItemSlotUpgrades.DefaultMinSlotCount;
            }
        }

        public int DefaultMaxSlotCount
        {
            get
            {
                return File.State.MajorItemSlotUpgrades.DefaultMaxSlotCount;
            }
        }

        public int GetMaxMajorItemSlots(MajorItemCategory.Enum category)
        {
            return File.State.MajorItemSlotUpgrades.GetMaxMajorItemSlots(category);
        }

        public void SetMaxMajorItemSlots(MajorItemCategory.Enum category, int slots)
        {
            int prev = GetMaxMajorItemSlots(category);
            File.State.MajorItemSlotUpgrades.SetMaxMajorItemSlots(category, slots);
            if (slots != prev)
            {
                IsDirty = true;
            }
        }

        public override IMajorItemSlotLimits MajorItemSlotLimits
        {
            get
            {
                return this;
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

        public override bool IsExperimental
        {
            get
            {
                return false;
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
        internal SaveDirEditor(string path, bool experimental)
        {
            dir_ = new SaveDir(path, experimental);
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
                foreach (string name in dir_.FileNames)
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

        public override bool IsExperimental
        {
            get
            {
                return dir_.IsExperimental;
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
        public static SaveEditor OpenSave(string path, bool experimental)
        {
            if (Directory.Exists(path))
            {
                return new SaveDirEditor(path, experimental);
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

        private class LibraryRootEditor : RootEditor, ISaveMetadata, IMajorItemSlotLimits
        {
            public override ISaveMetadata SaveMetadata
            {
                get
                {
                    return this;
                }
            }

            public override IMajorItemSlotLimits MajorItemSlotLimits
            {
                get
                {
                    return this;
                }
            }

            public override bool IsDirty
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

            public int DefaultMinSlotCount
            {
                get
                {
                    return 0;
                }
            }

            public int DefaultMaxSlotCount
            {
                get
                {
                    return int.MaxValue; // no limit
                }
            }

            public int GetMaxMajorItemSlots(MajorItemCategory.Enum category)
            {
                return DefaultMaxSlotCount;
            }

            public void SetMaxMajorItemSlots(MajorItemCategory.Enum category, int slots)
            {
                // Does nothing.
            }
        }
    }
}
