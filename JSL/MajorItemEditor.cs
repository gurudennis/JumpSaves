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
        protected MajorItemEditor(object item, IRootEditor rootEditor)
            : base(rootEditor)
        {
            Item = item;
        }

        protected object Item { get; private set; }
    }

    // Stored major item
    internal class StoredMajorItemEditor : MajorItemEditor
    {
        protected StoredMajorItemEditor(object item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }
    }

    // Recent or Library major item (they have a lot in common because
    // the library uses the same item format internally).
    internal abstract class NonStoredMajorItemEditor : MajorItemEditor
    {
        protected NonStoredMajorItemEditor(object item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }
    }

    // Recent major item
    internal class RecentMajorItemEditor : NonStoredMajorItemEditor
    {
        protected RecentMajorItemEditor(object item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }
    }

    // Library major item
    internal class LibraryMajorItemEditor : NonStoredMajorItemEditor
    {
        protected LibraryMajorItemEditor(object item, IRootEditor rootEditor)
            : base(item, rootEditor)
        {
        }
    }

    // Any list of major items
    public abstract class MajorItemListEditor : Editor
    {
        protected MajorItemListEditor(IRootEditor rootEditor)
            : base(rootEditor)
        {
        }
    }

    // List of Stored or Recent major items (they have a lot in common
    // because both ultimately come from a safe file).
    internal abstract class MajorSaveFileItemListEditor : MajorItemListEditor
    {
        protected MajorSaveFileItemListEditor(object[] items, IRootEditor rootEditor)
            : base(rootEditor)
        {
            items_ = items;
        }

        private object[] items_;
    }

    // List of Stored major items
    internal class StoredMajorItemListEditor : MajorSaveFileItemListEditor
    {
        internal StoredMajorItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(state.StoredMajorItems, rootEditor)
        {
        }

        //public MajorItemEditor this[int index]
        //{
        //    get { }
        //    set { }
        //}
    }

    // List of Recent major items
    internal class RecentMajorItemListEditor : MajorSaveFileItemListEditor
    {
        internal RecentMajorItemListEditor(SaveState state, IRootEditor rootEditor)
            : base(state.RecentMajorItems, rootEditor)
        {
        }
    }

    // List of Library major items
    public class LibraryMajorItemListEditor : MajorItemListEditor
    {
        internal LibraryMajorItemListEditor(Library library)
            : base(null)
        {
        }

        private Library library_;
    }
}
