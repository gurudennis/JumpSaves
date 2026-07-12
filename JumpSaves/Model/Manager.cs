using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JumpSaves.Model
{
    public class GlobalPeriodicInfoEventArgs : EventArgs
    {
        public bool IsGameRunning { get; set; }

        public Dictionary<string, DateTime> LastSaveTimes { get; set; }
    }

    public class Manager
    {
        public Manager(SynchronizationContext syncContext)
        {
            syncContext_ = syncContext;
        }

        public Instance CreateInstance()
        {
            return new Instance(this, new OnlyManagerShouldCreateThis());
        }

        public event EventHandler<GlobalPeriodicInfoEventArgs> PeriodicInfoEvent
        {
            add
            {
                periodicInfoEvent_ = (EventHandler<GlobalPeriodicInfoEventArgs>)Delegate.Combine(periodicInfoEvent_, value);
            }
            remove
            {
                periodicInfoEvent_ = (EventHandler<GlobalPeriodicInfoEventArgs>)Delegate.Remove(periodicInfoEvent_, value);
            }
        }

        private void PostOnUIThread(Action action)
        {
            syncContext_.Post(_ => { action(); }, null);
        }

        private class ProtectedState
        {
            public void AddSaveOfInterest(string path)
            {
                // ...
            }

            private Mutex guard_ = new Mutex();
        }

        private readonly SynchronizationContext syncContext_;
        private EventHandler<GlobalPeriodicInfoEventArgs> periodicInfoEvent_;
        private ProtectedState protectedState_ = new ProtectedState();
    }
}
