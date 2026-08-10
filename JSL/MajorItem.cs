using MessagePack;
using System;
using static JSL.PlayerWeaponCustomizationType;

namespace JSL
{
    public class Module : ArrayBasedObject
    {
        public Module() : this(New(), null)
        {
        }

        public Module(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in Module, found {Root.Length}.");
            }
        }

        public new Module Clone()
        {
            return new Module(Bytes, null);
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
                return (Rarity)GetPropertyStrict<byte>(Index_Rarity);
            }
            set
            {
                SetPropertyStrict(Index_Rarity, (byte)value);
            }
        }

        public double[] Potencies
        {
            get
            {
                if (!GetSubArray(Index_Potencies, out object[] arr))
                {
                    return new double[0];
                }

                double[] res = new double[arr.Length];
                for (int i = 0; i < arr.Length; ++i)
                {
                    res[i] = (float)arr[i];
                }
                return res;
            }
            set
            {
                object[] res = new object[value.Length];
                for (int i = 0; i < value.Length; ++i)
                {
                    res[i] = (float)value[i];
                }
                SetSubArrayStrict(Index_Potencies, res);
            }
        }

        private static object New()
        {
            Module module = new Module(new object[ExpectedElementCount], null);
            module.SetPropertyStrict(Index_RawType, string.Empty);
            module.SetPropertyStrict(Index_ActivePips, (byte)1);
            module.SetPropertyStrict(Index_Rarity, (byte)0);
            module.SetPropertyStrict(Index_Potencies, new object[0]);
            return module.Root;
        }

        private const int Index_RawType = 0;
        private const int Index_ActivePips = 1;
        private const int Index_Rarity = 2;
        private const int Index_Potencies = 3;
        private const int ExpectedElementCount = 4;
    }

    public class Customization : ArrayBasedObject
    {
        public Customization() : this(New(), null)
        {
        }

        public Customization(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in Customization, found {Root.Length}.");
            }
        }

        public new Module Clone()
        {
            return new Module(Bytes, null);
        }

        public int RawCategory
        {
            get
            {
                return GetPropertyStrict<int>(Index_RawCategory);
            }
            set
            {
                SetPropertyStrict(Index_RawCategory, value);
            }
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

        private static object New()
        {
            Customization module = new Customization(new object[ExpectedElementCount], null);
            module.SetPropertyStrict(Index_RawCategory, (byte)0);
            module.SetPropertyStrict(1, (byte)0); // unknown, always zero
            module.SetPropertyStrict(Index_RawType, string.Empty);
            return module.Root;
        }

        private const int Index_RawCategory = 0;
        private const int Index_RawType = 2;
        private const int ExpectedElementCount = 3;
    }

    public class MajorItemBlueprint : ArrayBasedObject
    {
        public MajorItemBlueprint() : this(New(), null)
        {
        }

        public MajorItemBlueprint(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in MajorItemBlueprint, found {Root.Length}.");
            }
        }

        public new MajorItemBlueprint Clone()
        {
            return new MajorItemBlueprint(Bytes, null);
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
                return (Rarity)GetPropertyStrict<byte>(Index_Rarity);
            }
            set
            {
                SetPropertyStrict(Index_Rarity, (byte)value);
            }
        }

        public int MaxModules
        {
            get
            {
                return 5;
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

        public object[] Customizations
        {
            get
            {
                return GetSubArrayStrict(Index_Customizations);
            }
            set
            {
                SetSubArrayStrict(Index_Customizations, value);
            }
        }

        public int SaveVersion
        {
            get
            {
                return GetPropertyStrict<int>(Index_SaveVersion);
            }
            set
            {
                SetPropertyStrict(Index_SaveVersion, value);
            }
        }

        public string CraftedBy
        {
            get
            {
                return GetPropertyStrict<string>(Index_CraftedBy);
            }
            set
            {
                SetPropertyStrict(Index_CraftedBy, value);
            }
        }

        public string ItemSchool
        {
            get
            {
                return GetPropertyStrict<string>(Index_ItemSchool);
            }
            set
            {
                SetPropertyStrict(Index_ItemSchool, value);
            }
        }

        public string GivenName
        {
            get
            {
                return GetPropertyStrict<string>(Index_GivenName);
            }
            set
            {
                SetPropertyStrict(Index_GivenName, value);
            }
        }

        public string ID
        {
            get
            {
                return GetPropertyStrict<string>(Index_ID);
            }
            set
            {
                SetPropertyStrict(Index_ID, value);
            }
        }

        public string OwningPlayerID
        {
            get
            {
                return GetPropertyStrict<string>(Index_OwningPlayerID);
            }
            set
            {
                SetPropertyStrict(Index_OwningPlayerID, value);
            }
        }

        public string OriginLobbyID
        {
            get
            {
                return GetPropertyStrict<string>(Index_OriginLobbyID);
            }
            set
            {
                SetPropertyStrict(Index_OriginLobbyID, value);
            }
        }

        public void SetNewIdentity()
        {
            SetPropertyStrict(Index_ID, Guid.NewGuid().ToString("D"));
        }

        public void ResetActivePips()
        {
            object[] modules = Modules;
            if (modules != null)
            {
                foreach (object o in modules)
                {
                    if (o != null)
                    {
                        Module m = new Module(o, modules);
                        m.ActivePips = 1;
                    }
                }
            }
        }

        private static object New()
        {
            MajorItemBlueprint blueprint = new MajorItemBlueprint(new object[ExpectedElementCount], null);
            blueprint.SetPropertyStrict(Index_RawType, string.Empty);
            blueprint.SetPropertyStrict(Index_Level, (byte)1);
            blueprint.SetPropertyStrict(Index_Rarity, (byte)0);
            blueprint.SetPropertyStrict(Index_Modules, new object[0]);
            blueprint.SetPropertyStrict(Index_Customizations, new object[0]);
            blueprint.SetPropertyStrict(5, (uint)1342734106); // unknown, always in this ballpark
            blueprint.SetPropertyStrict(6, (byte)5); // unknown constant
            blueprint.SetPropertyStrict(7, Guid.NewGuid().ToString("D")); // unknown, always unique
            blueprint.SetPropertyStrict(Index_SaveVersion, (byte)0); // same for all modules in a given save, must be copied from SaveMetadata.SaveVersion
            blueprint.SetPropertyStrict(Index_CraftedBy, null);
            blueprint.SetPropertyStrict(10, false); // unknown constant
            blueprint.SetPropertyStrict(Index_ItemSchool, "4c508dd3046b4cb4b8bd5a0e24877f12"); // irrlevant once the item is spawned
            blueprint.SetPropertyStrict(Index_GivenName, null);
            blueprint.SetNewIdentity(); // ID
            blueprint.SetPropertyStrict(Index_OwningPlayerID, string.Empty); // same for all modules in a given save, must be copied from SaveMetadata.PlayerID
            blueprint.SetPropertyStrict(Index_OriginLobbyID, "Tv2HrDk8AJM5rEKi");
            return blueprint.Root;
        }

        private const int Index_RawType = 0;
        private const int Index_Level = 1;
        private const int Index_Rarity = 2;
        private const int Index_Modules = 3;
        private const int Index_Customizations = 4;
        private const int Index_SaveVersion = 8;
        private const int Index_CraftedBy = 9;
        private const int Index_ItemSchool = 11;
        private const int Index_GivenName = 12;
        private const int Index_ID = 13;
        private const int Index_OwningPlayerID = 14;
        private const int Index_OriginLobbyID = 15;
        private const int ExpectedElementCount = 16;
    }

    public abstract class MajorItem : ArrayBasedObject
    {
        public MajorItem(object o, object[] parent) : base(o, parent)
        {
        }

        public string TypeName
        {
            get
            {
                return MajorItemType.GetTitle(MajorItemType.FromRaw(Blueprint?.RawType, MajorItemCategory.FromRaw(RawCategory)));
            }
        }

        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(Blueprint?.GivenName) ? TypeName : Blueprint.GivenName;
            }
        }

        public abstract string RawCategory { get; set; }

        public abstract MajorItemBlueprint Blueprint { get; set; }
    }

    public class StoredMajorItem : MajorItem
    {
        public StoredMajorItem() : this(New(), null)
        {
        }

        public StoredMajorItem(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in StoredMajorItem, found {Root.Length}.");
            }
        }

        public static StoredMajorItem FromRecent(RecentMajorItem recent)
        {
            StoredMajorItem stored = new StoredMajorItem();
            stored.RawCategory = recent.RawCategory;
            stored.Blueprint = recent.Blueprint.Clone();
            return stored;
        }

        public static StoredMajorItem FromLibrary(LibraryMajorItem library)
        {
            return FromRecent(library);
        }

        public new StoredMajorItem Clone()
        {
            return new StoredMajorItem(Bytes, null);
        }

        public override string RawCategory
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

        public override MajorItemBlueprint Blueprint
        {
            get
            {
                return new MajorItemBlueprint(GetSubObjectStrict(Index_Blueprint), Root);
            }
            set
            {
                SetSubObjectStrict(Index_Blueprint, value.Root);
                BlueprintID = value?.ID ?? string.Empty;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return new DateTime(GetPropertyStrict<long>(Index_Timestamp));
            }
            set
            {
                SetPropertyStrict(Index_Timestamp, value.Ticks);
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

        public string BlueprintID
        {
            get
            {
                return GetPropertyStrict<string>(Index_BlueprintID);
            }
            set
            {
                SetPropertyStrict(Index_BlueprintID, value);
            }
        }

        public bool HasEverBeenStored
        {
            get
            {
                return GetPropertyStrict<bool>(Index_HasEverBeenStored);
            }
            set
            {
                SetPropertyStrict(Index_HasEverBeenStored, value);
            }
        }

        private static object New()
        {
            MajorItemBlueprint blueprint = new MajorItemBlueprint();
            StoredMajorItem item = new StoredMajorItem(new object[ExpectedElementCount], null);
            item.SetPropertyStrict(Index_RawCategory, string.Empty);
            item.SetPropertyStrict(Index_Blueprint, blueprint.Root);
            item.SetPropertyStrict(Index_Timestamp, (ulong)DateTime.Now.Ticks);
            item.SetPropertyStrict(Index_PlacementInCategory, (byte)0);
            item.SetPropertyStrict(Index_BlueprintID, blueprint.ID);
            item.SetPropertyStrict(Index_HasEverBeenStored, true);
            return item.Root;
        }

        private const int Index_RawCategory = 0;
        private const int Index_Blueprint = 1;
        private const int Index_Timestamp = 2;
        private const int Index_PlacementInCategory = 3;
        private const int Index_BlueprintID = 4;
        private const int Index_HasEverBeenStored = 5;
        private const int ExpectedElementCount = 6;
    }

    public class RecentMajorItem : MajorItem
    {
        public RecentMajorItem() : this(New(), null)
        {
        }

        public RecentMajorItem(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in RecentMajorItem, found {Root.Length}.");
            }
        }

        public static RecentMajorItem FromLibrary(LibraryMajorItem lib)
        {
            return new RecentMajorItem(lib.Clone().Root, null);
        }

        public static RecentMajorItem FromStored(StoredMajorItem stored)
        {
            RecentMajorItem recent = new RecentMajorItem();
            recent.RawCategory = stored.RawCategory;
            recent.Blueprint = stored.Blueprint.Clone();
            return recent;
        }

        public new RecentMajorItem Clone()
        {
            return new RecentMajorItem(Bytes, null);
        }

        public override string RawCategory
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

        public override MajorItemBlueprint Blueprint
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

        public DateTime Timestamp
        {
            get
            {
                return new DateTime(GetPropertyStrict<long>(Index_Timestamp));
            }
            set
            {
                SetPropertyStrict(Index_Timestamp, value.Ticks);
            }
        }

        public bool HasEverBeenStored
        {
            get
            {
                return GetPropertyStrict<bool>(Index_HasEverBeenStored);
            }
            set
            {
                SetPropertyStrict(Index_HasEverBeenStored, value);
            }
        }

        protected static object New()
        {
            RecentMajorItem item = new RecentMajorItem(new object[ExpectedElementCount], null);
            item.SetPropertyStrict(Index_RawCategory, string.Empty);
            item.SetPropertyStrict(Index_Blueprint, (new MajorItemBlueprint()).Root);
            item.SetPropertyStrict(Index_Timestamp, (ulong)DateTime.Now.Ticks);
            item.SetPropertyStrict(3, "efa565eb7189aa54fb0bcbc11e1b54f0"); // unknown identifier with a few distinct repeating values
            item.SetPropertyStrict(Index_HasEverBeenStored, false);
            return item.Root;
        }

        private const int Index_RawCategory = 0;
        private const int Index_Blueprint = 1;
        private const int Index_Timestamp = 2;
        private const int Index_HasEverBeenStored = 4;
        private const int ExpectedElementCount = 5;
    }

    // Library items use the recent item format.
    public class LibraryMajorItem : RecentMajorItem
    {
        public LibraryMajorItem() : base(New(), null)
        {
        }

        public LibraryMajorItem(object o) : base(o, null)
        {
        }

        public LibraryMajorItem(byte[] b) : base(Deserialize(b), null)
        {
        }

        public static LibraryMajorItem FromRecent(RecentMajorItem recent)
        {
            return new LibraryMajorItem(recent.Clone().Root);
        }

        public static new LibraryMajorItem FromStored(StoredMajorItem stored)
        {
            return new LibraryMajorItem(RecentMajorItem.FromStored(stored).Root);
        }

        public new LibraryMajorItem Clone()
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

    public class MajorItemFactory
    {
        public MajorItemFactory(ISaveMetadata metadata)
        {
            metadata_ = metadata;
            if (metadata_ == null)
            {
                throw new ArgumentNullException("MajorItemFactory created with invalid SaveMetadata");
            }
        }

        public StoredMajorItem CreateStored()
        {
            StoredMajorItem item = new StoredMajorItem();
            FixBlueprint(item.Blueprint);
            return item;
        }

        public RecentMajorItem CreateRecent()
        {
            RecentMajorItem item = new RecentMajorItem();
            FixBlueprint(item.Blueprint);
            return item;
        }

        public LibraryMajorItem CreateLibrary()
        {
            return new LibraryMajorItem(CreateRecent().Root);
        }

        private void FixBlueprint(MajorItemBlueprint blueprint)
        {
            blueprint.OwningPlayerID = metadata_.PlayerID;
            blueprint.SaveVersion = metadata_.SaveVersion;
        }

        private ISaveMetadata metadata_;
    }
}
