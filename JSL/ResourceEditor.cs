using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
    public class ResourceEditor
    {
        internal ResourceEditor(IRootEditor rootEditor)
        {
            rootEditor_ = rootEditor;
            state_ = rootEditor_.State;
            resources_ = state_.Resources;
        }

        public int Credits
        {
            get
            {
                return resources_.Credits;
            }
            set
            {
                resources_.Credits = value;
                rootEditor_.IsDirty = true;
            }
        }

        public int GreenIngots
        {
            get
            {
                return resources_.GreenIngots;
            }
            set
            {
                resources_.GreenIngots = value;
                rootEditor_.IsDirty = true;
            }
        }

        public int BlueIngots
        {
            get
            {
                return resources_.BlueIngots;
            }
            set
            {
                resources_.BlueIngots = value;
                rootEditor_.IsDirty = true;
            }
        }

        public int PurpleIngots
        {
            get
            {
                return resources_.PurpleIngots;
            }
            set
            {
                resources_.PurpleIngots = value;
                rootEditor_.IsDirty = true;
            }
        }

        public int OrangeIngots
        {
            get
            {
                return resources_.OrangeIngots;
            }
            set
            {
                resources_.OrangeIngots = value;
                rootEditor_.IsDirty = true;
            }
        }

        public int RedIngots
        {
            get
            {
                return resources_.RedIngots;
            }
            set
            {
                resources_.RedIngots = value;
                rootEditor_.IsDirty = true;
            }
        }

        private IRootEditor rootEditor_;
        private SaveState state_;
        private Resources resources_;
    }
}
