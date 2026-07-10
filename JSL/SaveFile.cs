using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JSL
{
    public class SaveFile
    {
        public SaveFile(string path)
        {
            Path = path;
            Load();
        }

        public string Path { get; private set; }

        public SaveState State { get; private set; }

        public void Save()
        {
            File.WriteAllBytes(Path, State.Bytes);
        }

        private void Load()
        {
            State = new SaveState(File.ReadAllBytes(Path));
        }
    }
}
