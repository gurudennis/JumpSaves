using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Net.WebRequestMethods;

namespace JumpSaves.Model
{
    public class ActionLog : IDisposable
    {
        public enum Level
        {
            Debug,
            Info,
            Warning,
            Error
        }

        public enum Origin
        {
            Application,
            Editor,
            Library
        }

        public class Entry
        {
            public Entry(Origin origin, Level level, string text)
            {
                Origin = origin;
                Level = level;
                Text = text;
                Timestamp = DateTime.Now;
            }

            public Origin Origin { get; set; }
                
            public Level Level { get; set; }

            public string Text { get; set; }

            public DateTime Timestamp { get; private set; }
        }

        internal ActionLog()
        {
            Open();
        }

        public void Dispose()
        {
            AddEntry(Origin.Application, Level.Info, "Closing log.");

            if (file_ != null)
            {
                file_.Close();
                file_.Dispose();
                file_ = null;
            }
        }

        public EventHandler<EventArgs> Changed;

        public void AddEntry(Origin origin, Level level, string text)
        {
#if !DEBUG
            if (level == Level.Debug)
            {
                return;
            }
#endif

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Log action text can't be null or empty");
            }

            Entry entry = new Entry(origin, level, text);
            entries_.Add(entry);

            if (file_ != null)
            {
                byte[] b = Encoding.UTF8.GetBytes($"[{entry.Timestamp}] [{entry.Level}] [{entry.Origin}]: {entry.Text}\r\n");
                file_.Write(b, 0, b.Length);
            }

            Changed?.Invoke(this, null);
        }

        public IReadOnlyCollection<Entry> Entries
        {
            get
            {
                return entries_;
            }
        }

        public string LocationPath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JumpSaves", "Logs");
            }
        }

        private void Open()
        {
            // Create the directory
            DirectoryInfo dir = new DirectoryInfo(LocationPath);
            if (!dir.Exists)
            {
                Directory.CreateDirectory(LocationPath);
            }

            // Clean up old files if any
            DateTime cutoff = DateTime.Now - TimeSpan.FromDays(7);
            foreach (FileInfo file in dir.GetFiles("*_JumpSaves_Log.txt"))
            {
                if (file.CreationTime < cutoff)
                {
                    file.Delete();
                }
            }

            // Open a new one
            string path = Path.Combine(LocationPath, $"{DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss")}_JumpSaves_Log.txt");
            file_ = System.IO.File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        }

        private List<Entry> entries_ = new List<Entry>();
        private FileStream file_;
    }
}
