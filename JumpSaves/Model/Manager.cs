using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            stop_ = true;
            thread_.Join();
        }

        public Instance CreateInstance(SynchronizationContext syncContext)
        {
            return new Instance(syncContext, this, new OnlyManagerShouldCreateThis());
        }

        public event EventHandler<GlobalPeriodicInfoEventArgs> PeriodicInfoEvent;

        private void ThreadProc()
        {
            while (!stop_)
            {
                Thread.Sleep(2000);

                GlobalPeriodicInfoEventArgs args = new GlobalPeriodicInfoEventArgs();

                Process[] processes = Process.GetProcessesByName("Jump Space");
                args.IsGameRunning = (processes != null && processes.Length > 0);

                PeriodicInfoEvent?.Invoke(this, args);
            }
        }

        private bool stop_ = false;
        private readonly Thread thread_;
    }
}
