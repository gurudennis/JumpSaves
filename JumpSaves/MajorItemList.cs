using BrightIdeasSoftware;
using JSL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class MajorItemList : UserControl
    {
        public MajorItemList()
        {
            InitializeComponent();
        }

        public EventHandler<EventArgs> MaybeDirty;

        public JSL.SaveEditor SaveEditor
        {
            get
            {
                return saveEditor_;
            }
            set
            {
                if (saveEditor_ != value)
                {
                    saveEditor_ = value;
                    OnStateChange();
                }
            }
        }

        public JSL.LibraryMajorItemListEditor LibraryEditor
        {
            get
            {
                return libraryEditor_;
            }
            set
            {
                if (libraryEditor_ != value)
                {
                    libraryEditor_ = value;
                    OnStateChange();
                }
            }
        }

        public JSL.MajorItemListEditor Editor { get; private set; }

        public bool AllowCustomization
        {
            get
            {
                return allowCustomization_;
            }
            set
            {
                if (allowCustomization_ != value)
                {
                    allowCustomization_ = value;
                    OnStateChange();
                }
            }
        }

        private class Row
        {
            public Row(MajorItemEditor editor)
            {
                Editor = editor;
            }

            public string Name
            {
                get
                {
                    return String.IsNullOrEmpty(Editor.Name) ? "(unnamed)" : Editor.Name;
                }
                set
                {
                    Editor.Name = value;
                }
            }

            public MajorItemEditor Editor { get; private set; }
        }

        private void toolStripComboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editor = null;
            OnStateChange();
        }

        private bool IsLibraryEditor
        {
            get
            {
                return Editor == null || Editor.GetType() == typeof(JSL.LibraryMajorItemListEditor);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            list.RowGetter += GetRow;

            toolStripComboBoxFilter.SelectedIndex = 0;

            OnStateChange();
        }

        private object GetRow(int index)
        {
            return new Row(Editor[index]);
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            EnsureEditorAvailable();

            list.Enabled = Enabled;
            list.BackColor = Enabled ? Color.White : Color.Gainsboro;
            list.VirtualListSize = Editor?.Count ?? 0;
            list.Invalidate();

            toolStrip.Enabled = Enabled;
            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.Enabled = toolStrip.Enabled;
            }
            toolStripButtonAdd.Enabled &= AllowCustomization;
            toolStripButtonEdit.Enabled &= AllowCustomization;
            toolStripLabelFilter.Enabled = Enabled;
            toolStripComboBoxFilter.Enabled = Enabled;
            toolStripLabelFilter.Visible = !IsLibraryEditor;
            toolStripComboBoxFilter.Visible = !IsLibraryEditor;
        }

        private void EnsureEditorAvailable()
        {
            if (SaveEditor == null)
            {
                storedEditor_ = null;
                recentEditor_ = null;
            }

            if (LibraryEditor == null)
            {
                libraryEditor_ = null;
            }

            if (SaveEditor == null && LibraryEditor == null)
            {
                Editor = null;
                return;
            }

            if (Editor != null)
            {
                return;
            }

            if (LibraryEditor != null && SaveEditor != null)
            {
                throw new Exception("Can't edit the library and the save in the same control instance");
            }

            if (LibraryEditor != null)
            {
                Editor = LibraryEditor;
            }
            else if (SaveEditor != null)
            {
                if (storedEditor_ == null)
                {
                    storedEditor_ = SaveEditor.StoredMajorItems;
                }

                if (recentEditor_ == null)
                {
                    recentEditor_ = SaveEditor.RecentMajorItems;
                }

                Editor = toolStripComboBoxFilter.SelectedIndex == 0 ? storedEditor_ : recentEditor_;
            }
        }

        private JSL.SaveEditor saveEditor_;
        private JSL.LibraryMajorItemListEditor libraryEditor_;
        private JSL.MajorItemListEditor storedEditor_;
        private JSL.MajorItemListEditor recentEditor_;
        private bool allowCustomization_;
    }
}
