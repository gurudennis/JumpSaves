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

        protected MajorItemEditor(object item, IRootEditor rootEditor)
            : base(rootEditor)
        {
            Item = item;
        }

        protected object Item { get; set; }
    }

    // Stored major item
    internal class StoredMajorItemEditor : MajorItemEditor
    {
        internal StoredMajorItemEditor(object item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }
    }

    // Recent major item
    internal class RecentMajorItemEditor : MajorItemEditor
    {
        internal RecentMajorItemEditor(object item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }
    }

    // Library major item
    internal class LibraryMajorItemEditor : MajorItemEditor
    {
        internal LibraryMajorItemEditor(Library library, IRootEditor rootEditor)
            : base(rootEditor)
        {
            library_ = library;
            // LibraryItem = ... make a new one ...
            Item = LibraryItem.Root;

        }

        internal LibraryMajorItemEditor(Library library, int index, IRootEditor rootEditor)
            : base(library.Entries[index].Item.Root, rootEditor)
        {
            library_ = library;
            index_ = index;
            LibraryItem = library_.Entries[index_].Item;
        }

        internal LibraryMajorItem LibraryItem { get; private set; }

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
                // ...

                return null;
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
                // ...

                return null;
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
                // TODO: Can this be made more efficient? Caching seems complicated since
                //       each edit would have to invalidate only a given element.
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

            library_.AddEntry(libraryItem.LibraryItem);
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
