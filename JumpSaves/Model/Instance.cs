using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace JumpSaves.Model
{
    struct OnlyManagerShouldCreateThis { }

    public class PeriodicInfoArgs : EventArgs
    {
        public bool IsRunning { get; set; }

        public DateTime? LastSaveTime { get; set; }
    }

    public class Instance : IDisposable
    {
        internal Instance(SynchronizationContext syncContext, Manager manager, OnlyManagerShouldCreateThis onlyManagerShouldCreateThis)
        {
            syncContext_ = syncContext;
            manager_ = manager;

            manager_.PeriodicInfoEvent += OnGlobalPeriodicInfo;
        }

        public void Dispose()
        {
            Close();
        }

        public string DefaultPath
        {
            get
            {
#if DEBUG
                return "C:\\Prj\\JumpSpaceSaves\\Data\\FakeDir";
#else
                return JSL.SaveDir.Default.Path;
#endif
            }
        }

        public void Open(string path)
        {
            Close();

            Editor = JSL.SaveEditorFactory.Create(path);
        }

        public void Close()
        {
            Editor = null;
        }

        public bool IsOpen
        {
            get
            {
                return Editor != null;
            }
        }

        public bool IsDirty
        {
            get
            {
                return Editor?.IsDirty ?? false;
            }
        }

        public bool IsGameRunning { get; private set; }

        public JSL.SaveEditor Editor { get; private set; }

        public event EventHandler<PeriodicInfoArgs> PeriodicInfoEvent;

        public void RunCLI()
        {
            string selfPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string cliPath = Path.Combine(Path.GetDirectoryName(selfPath), "JumpSavesCLI.exe");
            Process.Start(cliPath, $"-s {Editor?.Path ?? DefaultPath}");
        }

        private void OnGlobalPeriodicInfo(object sender, GlobalPeriodicInfoEventArgs args)
        {
            PostOnUIThread(() =>
            {
                IsGameRunning = args.IsGameRunning;

                PeriodicInfoEvent?.Invoke(this, new PeriodicInfoArgs
                {
                    IsRunning = args.IsGameRunning,
                    LastSaveTime = Editor?.LastEditTime
                });
            });
        }

        private void PostOnUIThread(Action action)
        {
            syncContext_.Post(_ => { action(); }, null);
        }

        private Manager manager_;
        private readonly SynchronizationContext syncContext_;
    }
}
