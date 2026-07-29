using System;
using System.Diagnostics;
using System.Threading;

namespace JumpSaves.Model
{
    public class GlobalPeriodicInfoEventArgs : EventArgs
    {
        public bool IsGameRunning { get; set; }
    }

    public class Manager : IDisposable
    {
        public Manager()
        {
            Settings = new Settings(DefaultSettingsPath);
            ActionLog = new ActionLog();

            thread_ = new Thread(() => { ThreadProc(); });
            thread_.Start();
        }

        public void Dispose()
        {
            stop_.Set();
            thread_.Join();

            ActionLog.Dispose();
            Settings.Dispose();
        }

        public Instance CreateInstance(SynchronizationContext syncContext)
        {
            return new Instance(syncContext, this, new OnlyManagerShouldCreateThis());
        }

        public Settings Settings { get; private set; }

        public ActionLog ActionLog { get; private set; }

        public JSL.Library Library
        {
            get; private set;
        }

        public JSL.BackupStore BackupStore
        {
            get; private set;
        }

        public event EventHandler<GlobalPeriodicInfoEventArgs> PeriodicInfoEvent;

        private void ThreadProc()
        {
            Library = new JSL.Library(DefaultLibraryPath);
            EmitPeriodicInfoEvent();

            if (stop_.WaitOne(0))
            {
                return;
            }

            BackupStore = new JSL.BackupStore(DefaultBackupStorePath, 100);
            EmitPeriodicInfoEvent();

            do
            {
                EmitPeriodicInfoEvent();
            }
            while (!stop_.WaitOne(2000));
        }

        private void EmitPeriodicInfoEvent()
        {
            GlobalPeriodicInfoEventArgs args = new GlobalPeriodicInfoEventArgs();

            Process[] processes = Process.GetProcessesByName("Jump Space");
            args.IsGameRunning = (processes != null && processes.Length > 0);

            PeriodicInfoEvent?.Invoke(this, args);
        }

        private string DefaultSettingsPath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JumpSaves", "JumpSavesSettings.json");
            }
        }

        private string DefaultLibraryPath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JumpSaves", "Library");
            }
        }

        private string DefaultBackupStorePath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JumpSaves", "Backups");
            }
        }

        private static ManualResetEvent stop_ = new ManualResetEvent(false);
        private readonly Thread thread_;
    }
}
