using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JumpSaves.Model
{
    struct OnlyManagerShouldCreateThis { }

    public class PeriodicInfoArgs : EventArgs
    {
        public bool IsRunning { get; set; }

        public DateTime? LastSaveTime { get; set; }
    }

    public class Instance
    {
        internal Instance(Manager manager, OnlyManagerShouldCreateThis onlyManagerShouldCreateThis)
        {
            manager_ = manager;
        }

        public event EventHandler<PeriodicInfoArgs> PeriodicInfoEvent;

        private void OnGlobalPeriodicInfo(object sender, GlobalPeriodicInfoEventArgs args)
        {
            PeriodicInfoEvent?.Invoke(this, new PeriodicInfoArgs
            {
                IsRunning = args.IsGameRunning
                // ...
            });
        }

        private Manager manager_;
    }
}
