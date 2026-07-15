using JSL;
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
            thread_ = new Thread(() => { ThreadProc(); });
            thread_.Start();
        }

        public void Dispose()
        {
            stop_.Set();
            thread_.Join();
        }

        public Instance CreateInstance(SynchronizationContext syncContext)
        {
            return new Instance(syncContext, this, new OnlyManagerShouldCreateThis());
        }

        public JSL.Library Library
        {
            get; private set;
        }

        public event EventHandler<GlobalPeriodicInfoEventArgs> PeriodicInfoEvent;

        private void ThreadProc()
        {
            Library = new Library(DefaultLibraryPath);

            do
            {
                GlobalPeriodicInfoEventArgs args = new GlobalPeriodicInfoEventArgs();

                Process[] processes = Process.GetProcessesByName("Jump Space");
                args.IsGameRunning = (processes != null && processes.Length > 0);

                PeriodicInfoEvent?.Invoke(this, args);
            }
            while (!stop_.WaitOne(2000));
        }

        private string DefaultLibraryPath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JumpSaves", "Library");
            }
        }

        private static ManualResetEvent stop_ = new ManualResetEvent(false);
        private readonly Thread thread_;
    }
}
