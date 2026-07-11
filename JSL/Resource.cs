using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
    public class Resource : ArrayBasedObject
    {
        public Resource(object o) : base(o)
        {
        }

        public string RawType
        {
            get
            {
                return GetPropertyStrict<string>(Index_RawType);
            }
            set
            {
                SetPropertyStrict(Index_RawType, value);
            }
        }

        public int Value
        {
            get
            {
                return GetPropertyStrict<int>(Index_Value);
            }
            set
            {
                SetPropertyStrict(Index_Value, value);
            }
        }

        private const int Index_RawType = 0;
        private const int Index_Value = 0;
    }

    public class Resources : ArrayBasedObject
    {
        public Resources(object o) : base(o)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in Resources, found {Root.Length}.");
            }
        }

        public Resource[] Elements
        {
            get
            {
                return GetFixedElementsStrict<Resource>();
            }
        }

        public int Credits
        {
            get
            {
                return Elements[Index_Credits].Value;
            }
            set
            {
                Elements[Index_Credits].Value = value;
            }
        }

        public int GreenIngots
        {
            get
            {
                return Elements[Index_GreenIngots].Value;
            }
            set
            {
                Elements[Index_GreenIngots].Value = value;
            }
        }

        public int BlueIngots
        {
            get
            {
                return Elements[Index_BlueIngots].Value;
            }
            set
            {
                Elements[Index_BlueIngots].Value = value;
            }
        }

        public int PurpleIngots
        {
            get
            {
                return Elements[Index_PurpleIngots].Value;
            }
            set
            {
                Elements[Index_PurpleIngots].Value = value;
            }
        }

        public int OrangeIngots
        {
            get
            {
                return Elements[Index_OrangeIngots].Value;
            }
            set
            {
                Elements[Index_OrangeIngots].Value = value;
            }
        }

        public int RedIngots
        {
            get
            {
                return Elements[Index_RedIngots].Value;
            }
            set
            {
                Elements[Index_RedIngots].Value = value;
            }
        }

        private const int ExpectedElementCount = 6;

        private const int Index_Credits = 1;
        private const int Index_GreenIngots = 1;
        private const int Index_BlueIngots = 2;
        private const int Index_PurpleIngots = 3;
        private const int Index_OrangeIngots = 4;
        private const int Index_RedIngots = 5;
    }
}
