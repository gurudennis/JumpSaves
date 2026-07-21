using System;
using System.Collections.Generic;

namespace JumpSaves.Model
{
    public class ActionLog
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
        }

        public void AddEntry(Origin origin, Level level, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Log action text can't be null or empty");
            }

            entries_.Add(new Entry(origin, level, text));
        }

        public IReadOnlyCollection<Entry> Entries
        {
            get
            {
                return entries_;
            }
        }

        private List<Entry> entries_ = new List<Entry>();
    }
}
