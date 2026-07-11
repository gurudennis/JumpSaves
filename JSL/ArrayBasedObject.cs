using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
    public class ArrayBasedObject
    {
        public ArrayBasedObject(object o)
        {
            Root = o as object[];
        }

        public object[] Root
        {
            get
            {
                return root_;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("The provided object is invalid");
                }

                root_ = value;
            }
        }

        public bool GetProperty<T>(int index, out T value)
        {
            if (index >= Root.Length || !(Root[0] is T))
            {
                value = default(T);
                return false;
            }

            value = (T)Root[index];
            return true;
        }

        public bool SetProperty(int index, object value, bool adaptive = true)
        {
            if (index >= Root.Length)
            {
                return false;
            }

            if (adaptive)
            {
                if (Root[index] != null)
                {
                    Root[index] = Convert.ChangeType(value, Root[index].GetType());
                }
                else
                {
                    Root[index] = value;
                }
            }
            else
            {
                if (value != null && Root[index] != null && Root[index].GetType() != value.GetType())
                {
                    return false;
                }

                Root[index] = value;
            }

            return true;
        }

        private object[] root_;
    }
}
