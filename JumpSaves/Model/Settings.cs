using System;
using System.IO;
using System.Text.Json;

namespace JumpSaves.Model
{
    public class Settings : IDisposable
    {
        internal Settings(string path)
        {
            Path = path;
            Load();
        }

        public void Dispose()
        {
            File.WriteAllText(Path, JsonSerializer.Serialize(params_));
        }

        public string Path { get; private set; }

        public bool ShowTutorial
        {
            get
            {
                return params_.ShowTutorial;
            }
            set
            {
                params_.ShowTutorial = value;
            }
        }

        public bool Colorblind
        {
            get
            {
                return params_.Colorblind;
            }
            set
            {
                params_.Colorblind = value;
            }
        }

        private class Params
        {
            public bool ShowTutorial { get; set; } = true;

            public bool Colorblind { get; set; } = false;
        }

        private void Load()
        {
            string parent = System.IO.Path.GetDirectoryName(Path);
            if (!Directory.Exists(parent))
            {
                Directory.CreateDirectory(parent);
            }

            if (File.Exists(Path))
            {
                params_ = JsonSerializer.Deserialize<Params>(File.ReadAllText(Path));
            }
            else
            {
                params_ = new Params();
            }
        }

        private Params params_;
    }
}
