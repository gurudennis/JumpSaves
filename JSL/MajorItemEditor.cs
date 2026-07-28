using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace JSL
{
    public enum CloneIdentity
    {
        Same,
        New
    }

    // Any module of a major item
    public class ModuleEditor : Editor
    {
        internal ModuleEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
            module_ = new Module();
            IsOrphaned = true;
        }

        internal ModuleEditor(object module, object[] parent, IRootEditor rootEditor)
            : base(rootEditor)
        {
            module_ = new Module(module, parent);
        }

        public Rarity Rarity
        {
            get
            {
                return module_.Rarity;
            }
            set
            {
                if (module_.Rarity != value)
                {
                    module_.Rarity = value;
                    SetDirtyIfNecessary();
                }
            }
        }

        public ModuleKind Kind
        {
            get
            {
                if (kind_ == null)
                {
                    kind_ = ModuleType.GetKindFromRaw(module_.RawType);
                }

                return kind_ ?? ModuleKind.Unknown;
            }
        }

        public string RawType
        {
            get
            {
                return module_.RawType;
            }
            set
            {
                if (module_.RawType != value)
                {
                    module_.RawType = value;

                    // Reset the cache
                    kind_ = null;
                    typeName_ = null;
                    typeAbbreviation_ = null;

                    SetDirtyIfNecessary();
                }
            }
        }

        public string TypeName
        {
            get
            {
                if (typeName_ == null)
                {
                    typeName_ = ModuleType.GetTitleFromRaw(module_.RawType);
                }

                return typeName_;
            }
        }

        public string TypeAbbreviation
        {
            get
            {
                if (typeAbbreviation_ == null)
                {
                    typeAbbreviation_ = ModuleType.GetAbbreviationFromRaw(module_.RawType);
                }

                return typeAbbreviation_;
            }
        }

        public string JSON
        {
            get
            {
                return SaveState.JSONFromObject(module_.Root);
            }
        }

        public static int MaxTheoreticalPotencyCount
        {
            get
            {
                return 3;
            }
        }

        public int? ExpectedPotencyCount
        {
            get
            {
                return ModuleType.GetMaxPotencyCountFromRaw(RawType);
            }
        }

        public double[] Potencies
        {
            get
            {
                return module_.Potencies;
            }
        }

        public void SetPotency(int index, double? value)
        {
            if (index >= MaxTheoreticalPotencyCount)
            {
                throw new ArgumentOutOfRangeException($"A module can't have more than {MaxTheoreticalPotencyCount} potencies.");
            }
            else if (index > Potencies.Length)
            {
                throw new ArgumentOutOfRangeException($"Can't add potency #{index + 1} to a module with only {Potencies.Length} potencies so far.");
            }

            if (value != null && (value <= 0.0 || value >= 1.0))
            {
                throw new ArgumentOutOfRangeException("Potency value out of acceptable range");
            }

            if (index < Potencies.Length)
            {
                if (value == null)
                {
                    // Remove an existing element
                    List<double> potencies = new List<double>();
                    for (int i = 0; i < Potencies.Length; ++i)
                    {
                        if (i != index)
                        {
                            potencies.Add(Potencies[i]);
                        }
                    }
                    module_.Potencies = potencies.ToArray();
                }
                else
                {
                    // Set an existing element
                    double[] potencies = new double[Potencies.Length];
                    for (int i = 0; i < Potencies.Length; ++i)
                    {
                        potencies[i] = Potencies[i];
                    }
                    potencies[index] = (double)value;
                    module_.Potencies = potencies;
                }
            }
            else if (index == Potencies.Length)
            {
                // Remove a nonexistent element?
                if (value == null)
                {
                    return; // nothing to do here
                }

                // Add a new element
                double[] potencies = new double[index + 1];
                for (int i = 0; i < Potencies.Length; ++i)
                {
                    potencies[i] = Potencies[i];
                }
                potencies[index] = (double)value;
                module_.Potencies = potencies;
            }

            SetDirtyIfNecessary();
        }

        public void SetExpectedPotencyCount()
        {
            int? expectedPotencyCount = ExpectedPotencyCount;
            if (expectedPotencyCount == null || expectedPotencyCount == Potencies.Length)
            {
                return;
            }

            List<double> potencies = new List<double>();
            for (int i = 0; i < expectedPotencyCount; ++i)
            {
                if (i < Potencies.Length)
                {
                    potencies.Add(Potencies[i]);
                }
                else
                {
                    potencies.Add(0.01);
                }
            }

            module_.Potencies = potencies.ToArray();

            SetDirtyIfNecessary();
        }

        internal Module Module
        {
            get
            {
                return module_;
            }
        }

        private Module module_;
        private ModuleKind? kind_ = null;
        private string typeName_;
        private string typeAbbreviation_;
    }

    public class PlayerWeaponCustomizationsEditor : Editor
    {
        internal PlayerWeaponCustomizationsEditor(object customizations, object[] parent, IRootEditor rootEditor)
            : base(rootEditor)
        {
            list_ = new ArrayBasedObject(customizations, parent);
            scope_ = EnsureExists(PlayerWeaponCustomizationType.Category.Scope);
            color_ = EnsureExists(PlayerWeaponCustomizationType.Category.Color);
        }

        public PlayerWeaponCustomizationType.Enum Scope
        {
            get
            {
                PlayerWeaponCustomizationType.Enum value = PlayerWeaponCustomizationType.FromRaw(scope_.RawType);
                Debug.Assert(value == PlayerWeaponCustomizationType.Enum.Unknown || PlayerWeaponCustomizationType.GetCategory(value) == PlayerWeaponCustomizationType.Category.Scope);
                return value;
            }
            set
            {
                if (value != PlayerWeaponCustomizationType.Enum.Unknown && value != Scope)
                {
                    Debug.Assert(PlayerWeaponCustomizationType.GetCategory(value) == PlayerWeaponCustomizationType.Category.Scope);
                    scope_.RawType = PlayerWeaponCustomizationType.GetRaw(value);
                    SetDirtyIfNecessary();
                }
            }
        }

        public PlayerWeaponCustomizationType.Enum Color
        {
            get
            {
                PlayerWeaponCustomizationType.Enum value = PlayerWeaponCustomizationType.FromRaw(color_.RawType);
                Debug.Assert(value == PlayerWeaponCustomizationType.Enum.Unknown || PlayerWeaponCustomizationType.GetCategory(value) == PlayerWeaponCustomizationType.Category.Color);
                return value;
            }
            set
            {
                if (value != PlayerWeaponCustomizationType.Enum.Unknown && value != Color)
                {
                    Debug.Assert(PlayerWeaponCustomizationType.GetCategory(value) == PlayerWeaponCustomizationType.Category.Color);
                    color_.RawType = PlayerWeaponCustomizationType.GetRaw(value);
                    SetDirtyIfNecessary();
                }
            }
        }

        private Customization EnsureExists(PlayerWeaponCustomizationType.Category category)
        {
            Customization customization = list_.Root.Select((o) => new Customization(o, list_.Root)).FirstOrDefault((c) => c.RawCategory == (int)category);
            if (customization == null)
            {
                customization = new Customization();
                customization.RawCategory = (int)category;
                list_.InsertProperty(list_.Root.Length, customization.Root);
            }

            return customization;
        }

        private ArrayBasedObject list_;
        private Customization scope_;
        private Customization color_;
    }

    // Any major item
    public abstract class MajorItemEditor : Editor
    {
        protected MajorItemEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
            IsOrphaned = true;
        }

        protected MajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(rootEditor)
        {
            Item = item;
        }

        public abstract MajorItemEditor Clone(CloneIdentity identity);

        public abstract string SelfDesignation { get; }

        public string RawType
        {
            get
            {
                return Item.Blueprint.RawType;
            }
            set
            {
                if (Item.Blueprint.RawType != value)
                {
                    Item.Blueprint.RawType = value;

                    // If the item can't have customizations, or has them as null, then reset to an empty array
                    if (!MajorItemType.HasCustomizations(Type) || Item.Blueprint.Customizations == null)
                    {
                        Item.Blueprint.Customizations = new object[0];
                    }

                    SetDirtyIfNecessary();
                }
            }
        }

        public MajorItemType.Enum Type
        {
            get
            {
                return MajorItemType.FromRaw(Item.Blueprint.RawType, Category);
            }
            set
            {
                RawType = MajorItemType.GetRaw(value);
            }
        }

        public string TypeName
        {
            get
            {
                return MajorItemType.GetTitle(MajorItemType.FromRaw(Item.Blueprint.RawType, Category));
            }
        }

        public string GivenName
        {
            get
            {
                return Item.Blueprint.Name;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = null; // shouldn't assign an empty string
                }

                if (GivenName != value)
                {
                    Item.Blueprint.Name = value;
                    SetDirtyIfNecessary();
                }
            }
        }

        public string Name
        {
            get
            {
                string name = Item.Blueprint.Name;
                if (string.IsNullOrEmpty(name))
                {
                    return TypeName;
                }
                else
                {
                    return name;
                }
            }
            set
            {
                if (Name != value) // note that we are comparing with the default name here if one is not explicitly assigned
                {
                    GivenName = value;
                }
            }
        }

        public MajorItemCategory.Enum Category
        {
            get
            {
                return MajorItemCategory.FromRaw(Item.RawCategory);
            }
            set
            {
                if (value == MajorItemCategory.Enum.Unknown && Category != MajorItemCategory.Enum.Unknown)
                {
                    throw new ArgumentException("Can't set major item category to Unknown");
                }

                string raw = MajorItemCategory.GetRaw(value);
                if (Item.RawCategory != raw)
                {
                    Item.RawCategory = raw;
                    SetDirtyIfNecessary();
                }
            }
        }

        public string RawCategory
        {
            get
            {
                return Item.RawCategory;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Can't set raw category to an empty or null string");
                }

                if (Item.RawCategory != value)
                {
                    Item.RawCategory = value;
                    SetDirtyIfNecessary();
                }
            }
        }

        public abstract long PlacementInCategory { get; }

        public Rarity Rarity
        {
            get
            {
                return Item.Blueprint.Rarity;
            }
            set
            {
                if (Item.Blueprint.Rarity != value)
                {
                    Item.Blueprint.Rarity = value;
                    SetDirtyIfNecessary();
                }
            }
        }

        public int Level
        {
            get
            {
                return Item.Blueprint.Level;
            }
            set
            {
                if (Item.Blueprint.Level != value)
                {
                    Item.Blueprint.Level = value;
                    SetDirtyIfNecessary();
                }
            }
        }

        public int ModuleCount
        {
            get
            {
                return Modules.Length;
            }
        }

        public int MaxModuleCount
        {
            get
            {
                return Item.Blueprint.MaxModules;
            }
        }

        public ModuleEditor GetModule(int index)
        {
            if (index < 0 || index > Modules.Length)
            {
                return null;
            }

            return Modules[index];
        }

        public void AddModule(ModuleEditor module)
        {
            if (Item.Blueprint.Modules.Length >= Item.Blueprint.MaxModules)
            {
                throw new InvalidOperationException($"Adding this module would exceed the maximum of {Item.Blueprint.MaxModules} modules for this item.");
            }

            ArrayBasedObject moduleList = new ArrayBasedObject(Item.Blueprint.Modules, Item.Blueprint.Root);
            moduleList.InsertProperty(Item.Blueprint.Modules.Length, module.Module.Root);
            modules_ = null; // clear the module cache

            SetDirtyIfNecessary();
        }

        public void RemoveModule(int index)
        {
            ModuleEditor module = Modules[index];
            ArrayBasedObject moduleList = new ArrayBasedObject(Item.Blueprint.Modules, Item.Blueprint.Root);
            moduleList.RemovePropertyStrict(Array.IndexOf(moduleList.Root, module.Module.Root));
            modules_ = null; // clear the module cache

            SetDirtyIfNecessary();
        }

        public ModuleEditor NewModule()
        {
            return new ModuleEditor(RootEditor);
        }

        public PlayerWeaponCustomizationsEditor PlayerWeaponCustomizations
        {
            get
            {
                if (MajorItemType.HasCustomizations(Type))
                {
                    if (playerWeaponCustomizationsEditor_ == null)
                    {
                        playerWeaponCustomizationsEditor_ = new PlayerWeaponCustomizationsEditor(Item.Blueprint.Customizations, Item.Blueprint.Root, RootEditor);
                    }
                }
                else
                {
                    playerWeaponCustomizationsEditor_ = null;
                }

                return playerWeaponCustomizationsEditor_;
            }
        }

        public void ResetActivePips()
        {
            Item.Blueprint.ResetActivePips();
        }

        public string JSON
        {
            get
            {
                return SaveState.JSONFromObject(Item.Root);
            }
        }

        internal MajorItem Item { get; set; }

        private ModuleEditor[] Modules
        {
            get
            {
                if (modules_ == null)
                {
                    modules_ = new ModuleEditor[Item.Blueprint.Modules.Length];
                    for (int i = 0; i < modules_.Length; ++i)
                    {
                        modules_[i] = new ModuleEditor(Item.Blueprint.Modules[i], Item.Blueprint.Modules, RootEditor);
                        modules_[i].IsOrphaned = IsOrphaned;
                    }

                    // Sort such that ModuleKind.Feature comes first,
                    // ModuleKind.Unknown comes last, and otherwise
                    // by rarity.
                    Array.Sort(modules_, (l, r) =>
                    {
                        int rarityComp =  l.Rarity.CompareTo(r.Rarity) * -1;
                        if (rarityComp != 0)
                        {
                            return rarityComp; // features are always the highest rarity possible for a component anyway
                        }

                        if (l.Kind == ModuleKind.Unknown && r.Kind == ModuleKind.Unknown)
                        {
                            return 0;
                        }
                        else if (l.Kind == ModuleKind.Unknown)
                        {
                            return 1;
                        }
                        else if (r.Kind == ModuleKind.Unknown)
                        {
                            return -1;
                        }

                        return l.Kind.CompareTo(r.Kind);
                    });
                }

                return modules_;
            }
        }

        private ModuleEditor[] modules_;
        private PlayerWeaponCustomizationsEditor playerWeaponCustomizationsEditor_;
    }

    // Stored major item
    internal class StoredMajorItemEditor : MajorItemEditor
    {
        internal StoredMajorItemEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
            Item = new MajorItemFactory(RootEditor.SaveMetadata).CreateStored();
            IsOrphaned = true;
        }

        internal StoredMajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }

        public override MajorItemEditor Clone(CloneIdentity identity)
        {
            StoredMajorItemEditor e = new StoredMajorItemEditor(new StoredMajorItem(Item.Clone().Root, null), RootEditor);
            if (identity == CloneIdentity.New)
            {
                e.Item.Blueprint.SetNewIdentity();
                ((StoredMajorItem)e.Item).BlueprintID = e.Item.Blueprint.ID;
            }
            e.IsOrphaned = true;
            return e;
        }

        public override string SelfDesignation
        {
            get
            {
                return "Stored";
            }
        }

        public override long PlacementInCategory
        {
            get
            {
                return ((StoredMajorItem)Item).PlacementInCategory;
            }
        }
    }

    // Recent major item
    internal class RecentMajorItemEditor : MajorItemEditor
    {
        internal RecentMajorItemEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
            Item = new MajorItemFactory(RootEditor.SaveMetadata).CreateRecent();
            IsOrphaned = true;
        }

        internal RecentMajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }

        public override MajorItemEditor Clone(CloneIdentity identity)
        {
            RecentMajorItemEditor e = new RecentMajorItemEditor(new RecentMajorItem(Item.Clone().Root, null), RootEditor);
            if (identity == CloneIdentity.New)
            {
                e.Item.Blueprint.SetNewIdentity();
            }
            e.IsOrphaned = true;
            return e;
        }

        public override string SelfDesignation
        {
            get
            {
                return "Recent";
            }
        }

        public override long PlacementInCategory
        {
            get
            {
                // Not technically correct because the values will be sparse, but OK for relative ordering.
                return -((RecentMajorItem)Item).Timestamp.Ticks;
            }
        }
    }

    // Library major item
    internal class LibraryMajorItemEditor : MajorItemEditor
    {
        internal LibraryMajorItemEditor(Library library, IRootEditor rootEditor)
            : base(rootEditor)
        {
            library_ = library;
            Item = new MajorItemFactory(RootEditor.SaveMetadata).CreateLibrary();
            IsOrphaned = true;
        }

        internal LibraryMajorItemEditor(Library library, int index, IRootEditor rootEditor)
            : base(library.Entries[index].Item, rootEditor)
        {
            library_ = library;
        }

        internal LibraryMajorItemEditor(Library library, LibraryMajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
            library_ = library;
        }

        public override MajorItemEditor Clone(CloneIdentity identity)
        {
            LibraryMajorItemEditor e = new LibraryMajorItemEditor(library_, new LibraryMajorItem(Item.Clone().Root), RootEditor);
            if (identity == CloneIdentity.New)
            {
                e.Item.Blueprint.SetNewIdentity();
            }
            e.IsOrphaned = true;
            return e;
        }

        public override string SelfDesignation
        {
            get
            {
                return "Library";
            }
        }

        public override long PlacementInCategory
        {
            get
            {
                return -1; // intentionally unused
            }
        }

        private Library library_;
    }

    // Any list of major items
    public abstract class MajorItemListEditor : Editor
    {
        protected MajorItemListEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
        }

        public abstract string SelfDesignation { get; }

        public abstract int Count { get; }

        public abstract MajorItemEditor this[int index] { get; }

        public abstract MajorItemEditor New();

        public abstract bool Add(MajorItemEditor item, ConflictBehavior onConflict);

        public abstract void Remove(int index);

        public void Remove(MajorItemEditor item)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (this[i].Item.Root == item.Item.Root)
                {
                    Remove(i);
                    return;
                }
            }
        }

