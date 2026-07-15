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
                if (value != resources_.Credits)
                {
                    resources_.Credits = value;
                    rootEditor_.IsDirty = true;
                }
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
                if (value != resources_.GreenIngots)
                {
                    resources_.GreenIngots = value;
                    rootEditor_.IsDirty = true;
                }
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
                if (value != resources_.BlueIngots)
                {
                    resources_.BlueIngots = value;
                    rootEditor_.IsDirty = true;
                }
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
                if (value != resources_.PurpleIngots)
                {
                    resources_.PurpleIngots = value;
                    rootEditor_.IsDirty = true;
                }
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
                if (value != resources_.OrangeIngots)
                {
                    resources_.OrangeIngots = value;
                    rootEditor_.IsDirty = true;
                }
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
                if (value != resources_.RedIngots)
                {
                    resources_.RedIngots = value;
                    rootEditor_.IsDirty = true;
                }
            }
        }

        public static readonly int MaxCredits = 500000;
        public static readonly int MaxIngots = 1000;

        private IRootEditor rootEditor_;
        private SaveState state_;
        private Resources resources_;
    }
}
