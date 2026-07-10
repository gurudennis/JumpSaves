using System.IO;

namespace JSL
{
    public class SaveFile
    {
        public SaveFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found: {path}");
            }

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