#if DEBUG
        public string VerifyConstants()
        {
            string report = string.Empty;
            for (int i = 0; i < Count; ++i)
            {
                MajorItemEditor item = this[i];
                for (int q = 0; q < item.ModuleCount; ++q)
                {
                    ModuleEditor module = item.GetModule(q);
                    if (module.ExpectedPotencyCount != null && module.Potencies.Length != module.ExpectedPotencyCount)
                    {
                        // Known exceptions:
                        if (module.RawType == ShipModuleType.GetRaw(ShipModuleType.Enum.C_ChanceToChainEnemies) && module.Potencies.Length == 2)
                        {
                            continue; // Some "Chance to chain" modules have 3 potencies while others have two
                        }

                        report += $"{SelfDesignation} item \"{item.TypeName ?? "Unknown"}\" module \"{module.TypeName ?? "Unknown"}\" expected {module.ExpectedPotencyCount} potencies, found {module.Potencies.Length}.\n";
                    }
                }
            }

            return report;
        }
#endif

        protected IReadOnlyList<MajorItemEditor> GetItemsInCategory(MajorItemCategory.Enum category)
        {
            string rawCategory = MajorItemCategory.GetRaw(category);
            List<MajorItemEditor> items = new List<MajorItemEditor>();
            for (int i = 0; i < Count; ++i)
            {
                MajorItemEditor item = this[i];
                if (item.RawCategory == rawCategory)
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }

    // List of Stored or Recent major items (they have a lot in common
    // because both ultimately come from a safe file).
    internal abstract class MajorSaveFileItemListEditor : MajorItemListEditor
    {
        protected MajorSaveFileItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(rootEditor)
        {
            State = state;
        }

        protected abstract object[] Items { get; }

        protected ArrayBasedObject ItemsArr
        {
            get
            {
                return new ArrayBasedObject(Items, State.Root);
            }
        }

        protected SaveState State { get; private set; }
    }

    // List of Stored major items
    internal class StoredMajorItemListEditor : MajorSaveFileItemListEditor
    {
        internal StoredMajorItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(state, rootEditor)
        {
        }

        public override string SelfDesignation
        {
            get
            {
                return "Stored";
            }
        }

        public override int Count
        {
            get
            {
                return Items.Length;
            }
        }

        public override MajorItemEditor this[int index]
        {
            get
            {
                return new StoredMajorItemEditor(new StoredMajorItem(Items[index], Items), RootEditor);
            }
        }

        public override MajorItemEditor New()
        {
            return new StoredMajorItemEditor(RootEditor);
        }

        public override bool Add(MajorItemEditor item, ConflictBehavior onConflict)
        {
            StoredMajorItem storedItem = null;
            if (item.GetType() == typeof(LibraryMajorItemEditor))
            {
                storedItem = StoredMajorItem.FromLibrary((LibraryMajorItem)item.Item);
            }
            else if (item.GetType() == typeof(StoredMajorItemEditor))
            {
                storedItem = (StoredMajorItem)item.Item;
            }
            else if (item.GetType() == typeof(RecentMajorItemEditor))
            {
                storedItem = StoredMajorItem.FromRecent((RecentMajorItem)item.Item);
            }
            else
            {
                throw new ArgumentException($"Item type {item.GetType().FullName} cannot be added to stored items");
            }

            storedItem.PlacementInCategory = ObtainSpareSlot(item.Category);

            ItemsArr.InsertProperty(Items.Length, storedItem.Root);

            SetDirtyIfNecessary();

            return true;
        }

        public override void Remove(int index)
        {
            ItemsArr.RemoveProperty(index);

            SetDirtyIfNecessary();
        }

        protected override object[] Items
        {
            get
            {
                return State.StoredMajorItems;
            }
        }

        private int ObtainSpareSlot(MajorItemCategory.Enum category)
        {
            IReadOnlyList<MajorItemEditor> items = GetItemsInCategory(category);

            // Ensure that there are enough slots
            int needMaxSlots = items.Count + 1;
            int curMaxSlots = 0;
            {
                curMaxSlots = RootEditor.MajorItemSlotLimits.GetMaxMajorItemSlots(category);
                if (needMaxSlots > curMaxSlots)
                {
                    if (needMaxSlots > RootEditor.MajorItemSlotLimits.DefaultMaxSlotCount)
                    {
                        throw new Exception($"Can't auto-expand stored slots in category \"{MajorItemCategory.GetTitle(category)}\" beyond {RootEditor.MajorItemSlotLimits.DefaultMaxSlotCount}.");
                    }

                    RootEditor.MajorItemSlotLimits.SetMaxMajorItemSlots(category, needMaxSlots);
                    curMaxSlots = needMaxSlots;
                }
            }

            // Find an empty slot
            bool[] occupied = new bool[curMaxSlots];
            foreach (MajorItemEditor item in items)
            {
                occupied[item.PlacementInCategory] = true;
            }
            int spareSlotIndex = -1;
            for (int i = 0; i < occupied.Length; ++i)
            {
                if (!occupied[i])
                {
                    spareSlotIndex = i;
                    break;
                }
            }
            if (spareSlotIndex < 0)
            {
                throw new Exception($"Failed to find a spare slot in category \"{MajorItemCategory.GetTitle(category)}\" of {items.Count} items " +
                                    $"with a current maximum of {RootEditor.MajorItemSlotLimits.GetMaxMajorItemSlots(category)}.");
            }

            return spareSlotIndex;
        }
    }

    // List of Recent major items
    internal class RecentMajorItemListEditor : MajorSaveFileItemListEditor
    {
        internal RecentMajorItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(state, rootEditor)
        {
        }

        public override string SelfDesignation
        {
            get
            {
                return "Recent";
            }
        }

        public override int Count
        {
            get
            {
                return Items.Length;
            }
        }

        public override MajorItemEditor this[int index]
        {
            get
            {
                return new RecentMajorItemEditor(new RecentMajorItem(Items[index], Items), RootEditor);
            }
        }

        public override MajorItemEditor New()
        {
            return new RecentMajorItemEditor(RootEditor);
        }

        public override bool Add(MajorItemEditor item, ConflictBehavior onConflict)
        {
            if (item.GetType() == typeof(LibraryMajorItemEditor))
            {
                ItemsArr.InsertProperty(Items.Length, JSL.RecentMajorItem.FromLibrary((LibraryMajorItem)item.Item).Root);
            }
            else if (item.GetType() == typeof(StoredMajorItemEditor))
            {
                ItemsArr.InsertProperty(Items.Length, JSL.RecentMajorItem.FromStored((StoredMajorItem)item.Item).Root);
            }
            else if (item.GetType() == typeof(RecentMajorItemEditor))
            {
                ItemsArr.InsertProperty(Items.Length, item.Item.Root);
            }
            else
            {
                throw new ArgumentException($"Item type {item.GetType().FullName} cannot be added to recent items");
            }

            SetDirtyIfNecessary();

            return true;
        }

        public override void Remove(int index)
        {
            ItemsArr.RemoveProperty(index);

            SetDirtyIfNecessary();
        }

        protected override object[] Items
        {
            get
            {
                return State.RecentMajorItems;
            }
        }
    }

    // List of Library major items
    public class LibraryMajorItemListEditor : MajorItemListEditor
    {
        internal LibraryMajorItemListEditor(Library library, IRootEditor rootEditor)
            : base(rootEditor)
        {
            if (library == null)
            {
                throw new ArgumentNullException("Invalid library object");
            }

            library_ = library;
        }

        public override string SelfDesignation
        {
            get
            {
                return "Library";
            }
        }

        public string Path
        {
            get
            {
                return library_.Path;
            }
        }

        public override int Count
        {
            get
            {
                return library_.Entries.Count;
            }
        }

        public override MajorItemEditor this[int index]
        {
            get
            {
                return new LibraryMajorItemEditor(library_, index, RootEditor);
            }
        }

        public override MajorItemEditor New()
        {
            return new LibraryMajorItemEditor(library_, RootEditor);
        }

        public override bool Add(MajorItemEditor item, ConflictBehavior onConflict)
        {
            if (item.GetType() == typeof(LibraryMajorItemEditor))
            {
                return library_.AddEntry((LibraryMajorItem)item.Item, onConflict);
            }
            else if (item.GetType() == typeof(StoredMajorItemEditor))
            {
                return library_.AddEntry(JSL.LibraryMajorItem.FromStored((JSL.StoredMajorItem)item.Item), onConflict);
            }
            else if (item.GetType() == typeof(RecentMajorItemEditor))
            {
                return library_.AddEntry(JSL.LibraryMajorItem.FromRecent((JSL.RecentMajorItem)item.Item), onConflict);
            }
            else
            {
                throw new ArgumentException($"Item type {item.GetType().FullName} cannot be added to the library");
            }
        }

        public override void Remove(int index)
        {
            library_.RemoveEntry(index);
        }

        public void Reload()
        {
            library_.Reload();
        }

        public void Export(IReadOnlyCollection<MajorItemEditor> items, string path)
        {
            List<Library.Entry> entries = new List<Library.Entry>();
            foreach (MajorItemEditor item in items)
            {
                Library.Entry entry = library_.Entries.FirstOrDefault((e) => e.Item == (LibraryMajorItem)item.Item);
                if (entry == null)
                {
                    throw new Exception("At least one of the designated items can't be found in the Library");
                }

                entries.Add(entry);
            }

            library_.Export(entries, path);
        }

        public void Import(string path)
        {
            library_.Import(path);
        }

        public IReadOnlyList<string> TakeFailedFiles()
        {
            return library_.TakeFailedFiles();
        }

        private Library library_;
    }
}
