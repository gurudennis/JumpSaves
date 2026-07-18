using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
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

        public string Name
        {
            get
            {
                return Item.Blueprint.Name;
            }
            set
            {
                if (Item.Blueprint.Name != value)
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
                string raw = MajorItemCategory.GetRaw(value);
                if (Item.RawCategory != raw)
                {
                    Item.RawCategory = raw;
                    RootEditor.IsDirty = true;
                }
            }
        }

        public abstract int PlacementInCategory { get; }

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

        internal MajorItem Item { get; set; }
    }

    // Stored major item
    internal class StoredMajorItemEditor : MajorItemEditor
    {
        internal StoredMajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }

        public override int PlacementInCategory
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
        internal RecentMajorItemEditor(MajorItem item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }

        public override int PlacementInCategory
        {
            get
            {
                // Not technically correct because the values will be sparse, but OK for relative ordering.
                return (int)(((RecentMajorItem)Item).Timestamp.Ticks & 0x7FFFFFFFL);
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
            // Item = ... make a new one ...

        }

        internal LibraryMajorItemEditor(Library library, int index, IRootEditor rootEditor)
            : base(library.Entries[index].Item, rootEditor)
        {
            library_ = library;
            index_ = index;
        }

        public override int PlacementInCategory
        {
            get
            {
                // Not technically correct because the values will be sparse, but OK for relative ordering.
                return index_;
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
            // ...

            return null;
        }

        public override void Add(MajorItemEditor item)
        {
            if (item.GetType() != typeof(StoredMajorItemEditor))
            {
                throw new ArgumentException($"Expected to be adding {typeof(StoredMajorItemEditor).FullName}, received {item.GetType().FullName}");
            }

            // ...
        }

        public override void Remove(int index)
        {
            // ...
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
            // ...

            return null;
        }

        public override void Add(MajorItemEditor item)
        {
            if (item.GetType() != typeof(RecentMajorItemEditor))
            {
                throw new ArgumentException($"Expected to be adding {typeof(RecentMajorItemEditor).FullName}, received {item.GetType().FullName}");
            }

            // ...
        }

        public override void Remove(int index)
        {
            // ...
        }
    }

    // List of Library major items
    public class LibraryMajorItemListEditor : MajorItemListEditor
    {
        internal LibraryMajorItemListEditor(Library library)
            : base(null)
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

            library_.AddEntry((LibraryMajorItem)libraryItem.Item);
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
