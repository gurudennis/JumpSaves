using BrightIdeasSoftware;
using JSL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static JumpSaves.MajorItemList;

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

        public JSL.MajorItemListEditor Editor
        {
            get
            {
                return editor_;
            }
            private set
            {
                if (editor_ != value)
                {
                    editor_ = value;
                    ApplyEditor(editor_);
                }
            }
        }

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

        public class Row
        {
            public Row(JSL.MajorItemEditor editor)
            {
                Editor = editor;
            }

            public static readonly string Unnamed = "(unnamed)";

            public string Name
            {
                get
                {
                    return String.IsNullOrEmpty(Editor.Name) ? Unnamed : Editor.Name;
                }
                set
                {
                    Editor.Name = (value == Unnamed) ? null : value;
                }
            }

            public string Category
            {
                get
                {
                    return JSL.MajorItemCategory.GetTitle(Editor.Category);
                }
            }

            public long SlotIndex
            {
                get
                {
                    return Editor.PlacementInCategory;
                }
            }

            public JSL.Rarity Rarity
            {
                get
                {
                    return Editor.Rarity;
                }
            }

            public int Level
            {
                get
                {
                    return Editor.Level;
                }
            }

            public JSL.MajorItemEditor Editor { get; private set; }
        }

        private class GroupComparer : IComparer<OLVGroup>
        {
            public int Compare(OLVGroup x, OLVGroup y)
            {
                JSL.MajorItemCategory.Enum xe = JSL.MajorItemCategory.FromTitle((string)x.Key);
                JSL.MajorItemCategory.Enum ye = JSL.MajorItemCategory.FromTitle((string)y.Key);

                if (xe == JSL.MajorItemCategory.Enum.Unknown && ye == JSL.MajorItemCategory.Enum.Unknown)
                {
                    return 0;
                }
                else if (xe == JSL.MajorItemCategory.Enum.Unknown)
                {
                    return 1;
                }
                else if (ye == JSL.MajorItemCategory.Enum.Unknown)
                {
                    return -1;
                }

                return ((int)xe).CompareTo((int)ye);
            }
        }

        private void list_FormatCell(object sender, FormatCellEventArgs e)
        {
            Row row = (Row)e.Model;

            if (e.Column == olvColumnName)
            {
                e.Item.Text = row.Name;
                e.Item.ForeColor = GetRarityColor(row.Rarity, true);
            }
            else if (e.Column == olvColumnModule1)
            {
                FormatModuleColumn(row, 0, e);
            }
            else if (e.Column == olvColumnModule2)
            {
                FormatModuleColumn(row, 1, e);
            }
            else if (e.Column == olvColumnModule3)
            {
                FormatModuleColumn(row, 2, e);
            }
            else if (e.Column == olvColumnModule4)
            {
                FormatModuleColumn(row, 3, e);
            }
            else if (e.Column == olvColumnModule5)
            {
                FormatModuleColumn(row, 4, e);
            }
        }

        private void list_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            Row row = (Row)e.Model;

            if (e.ModifierKeys.HasFlag(Keys.Alt)) // developer popup
            {
                if (e.Column == olvColumnName)
                {
                    e.Text = row.Editor.JSON;
                    return;
                }
            }

            if (e.Column == olvColumnModule1)
            {
                FormatModuleColumnTooltip(row, 0, e);
            }
            else if (e.Column == olvColumnModule2)
            {
                FormatModuleColumnTooltip(row, 1, e);
            }
            else if (e.Column == olvColumnModule3)
            {
                FormatModuleColumnTooltip(row, 2, e);
            }
            else if (e.Column == olvColumnModule4)
            {
                FormatModuleColumnTooltip(row, 3, e);
            }
            else if (e.Column == olvColumnModule5)
            {
                FormatModuleColumnTooltip(row, 4, e);
            }
        }

        private void list_BeforeCreatingGroups(object sender, CreateGroupsEventArgs e)
        {
            e.Parameters.SortItemsByPrimaryColumn = false;
            e.Parameters.PrimarySort = olvColumnSlotInCategory;
            e.Parameters.PrimarySortOrder = SortOrder.Ascending;
            e.Parameters.SecondarySort = olvColumnName;
            e.Parameters.SecondarySortOrder = SortOrder.Ascending;
            e.Parameters.GroupComparer = new GroupComparer();
        }

        private void toolStripComboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editor = null;
            OnStateChange();
        }

        private void FormatModuleColumn(Row row, int moduleIndex, FormatCellEventArgs e)
        {
            if (moduleIndex >= row.Editor.ModuleCount)
            {
                e.SubItem.Text = string.Empty;
                return;
            }

            JSL.ModuleEditor module = row.Editor.GetModule(moduleIndex);
            e.SubItem.Text = module?.TypeAbbreviation ?? "Unk";
            e.SubItem.ForeColor = GetRarityColor(module?.Rarity ?? Rarity.Unknown, true);
        }

        private void FormatModuleColumnTooltip(Row row, int moduleIndex, ToolTipShowingEventArgs e)
        {
            if (moduleIndex >= row.Editor.ModuleCount)
            {
                return;
            }

            JSL.ModuleEditor module = row.Editor.GetModule(moduleIndex);
            e.Text = module?.TypeName ?? "Unknown";
        }

        private bool IsLibraryEditor
        {
            get
            {
                return Editor == null || Editor.GetType() == typeof(JSL.LibraryMajorItemListEditor);
            }
        }

        private Color GetRarityColor(JSL.Rarity rarity, bool fore)
        {
            if (rarity == JSL.Rarity.Common)
            {
                return fore ? Color.FromArgb(30, 112, 0) : Color.FromArgb(217, 242, 208);
            }
            else if (rarity == JSL.Rarity.Uncommon)
            {
                return fore ? Color.FromArgb(0, 67, 112) : Color.FromArgb(193, 216, 247);
            }
            else if (rarity == JSL.Rarity.Rare)
            {
                return fore ? Color.FromArgb(62, 6, 153) : Color.FromArgb(205, 187, 250);
            }
            else if (rarity == JSL.Rarity.Superior)
            {
                return fore ? Color.FromArgb(181, 59, 7) : Color.FromArgb(240, 175, 175);
            }

            return fore ? Color.Black : Color.White;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            list.ShowGroups = true;
            list.AlwaysGroupByColumn = olvColumnCategory;

            toolStripComboBoxFilter.SelectedIndex = 0;

            OnStateChange();
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

        private void ApplyEditor(MajorItemListEditor editor)
        {
            rows_ = new List<Row>();

            if (editor != null)
            {
                for (int i = 0; i < editor.Count; ++i)
                {
                    rows_.Add(new Row(editor[i]));
                }
            }

            list.SetObjects(rows_);
            toolStripLabelTotal.Text = $"Total: {rows_.Count}";
        }

        private JSL.SaveEditor saveEditor_;
        private JSL.LibraryMajorItemListEditor libraryEditor_;
        private JSL.MajorItemListEditor storedEditor_;
        private JSL.MajorItemListEditor recentEditor_;
        private JSL.MajorItemListEditor editor_;
        private List<Row> rows_;
        private bool allowCustomization_;
    }
}
