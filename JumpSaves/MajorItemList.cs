using BrightIdeasSoftware;
using JSL;
using JumpSaves.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class MajorItemList : UserControl
    {
        public MajorItemList()
        {
            InitializeComponent();
        }

        public Action<MajorItemList, IReadOnlyList<JSL.MajorItemEditor>> TransferAction { get; set; }

        public Action<MajorItemList, Model.ActionLog.Level, string> LogAction { get; set; }

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

        public bool IsLibraryEditor
        {
            get
            {
                return Editor?.GetType() == typeof(JSL.LibraryMajorItemListEditor);
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

        public bool CanEdit
        {
            get
            {
                return canEdit_;
            }
            set
            {
                if (canEdit_ != value)
                {
                    canEdit_ = value;
                    OnStateChange();
                }
            }
        }

        public bool CanTransfer
        {
            get
            {
                return canTransfer_;
            }
            set
            {
                if (canTransfer_ != value)
                {
                    canTransfer_ = value;
                    OnStateChange();
                }
            }
        }

        public string SelfDesignation
        {
            get
            {
                if (Editor == null)
                {
                    return string.Empty;
                }
                else if (IsLibraryEditor)
                {
                    return "Library";
                }
                else if (Editor == storedEditor_)
                {
                    return "Stored";
                }
                else if (Editor == recentEditor_)
                {
                    return "Recent";
                }

                return string.Empty;
            }
        }

        public bool ShouldAutoAcquire
        {
            get
            {
                return toolStripComboBoxMonitor.SelectedIndex != 2;
            }
        }

        public bool IsInterestedInItem(JSL.MajorItemEditor item)
        {
            if (toolStripComboBoxMonitor.SelectedIndex == 0) // Superior only
            {
                return item.Rarity == JSL.Rarity.Superior;
            }
            else if (toolStripComboBoxMonitor.SelectedIndex == 2) // None
            {
                Debug.Assert(false); // we shouldn't even be here since acquisition is disabled
                return false;
            }

            return true;
        }

        public void Reload()
        {
            ApplyEditor(Editor);
        }

        private class Row
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
                e.Item.ForeColor = Style.GetRarityColor(row.Rarity, true);
                e.Item.SelectedForeColor = e.Item.ForeColor;
                e.Item.SelectedBackColor = Style.GetRarityColor(row.Rarity, false);
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
            e.ToolTipControl.ReshowDelay = 5000;

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

        private void list_CellClick(object sender, CellClickEventArgs e)
        {
            Row row = (Row)e.Model;

            if (e.ModifierKeys.HasFlag(Keys.Alt)) // developer popup
            {
                if (e.Column == olvColumnName)
                {
                    MessageBox.Show(this, row.Editor.JSON, "Developer mode: object dump", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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

        private void toolStripButtonTransfer_Click(object sender, EventArgs e)
        {
            IList selected = list.SelectedObjects;
            if (selected == null || selected.Count == 0)
            {
                string dest = IsLibraryEditor ? "save" : "library";
                MessageBox.Show(Parent, $"Select one or more items to transfer to the {dest}, then press this button.", "No item selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<JSL.MajorItemEditor> items = new List<JSL.MajorItemEditor>();
            foreach (object obj in selected)
            {
                Row r = (Row)obj;
                items.Add(r.Editor);
            }

            TransferAction(this, items);
        }

        private void toolStripButtonClone_Click(object sender, EventArgs e)
        {
            IList selected = list.SelectedObjects;
            if (selected.Count != 1)
            {
                MessageBox.Show(Parent, "Select one item and press this button to clone it.", "Select one item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Row r = (Row)selected[0];
            string name = r.Editor.Name ?? "Unknown";

            try
            {
                JSL.MajorItemEditor clone = r.Editor.Clone(JSL.CloneIdentity.New);
                Editor.Add(clone, JSL.ConflictBehavior.Error);
                LogAction(this, Model.ActionLog.Level.Info, $"Duplicated {SelfDesignation} item \"{name}\"");
            }
            catch (Exception ex)
            {
                LogAndShowError($"Failed to duplicate {SelfDesignation} item \"{name}\": {ex.Message}", "Failed to duplicate");
            }

            Reload();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            MajorItemWindow propsWindow = new MajorItemWindow(Editor.New(), AllowCustomization);
            propsWindow.ShowDialog();
            if (propsWindow.ShouldSave)
            {
                try
                {
                    Editor.Add(propsWindow.Editor, JSL.ConflictBehavior.Error);
                }
                catch (Exception ex1)
                {
                    string name = propsWindow.Editor.Name ?? "Unknown";
                    LogAndShowError($"Failed to add {SelfDesignation} item \"{name}\". Error: {ex1.Message}", "Failed to add item");
                }

                Reload();
            }
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            IList selected = list.SelectedObjects;
            if (selected.Count == 0)
            {
                MessageBox.Show("Select one or more items and press this button to remove them", "No items selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string submsg = selected.Count == 1 ? "this item" : $"these {selected.Count} items";
            string msg = $"Are you sure you want to permanently remove {submsg}?\n\nThis is irreversible.";
            if (MessageBox.Show(Parent, msg, $"Remove {submsg}?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            bool failed = false;
            foreach (object obj in selected)
            {
                Row r = (Row)obj;
                string name = r.Editor.Name ?? "Unknown";
                try
                {
                    Editor.Remove(r.Editor);
                    LogAction(this, Model.ActionLog.Level.Warning, $"Removed {SelfDesignation} item \"{name}\"");
                }
                catch (Exception ex)
                {
                    LogAction(this, Model.ActionLog.Level.Error, $"Failed to remove {SelfDesignation} item \"{name}\": {ex.Message}");
                    failed = true;
                }

                if (failed)
                {
                    MessageBox.Show("Failed to remove one or more of the selected items. See log for more info.", "Failed to delete item(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Reload();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            Row row = list.SelectedObject as Row;
            if (row == null)
            {
                MessageBox.Show(Parent, "Select an item and press this button to see its properties", "Item properties", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MajorItemWindow propsWindow = new MajorItemWindow(row.Editor.Clone(JSL.CloneIdentity.Same), AllowCustomization);
            propsWindow.ShowDialog();
            if (propsWindow.ShouldSave)
            {
                Editor.Remove(row.Editor);
                try
                {
                    Editor.Add(propsWindow.Editor, JSL.ConflictBehavior.Error);
                }
                catch (Exception ex1)
                {
                    try
                    {
                        // Attempt to re-add the previous item whatever it takes
                        Editor.Add(row.Editor, JSL.ConflictBehavior.Overwrite);

                        // Report the original failure
                        string name = propsWindow.Editor.Name ?? "Unknown";
                        LogAndShowError($"Failed to edit {SelfDesignation} item \"{name}\". The item is returned to its previous state. Error: {ex1.Message}", "Failed to edit item");
                    }
                    catch (Exception ex2)
                    {
                        // Report the critical failure
                        string name = row.Editor.Name ?? "Unknown";
                        LogAndShowError($"Failed to edit {SelfDesignation} item \"{name}\". Unfortunately, the item is lost. " +
                                        $"Please report this to the developer of JumpSaves. Error: {ex2.Message}", "Critical failure");
                    }
                }

                Reload();
            }
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            if (IsLibraryEditor)
            {
                LibraryEditor.Reload();
                Reload();
            }
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            if (!IsLibraryEditor)
            {
                return;
            }

            IList selected = list.SelectedObjects;
            if (selected.Count == 0)
            {
                MessageBox.Show("Select one or more items and press this button to export them", "No items selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string path = null;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Select a location to save this JumpSaves Library Archive file";
                dialog.Filter = "JumpSaves Library Archives (*.jsla.zip)|*.jsla.zip|All Files (*.*)|*.*";
                dialog.FilterIndex = 0;
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = false;
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                dialog.FileName = Utils.MakeSafeName(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                DialogResult result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return;
                }

                path = dialog.FileName;
            }

            List<JSL.MajorItemEditor> items = new List<JSL.MajorItemEditor>();
            foreach (object o in selected)
            {
                items.Add(((Row)o).Editor);
            }

            try
            {
                LibraryEditor.Export(items, path);
                string msg = $"Successfully exported {SelfDesignation} items to \"{path}\"";
                Common.OpenFolderAndSelect(path);
                LogAction(this, ActionLog.Level.Info, msg);
            }
            catch (Exception ex)
            {
                string msg = $"Failed to export {SelfDesignation} items to \"{path}\": {ex.Message}";
                LogAction(this, ActionLog.Level.Error, msg);
                MessageBox.Show(Parent, msg, "Failed to export items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            if (!IsLibraryEditor)
            {
                return;
            }

            string path = null;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select a JumpSaves Library Archive file to import";
                dialog.Filter = "JumpSaves Library Archives (*.jsla.zip)|*.jsla.zip|All Files (*.*)|*.*";
                dialog.FilterIndex = 0;
                dialog.Multiselect = false;
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = true;
                dialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                DialogResult result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return;
                }

                path = dialog.FileName;
            }

            try
            {
                LibraryEditor.Import(path);

                string msg = $"Successfully imported {SelfDesignation} items from \"{path}\"";
                LogAction(this, ActionLog.Level.Info, msg);

                Reload();
                MessageBox.Show(Parent, msg, "Successfully imported items", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string msg = $"Failed to import {SelfDesignation} items from \"{path}\": {ex.Message}";
                LogAction(this, ActionLog.Level.Error, msg);

                Reload();
                MessageBox.Show(Parent, msg, "Failed to import items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonBrowse_Click(object sender, EventArgs e)
        {
            string path = null;
            if (IsLibraryEditor)
            {
                path = LibraryEditor.Path;
            }
            else
            {
                path = SaveEditor.Path;
                if (File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                }
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }

        private void list_DoubleClick(object sender, EventArgs e)
        {
            toolStripButtonEdit_Click(sender, e);
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
            e.SubItem.ForeColor = Style.GetRarityColor(module?.Rarity ?? JSL.Rarity.Unknown, true);
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

        private void LogAndShowError(string text, string caption)
        {
            LogAction(this, ActionLog.Level.Error, text);
            MessageBox.Show(Parent, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            list.ShowGroups = true;
            list.AlwaysGroupByColumn = olvColumnCategory;

            toolStripComboBoxFilter.SelectedIndex = 0;
            toolStripComboBoxMonitor.SelectedIndex = 0;

            OnStateChange();
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            EnsureEditorAvailable();

            // List
            list.Enabled = Enabled;
            list.BackColor = Enabled ? Color.White : Color.Gainsboro;

            // Tool strip buttons
            toolStrip.Enabled = Enabled;
            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.Enabled = toolStrip.Enabled;
            }
            toolStripButtonAdd.Enabled &= AllowCustomization && CanEdit;
            toolStripButtonClone.Enabled &= AllowCustomization && CanEdit;
            toolStripButtonEdit.Enabled &= CanEdit;
            toolStripButtonRemove.Enabled &= CanEdit;
            toolStripButtonTransfer.Enabled &= CanTransfer;
            toolStripButtonExport.Visible = IsLibraryEditor;
            toolStripButtonImport.Visible = IsLibraryEditor;
            toolStripButtonReload.Visible = IsLibraryEditor;

            // Browsing (library only)
            toolStripButtonBrowse.Visible = Editor != null;

            // Tool strip filter
            toolStripLabelFilter.Enabled = Enabled;
            toolStripComboBoxFilter.Enabled = Enabled;
            toolStripLabelFilter.Visible = !IsLibraryEditor && Editor != null;
            toolStripComboBoxFilter.Visible = !IsLibraryEditor && Editor != null;

            // Tool strip monitor
            toolStripLabelMonitor.Enabled = Enabled;
            toolStripComboBoxMonitor.Enabled = Enabled;
            toolStripLabelMonitor.Visible = IsLibraryEditor && Editor != null;
            toolStripComboBoxMonitor.Visible = IsLibraryEditor && Editor != null;
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

        private void ApplyEditor(JSL.MajorItemListEditor editor)
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
        private bool canEdit_;
        private bool canTransfer_;
    }
}
