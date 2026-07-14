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

        public event EventHandler<GlobalPeriodicInfoEventArgs> PeriodicInfoEvent;

        private void ThreadProc()
        {
            while (!stop_.WaitOne(2000))
            {
                GlobalPeriodicInfoEventArgs args = new GlobalPeriodicInfoEventArgs();

                Process[] processes = Process.GetProcessesByName("Jump Space");
                args.IsGameRunning = (processes != null && processes.Length > 0);

                PeriodicInfoEvent?.Invoke(this, args);
            }
        }

        private static ManualResetEvent stop_ = new ManualResetEvent(false);
        private readonly Thread thread_;
    }
}
