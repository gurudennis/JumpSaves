using MessagePack;
using System;

namespace JSL
{
    public enum Rarity
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Superior = 3,
    }

    public class Module : ArrayBasedObject
    {
        public Module(object o, object[] parent) : base(o, parent)
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

        public int ActivePips
        {
            get
            {
                return GetPropertyStrict<int>(Index_ActivePips);
            }
            set
            {
                SetPropertyStrict(Index_ActivePips, value);
            }
        }

        public Rarity Rarity
        {
            get
            {
                return GetPropertyStrict<Rarity>(Index_Rarity);
            }
            set
            {
                SetPropertyStrict(Index_Rarity, value);
            }
        }

        public double[] Potencies
        {
            get
            {
                object[] arr = GetSubArrayStrict(Index_Potencies);
                double[] res = new double[arr.Length];
                for (int i = 0; i < arr.Length; ++i)
                {
                    res[i] = (double)arr[i];
                }
                return res;
            }
            set
            {
                object[] res = new object[value.Length];
                for (int i = 0; i < value.Length; ++i)
                {
                    res[i] = value[i];
                }
                SetSubArrayStrict(Index_Potencies, res);
            }
        }

        private const int Index_RawType = 0;
        private const int Index_ActivePips = 1;
        private const int Index_Rarity = 2;
        private const int Index_Potencies = 3;
    }

    public class MajorItemBlueprint : ArrayBasedObject
    {
        public MajorItemBlueprint(object o, object[] parent) : base(o, parent)
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

        public int Level
        {
            get
            {
                return GetPropertyStrict<int>(Index_Level);
            }
            set
            {
                SetPropertyStrict(Index_Level, value);
            }
        }

        public int MaxActivePips
        {
            get
            {
                return Modules.Length + Level - 1;
            }
        }

        public Rarity Rarity
        {
            get
            {
                return GetPropertyStrict<Rarity>(Index_Rarity);
            }
            set
            {
                SetPropertyStrict(Index_Rarity, value);
            }
        }

        public object[] Modules
        {
            get
            {
                return GetSubArrayStrict(Index_Modules);
            }
            set
            {
                SetSubArrayStrict(Index_Modules, value);
            }
        }

        public string Name
        {
            get
            {
                return GetPropertyStrict<string>(Index_Name);
            }
            set
            {
                SetPropertyStrict(Index_Name, value);
            }
        }

        private const int Index_RawType = 0;
        private const int Index_Level = 1;
        private const int Index_Rarity = 2;
        private const int Index_Modules = 3;
        private const int Index_Name = 12;
    }

    public class StoredMajorItem : ArrayBasedObject
    {
        public StoredMajorItem(object o, object[] parent) : base(o, parent)
        {
        }

        public string RawCategory
        {
            get
            {
                return GetPropertyStrict<string>(Index_RawCategory);
            }
            set
            {
                SetPropertyStrict(Index_RawCategory, value);
            }
        }

        public MajorItemBlueprint Blueprint
        {
            get
            {
                return new MajorItemBlueprint(GetSubObjectStrict(Index_Blueprint), Root);
            }
            set
            {
                SetSubObjectStrict(Index_Blueprint, value.Root);
            }
        }

        public int PlacementInCategory // in-format type: byte
        {
            get
            {
                return GetPropertyStrict<int>(Index_PlacementInCategory);
            }
            set
            {
                SetPropertyStrict(Index_PlacementInCategory, value);
            }
        }

        private const int Index_RawCategory = 0;
        private const int Index_Blueprint = 1;
        private const int Index_PlacementInCategory = 3;
    }

    public class MajorItemSlotUpgrade : ArrayBasedObject
    {
        public MajorItemSlotUpgrade(object o, object[] parent) : base(o, parent)
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

        public int SlotCount
        {
            get
            {
                return GetPropertyStrict<int>(Index_SlotCount);
            }
            set
            {
                SetPropertyStrict(Index_SlotCount, value);
            }
        }

        private const int Index_RawType = 0;
        private const int Index_SlotCount = 0;
    }

    public class MajorItemSlotUpgrades : ArrayBasedObject
    {
        public MajorItemSlotUpgrades(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in MajorItemSlotUpgrades, found {Root.Length}.");
            }
        }

        public MajorItemSlotUpgrade[] Elements
        {
            get
            {
                return GetFixedElementsStrict<MajorItemSlotUpgrade>();
            }
        }

        private const int ExpectedElementCount = 9;
    }

    public class RecentMajorItem : ArrayBasedObject
    {
        public RecentMajorItem(object o, object[] parent) : base(o, parent)
        {
        }

        public string RawCategory
        {
            get
            {
                return GetPropertyStrict<string>(Index_RawCategory);
            }
            set
            {
                SetPropertyStrict(Index_RawCategory, value);
            }
        }

        public MajorItemBlueprint Blueprint
        {
            get
            {
                return new MajorItemBlueprint(GetSubObjectStrict(Index_Blueprint), Root);
            }
            set
            {
                SetSubObjectStrict(Index_Blueprint, value.Root);
            }
        }

        private const int Index_RawCategory = 0;
        private const int Index_Blueprint = 1;
    }

    // Library items largely mimick the recent item format,
    // with the exception of a small custom header when
    // serialized.
    public class LibraryMajorItem : RecentMajorItem
    {
        public LibraryMajorItem(object o) : base(o, null)
        {
        }

        public LibraryMajorItem(byte[] b) : base(Deserialize(b), null)
        {
        }

        public LibraryMajorItem Clone()
        {
            return new LibraryMajorItem(Bytes);
        }

        public override byte[] Bytes
        {
            get
            {
                return MessagePackSerializer.Serialize(Root);
            }
        }

        private static object Deserialize(byte[] b)
        {
            return MessagePackSerializer.Deserialize<object[]>(b);
        }
    }
}
