using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JSL
{
    public class SaveState
    {
        public SaveState(byte[] bytes)
        {
            origBytes_ = bytes;

            ReadOnlySequence<byte> messagePackBytes = new ReadOnlySequence<byte>(bytes, MessagePackOffset, bytes.Length - MessagePackOffset);

            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
            root_ = MessagePackSerializer.Deserialize<object[]>(messagePackBytes, lz4Options);
        }

        public byte[] Bytes
        {
            get
            {
                var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
                byte[] reser = MessagePackSerializer.Serialize<object[]>(root_, lz4Options);

                byte[] result = new byte[MessagePackOffset + reser.Length];
                Buffer.BlockCopy(origBytes_, 0, result, 0, MessagePackOffset);
                Buffer.BlockCopy(reser, 0, result, MessagePackOffset, reser.Length);

                return result;
            }
        }

        public override string ToString()
        {
            return JSONFromObject(root_);
        }

        public class Location // can't point to the root by design
        {
            public Location()
            {
                Sequence = new List<int>();
            }

            public bool IsValid
            {
                get
                {
                    return Sequence.Count != 0;
                }
            }

            public override string ToString()
            {
                return IsValid ? string.Join("/", Sequence) : "<invalid>";
            }

            public List<int> Sequence { get; private set; }
        }

        public Location FindObject(string name, int nameDepth = 2)
        {
            if (String.IsNullOrEmpty(name) || nameDepth < 0)
            {
                return new Location();
            }

            return FindObjectRecursive(root_, new Location(), name, nameDepth);
        }

        public object GetObject(Location location)
        {
            if (!location.IsValid)
            {
                return null;
            }

            object current = root_;
            for (int i = 0; i < location.Sequence.Count; ++i)
            {
                current = ((object[])current)[location.Sequence[i]];
            }

            return current;
        }

        public void SetObject(Location location, object newValue)
        {
            if (!location.IsValid)
            {
                return;
            }

            object current = root_;
            for (int i = 0; i < location.Sequence.Count - 1; ++i)
            {
                current = ((object[])current)[location.Sequence[i]];
            }

            ((object[])current)[location.Sequence[location.Sequence.Count - 1]] = newValue;
        }

        public static byte? GetObjectPlacement(object o)
        {
            if (o == null || o.GetType() != typeof(object[]))
            {
                return null;
            }

            return (byte)((object[])o)[ObjectPlacementIndex];
        }

        public static void SetObjectPlacement(object o, byte placement)
        {
            if (o == null || o.GetType() != typeof(object[]))
            {
                return;
            }

            ((object[])o)[ObjectPlacementIndex] = placement;
        }

        public static string JSONFromObject(object o)
        {
            if (o == null)
            {
                return "<invalid>";
            }

            return JsonSerializer.Serialize(o, new JsonSerializerOptions { WriteIndented = true });
        }

        private Location FindObjectRecursive(object current, Location location, string name, int nameDepth)
        {
            if (current == null)
            {
                return new Location();
            }

            if (current.GetType() == typeof(object[]))
            {
                object[] children = (object[])current;
                for (int i = 0; i < children.Length; ++i)
                {
                    object child = children[i];
                    Location childLocation = new Location();
                    foreach (int pos in location.Sequence)
                    {
                        childLocation.Sequence.Add(pos);
                    }
                    childLocation.Sequence.Add(i);

                    Location found = FindObjectRecursive(child, childLocation, name, nameDepth);
                    if (found.IsValid)
                    {
                        return found;
                    }
                }
            }
            else if (current.GetType() == typeof(string))
            {
                string currentString = (string)current;
                if (currentString == name)
                {
                    if (location.Sequence.Count < nameDepth)
                    {
                        return new Location();
                    }

                    location.Sequence.RemoveRange(location.Sequence.Count - nameDepth, nameDepth);
                    return location;
                }
            }

            return new Location();
        }

        private const int MessagePackOffset = 13;
        private const int ObjectPlacementIndex = 3;
        private readonly byte[] origBytes_;
        private object[] root_;
    }
}
