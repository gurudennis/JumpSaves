using System;
using System.Collections.Generic;

namespace JSL
{
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
                    if (!IsOrphaned)
                    {
                        RootEditor.IsDirty = true;
                    }
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

        private Module module_;
        private ModuleKind? kind_ = null;
        private string typeName_;
        private string typeAbbreviation_;
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

        public string TypeName
        {
            get
            {
                return MajorItemType.GetTitleFromRaw(Item.Blueprint.RawType, Category);
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
                if (value == string.Empty)
                {
                    value = null; // shouldn't assign an empty string
                }

                if (Name != value) // note that we are comparing with the default name here if one is not explicitly assigned
                {
                    Item.Blueprint.Name = value;
                    if (!IsOrphaned)
                    {
                        RootEditor.IsDirty = true;
                    }
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
                if (Category == MajorItemCategory.Enum.Unknown)
                {
                    throw new ArgumentException("Can't set major item category to Unknown");
                }

                string raw = MajorItemCategory.GetRaw(value);
                if (Item.RawCategory != raw)
                {
                    Item.RawCategory = raw;
                    if (!IsOrphaned)
                    {
                        RootEditor.IsDirty = true;
                    }
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
                    if (!IsOrphaned)
                    {
                        RootEditor.IsDirty = true;
                    }
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
                    if (!IsOrphaned)
                    {
                        RootEditor.IsDirty = true;
                    }
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
                    if (!IsOrphaned)
                    {
                        RootEditor.IsDirty = true;
                    }
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
            throw new NotImplementedException("Adding modules is not yet implemented");
        }

        public void RemoveModule(int index)
        {
            throw new NotImplementedException("Removing modules is not yet implemented");
        }

        public ModuleEditor NewModule()
        {
            return new ModuleEditor(RootEditor);
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
    }

    // Stored major item
    internal class StoredMajorItemEditor : MajorItemEditor
    {
        internal StoredMajorItemEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
            Item = new MajorItemFactory(RootEditor.SaveMetadata).CreateStored();
        }

        internal StoredMajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
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
        }

        internal RecentMajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
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
        }

        internal LibraryMajorItemEditor(Library library, int index, IRootEditor rootEditor)
            : base(library.Entries[index].Item, rootEditor)
        {
            library_ = library;
            index_ = index;
        }

        public override long PlacementInCategory
        {
            get
            {
                return -1; // intentionally unused
            }
        }

        private int index_ = -1;
        private Library library_;
    }

    // Any list of major items
    public abstract class MajorItemListEditor : Editor
    {
        protected MajorItemListEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
        }

        public abstract int Count { get; }

        public abstract MajorItemEditor this[int index] { get; }

        public abstract MajorItemEditor New();

        public abstract void Add(MajorItemEditor item, ConflictBehavior onConflict);

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

        public override void Add(MajorItemEditor item, ConflictBehavior onConflict)
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

            if (!IsOrphaned)
            {
                RootEditor.IsDirty = true;
            }
        }

        public override void Remove(int index)
        {
            ItemsArr.RemoveProperty(index);

            if (!IsOrphaned)
            {
                RootEditor.IsDirty = true;
            }
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

        public override void Add(MajorItemEditor item, ConflictBehavior onConflict)
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

            if (!IsOrphaned)
            {
                RootEditor.IsDirty = true;
            }
        }

        public override void Remove(int index)
        {
            ItemsArr.RemoveProperty(index);

            if (!IsOrphaned)
            {
                RootEditor.IsDirty = true;
            }
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

        public override void Add(MajorItemEditor item, ConflictBehavior onConflict)
        {
            if (item.GetType() == typeof(LibraryMajorItemEditor))
            {
                library_.AddEntry((LibraryMajorItem)item.Item, onConflict);
            }
            else if (item.GetType() == typeof(StoredMajorItemEditor))
            {
                library_.AddEntry(JSL.LibraryMajorItem.FromStored((JSL.StoredMajorItem)item.Item), onConflict);
            }
            else if (item.GetType() == typeof(RecentMajorItemEditor))
            {
                library_.AddEntry(JSL.LibraryMajorItem.FromRecent((JSL.RecentMajorItem)item.Item), onConflict);
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

        public IReadOnlyList<string> TakeFailedFiles()
        {
            return library_.TakeFailedFiles();
        }

        private Library library_;
    }
}
