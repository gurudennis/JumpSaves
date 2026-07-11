using MessagePack;
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

        public ArrayBasedObject(byte[] b)
        {
            Root = MessagePackSerializer.Deserialize<object[]>(b);
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

        public virtual byte[] Bytes
        {
            get
            {
                return MessagePackSerializer.Serialize(Root);
            }
        }

        public bool GetProperty<T>(int index, out T value)
        {
            if (index >= Root.Length)
            {
                value = default(T);
                return false;
            }

            try
            {
                value = (T)Convert.ChangeType(Root[index], typeof(T));
            }
            catch (Exception)
            {
                value = default(T);
                return false;
            }

            return true;
        }

        public T GetPropertyStrict<T>(int index)
        {
            if (!GetProperty<T>(index, out T value))
            {
                throw new Exception($"Failed to retrieve property of type {typeof(T)} at index {index}.");
            }

            return value;
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
                    try
                    {
                        Root[index] = Convert.ChangeType(value, Root[index].GetType());
                    }
                    catch (Exception)
                    {
                        return false;
                    }
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

        public void SetPropertyStrict(int index, object value, bool adaptive = true)
        {
            if (!SetProperty(index, value, adaptive))
            {
                throw new Exception($"Failed to set property at index {index}.");
            }
        }

        public bool GetSubObject(int index, out object value)
        {
            if (index >= Root.Length)
            {
                value = null;
                return false;
            }

            value = Root[index];
            return true;
        }

        public object GetSubObjectStrict(int index)
        {
            if (!GetSubObject(index, out object value))
            {
                throw new Exception($"Failed to retrieve sub-object at index {index}.");
            }

            return value;
        }

        public bool SetSubObject(int index, object value)
        {
            if (index >= Root.Length)
            {
                return false;
            }

            Root[index] = value;

            return true;
        }

        public void SetSubObjectStrict(int index, object value)
        {
            if (!SetSubObject(index, value))
            {
                throw new Exception($"Failed to set sub-object at index {index}.");
            }
        }

        public bool GetSubArray(int index, out object[] value)
        {
            if (!GetSubObject(index, out object v) || !(v is object[]))
            {
                value = null;
                return false;
            }

            value = (object[])v;
            return true;
        }

        public object[] GetSubArrayStrict(int index)
        {
            if (!GetSubArray(index, out object[] value))
            {
                throw new Exception($"Failed to retrieve sub-array at index {index}.");
            }

            return value;
        }

        public bool SetSubArray(int index, object[] value)
        {
            return SetSubObject(index, value);
        }

        public void SetSubArrayStrict(int index, object[] value)
        {
            if (!SetSubArray(index, value))
            {
                throw new Exception($"Failed to set sub-array at index {index}.");
            }
        }

        public T[] GetFixedElementsStrict<T>() where T : ArrayBasedObject
        {
            T[] res = new T[Root.Length];
            for (int i = 0; i < Root.Length; ++i)
            {
                res[i] = (T)Activator.CreateInstance(typeof(T), GetSubObjectStrict(i));
            }

            return res;
        }

        private object[] root_;
    }
}
