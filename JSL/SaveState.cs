using MessagePack;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text.Json;

namespace JSL
{
    public interface ISaveMetadata
    {
        int SaveVersion { get; }

        string PlayerID { get; }
    }

    public class SaveMetadata : ArrayBasedObject, ISaveMetadata
    {
        public SaveMetadata(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in SaveMetadata, found {Root.Length}.");
            }
        }

        public string RawType
        {
            get
            {
                return GetPropertyStrict<string>(Index_RawType);
            }
        }

        public int SaveVersion
        {
            get
            {
                return GetPropertyStrict<int>(Index_SaveVersion);
            }
        }

        public string PlayerID
        {
            get
            {
                return GetPropertyStrict<string>(Index_PlayerID);
            }
        }

        private const int Index_RawType = 0;
        private const int Index_SaveVersion = 1;
        private const int Index_PlayerID = 2;
        private const int ExpectedElementCount = 3;
    }

    public class SaveState : ArrayBasedObject
    {
        public SaveState(byte[] bytes) : base(Initialize(bytes), null)
        {
            origBytes_ = bytes;
        }

        public override byte[] Bytes
        {
            get
            {
                var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
                byte[] reser = MessagePackSerializer.Serialize<object[]>(Root, lz4Options);

                byte[] result = new byte[MessagePackOffset + reser.Length];
                Buffer.BlockCopy(origBytes_, 0, result, 0, MessagePackOffset);
                Buffer.BlockCopy(reser, 0, result, MessagePackOffset, reser.Length);

                return result;
            }
        }

        public override string ToString()
        {
            return JSONFromObject(Root);
        }

        public Location FindObject(string name, Location after = null, int nameDepth = 2)
        {
            if (String.IsNullOrEmpty(name) || nameDepth < 0)
            {
                return new Location();
            }

            return FindObjectRecursive(Root, new Location(), name, after, nameDepth);
        }

        public object GetObject(Location location)
        {
            if (!location.IsValid)
            {
                return null;
            }

            object current = Root;
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

            object current = Root;
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

            return (byte)((object[])o)[DeepIndex_ObjectPlacement];
        }

        public static void SetObjectPlacement(object o, byte placement)
        {
            if (o == null || o.GetType() != typeof(object[]))
            {
                return;
            }

            ((object[])o)[DeepIndex_ObjectPlacement] = placement;
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

        public SaveMetadata SaveMetadata
        {
            get
            {
                return new SaveMetadata(GetSubObjectStrict(Index_SaveMetadata), Root);
            }
        }

        public object[] Ships
        {
            get
            {
                return GetSubArrayStrict(Index_Ships);
            }
            set
            {
                SetSubArrayStrict(Index_Ships, value);
            }
        }

        public string CurrentShipRawType
        {
            get
            {
                return GetPropertyStrict<string>(Index_CurrentShipRawType);
            }
        }

        public object[] PlayerInventory
        {
            get
            {
                return GetSubArrayStrict(Index_PlayerInventory);
            }
            set
            {
                SetSubArrayStrict(Index_PlayerInventory, value);
            }
        }

        public object[] StoredMajorItems
        {
            get
            {
                return GetSubArrayStrict(Index_StoredMajorItems);
            }
            set
            {
                SetSubArrayStrict(Index_StoredMajorItems, value);
            }
        }

        public MajorItemSlotUpgrades MajorItemSlotUpgrades
        {
            get
            {
                return new MajorItemSlotUpgrades(GetSubArrayStrict(Index_MajorItemSlotUpgrades), Root);
            }
        }

        public object[] RecentMajorItems
        {
            get
            {
                return GetSubArrayStrict(Index_RecentMajorItems);
            }
            set
            {
                SetSubArrayStrict(Index_RecentMajorItems, value);
            }
        }

        public Resources Resources
        {
            get
            {
                return new Resources(GetSubArrayStrict(Index_Resources)[0], Root);
            }
        }

        private static object Initialize(byte[] bytes)
        {
            ReadOnlySequence<byte> messagePackBytes = new ReadOnlySequence<byte>(bytes, MessagePackOffset, bytes.Length - MessagePackOffset);

            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
            return MessagePackSerializer.Deserialize<object[]>(messagePackBytes, lz4Options);
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

        private const int DeepIndex_ObjectPlacement = 3;

        private const int Index_SaveMetadata = 0;
        private const int Index_Ships = 3;
        private const int Index_CurrentShipRawType = 6;
        private const int Index_PlayerInventory = 7;
        private const int Index_StoredMajorItems = 11;
        private const int Index_MajorItemSlotUpgrades = 12;
        private const int Index_RecentMajorItems = 13;
        private const int Index_Resources = 14;

        private byte[] origBytes_;
    }
}
