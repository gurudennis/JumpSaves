using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
    public abstract class SaveEditor
    {
        public SaveEditor()
        {
            OpenedTime = DateTime.Now;
        }

        public abstract string Path { get; }

        public DateTime OpenedTime { get; private set; }

        public abstract DateTime LastEditTime { get; }

        public bool IsDirty { get; protected set; }

        protected SaveFile File { get; set; }

        protected SaveState State
        {
            get
            {
                return File?.State;
            }
        }
    }

    public class SaveFileEditor : SaveEditor
    {
        public SaveFileEditor(string path)
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
    }

    public class SaveDirEditor : SaveEditor
    {
        public SaveDirEditor(string path)
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

        private SaveDir dir_;
    }

    public class SaveEditorFactory
    {
        public static SaveEditor Create(string path)
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
    }
}
