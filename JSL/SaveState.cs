using MessagePack;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text.Json;

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
            public Location(string loc = null)
            {
                Sequence = new List<int>();
                if (!String.IsNullOrEmpty(loc))
                {
                    string[] parts = loc.Split('/');
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int value) && value >= 0)
                        {
                            Sequence.Add(value);
                        }
                        else
                        {
                            Sequence.Clear();
                            return;
                        }
                    }
                }
            }

            public Location(List<int> sequence)
            {
                Sequence = sequence != null ? new List<int>(sequence) : new List<int>();
            }

            public bool IsValid
            {
                get
                {
                    return Sequence.Count != 0;
                }
            }

            public Location Parent
            {
                get
                {
                    List<int> parentSequence = new List<int>();
                    foreach (int child in Sequence)
                    {
                        parentSequence.Add(child);
                    }
                    parentSequence.RemoveRange(parentSequence.Count - 1, 1);
                    return new Location(parentSequence);
                }
            }

            public bool IsAtOrAfter(Location location)
            {
                if (location == null || !location.IsValid)
                {
                    return true;
                }

                int count = Math.Min(Sequence.Count, location.Sequence.Count);
                for (int i = 0; i < count; ++i)
                {
                    if (Sequence[i] > location.Sequence[i])
                    {
                        return true;
                    }
                    else if (Sequence[i] < location.Sequence[i])
                    {
                        return false;
                    }
                }

                return Sequence.Count >= location.Sequence.Count;
            }

            public override string ToString()
            {
                return IsValid ? string.Join("/", Sequence) : "<invalid>";
            }

            public List<int> Sequence { get; private set; }
        }

        public Location FindObject(string name, Location after = null, int nameDepth = 2)
        {
            if (String.IsNullOrEmpty(name) || nameDepth < 0)
            {
                return new Location();
            }

            return FindObjectRecursive(root_, new Location(), name, after, nameDepth);
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
                if (location.Sequence[i] < 0 || location.Sequence[i] >= ((object[])current).Length)
                {
                    return null;
                }

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
                if (location.Sequence[i] < 0 || location.Sequence[i] >= ((object[])current).Length)
                {
                    return;
                }

                current = ((object[])current)[location.Sequence[i]];
            }

            int lastIndex = location.Sequence[location.Sequence.Count - 1];
            if (lastIndex < 0 || lastIndex >= ((object[])current).Length)
            {
                return;
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

        public static object ObjectFromJSON(string json, Type type)
        {
            if (string.IsNullOrEmpty(json) || type == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize(json, type);
        }

        public object[] GetMajorItems()
        {
            return GetObject(new Location(new List<int> { MajorItemsIndex })) as object[];
        }

        private Location FindObjectRecursive(object current, Location location, string name, Location after, int nameDepth)
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

                    Location found = FindObjectRecursive(child, childLocation, name, after, nameDepth);
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
                    if (location.IsAtOrAfter(after))
                    {
                        return location;
                    }
                }
            }

            return new Location();
        }

        private const int MessagePackOffset = 13;
        private const int ObjectPlacementIndex = 3;
        private const int MajorItemsIndex = 11;
        private readonly byte[] origBytes_;
        private object[] root_;
    }
}
