using MessagePack.Formatters;
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
                    RootEditor.IsDirty = true;
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
                if (Category == MajorItemCategory.Enum.PlayerWeapons)
                {
                    PlayerWeaponType.Enum type = PlayerWeaponType.FromRaw(Item.Blueprint.RawType);
                    if (type != PlayerWeaponType.Enum.Unknown)
                    {
                        return PlayerWeaponType.GetTitle(type);
                    }
                }

                return null; // unknown
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
                    RootEditor.IsDirty = true;
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
                    RootEditor.IsDirty = true;
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
                    RootEditor.IsDirty = true;
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
                    RootEditor.IsDirty = true;
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
                    RootEditor.IsDirty = true;
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

        public abstract void Add(MajorItemEditor item);

        public abstract void Remove(int index);
    }

    // List of Stored or Recent major items (they have a lot in common
    // because both ultimately come from a safe file).
    internal abstract class MajorSaveFileItemListEditor : MajorItemListEditor
    {
        protected MajorSaveFileItemListEditor(SaveState state, object[] items, IRootEditor rootEditor)
            : base(rootEditor)
        {
            State = state;
            items_ = items;
        }

        public override int Count
        {
            get
            {
                return items_.Length;
            }
        }

        protected object[] Items
        {
            get
            {
                return items_;
            }
        }

        protected SaveState State { get; private set; }

        private object[] items_;
    }

    // List of Stored major items
    internal class StoredMajorItemListEditor : MajorSaveFileItemListEditor
    {
        internal StoredMajorItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(state, state.StoredMajorItems, rootEditor)
        {
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

        public override void Add(MajorItemEditor item)
        {
            if (item.GetType() != typeof(StoredMajorItemEditor))
            {
                throw new ArgumentException($"Expected to be adding {typeof(StoredMajorItemEditor).FullName}, received {item.GetType().FullName}");
            }

            throw new NotImplementedException("Adding major items is not implemented yet");
        }

        public override void Remove(int index)
        {
            throw new NotImplementedException("Removing major items is not implemented yet");
        }
    }

    // List of Recent major items
    internal class RecentMajorItemListEditor : MajorSaveFileItemListEditor
    {
        internal RecentMajorItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(state, state.RecentMajorItems, rootEditor)
        {
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

        public override void Add(MajorItemEditor item)
        {
            if (item.GetType() != typeof(RecentMajorItemEditor))
            {
                throw new ArgumentException($"Expected to be adding {typeof(RecentMajorItemEditor).FullName}, received {item.GetType().FullName}");
            }

            throw new NotImplementedException("Adding major items is not implemented yet");
        }

        public override void Remove(int index)
        {
            throw new NotImplementedException("Removing major items is not implemented yet");
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

        public override void Add(MajorItemEditor item)
        {
            if (item.GetType() != typeof(LibraryMajorItemEditor))
            {
                throw new ArgumentException($"Expected to be adding {typeof(LibraryMajorItemEditor).FullName}, received {item.GetType().FullName}");
            }

            LibraryMajorItemEditor libraryItem = (LibraryMajorItemEditor)item;

            library_.AddEntry((LibraryMajorItem)libraryItem.Item, Library.ConflictBehavior.Error);
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
