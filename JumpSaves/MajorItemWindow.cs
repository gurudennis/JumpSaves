using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class MajorItemWindow : Form
    {
        public MajorItemWindow(JSL.MajorItemEditor editor, bool canEdit)
        {
            editor_ = editor;
            canEdit_ = canEdit;

            InitializeComponent();
        }

        public JSL.MajorItemEditor Editor
        {
            get
            {
                return editor_;
            }
        }

        public bool IsDirty
        {
            get
            {
                return isDirty_;
            }
            private set
            {
                if (isDirty_ != value)
                {
                    isDirty_ = true;
                }
            }
        }

        public bool ShouldSave { get; private set; }

        private class TypeEntry
        {
            public JSL.MajorItemType.Enum Enum { get; set; }

            public string Raw { get; set; }

            public string Title { get; set; }

            public override string ToString()
            {
                return Title;
            }
        }

        private class ModuleRow
        {
            public string Effect { get; set; }

            public JSL.ModuleKind Kind { get; set; }

            public int Ranking { get; set; }

            public double? Potency1 { get; set; }

            public double? Potency2 { get; set; }

            public double? Potency3 { get; set; }

            public JSL.ModuleEditor Editor { get; set; }
        }

        private class GroupComparer : IComparer<OLVGroup>
        {
            public int Compare(OLVGroup x, OLVGroup y)
            {
                JSL.ModuleKind xe = (JSL.ModuleKind)x.Key;
                JSL.ModuleKind ye = (JSL.ModuleKind)y.Key;

                if (xe == JSL.ModuleKind.Unknown && ye == JSL.ModuleKind.Unknown)
                {
                    return 0;
                }
                else if (xe == JSL.ModuleKind.Unknown)
                {
                    return 1;
                }
                else if (ye == JSL.ModuleKind.Unknown)
                {
                    return -1;
                }

                return ((int)xe).CompareTo((int)ye);
            }
        }

        private void MajorItemWindow_Load(object sender, EventArgs e)
        {
            // Category
            comboBoxCategory.Enabled = canEdit_;
            for (int i = 1; i < (int)JSL.MajorItemCategory.Enum.__COUNT__; ++i)
            {
                comboBoxCategory.Items.Add(JSL.MajorItemCategory.GetTitle((JSL.MajorItemCategory.Enum)i));
            }
            comboBoxCategory.SelectedIndex = ((int)Editor.Category) - 1;
            CategoryUpdated(false);

            // Type
            comboBoxType.Enabled = canEdit_;

            // Name (can always be edited)
            textBoxName.Text = Editor.GivenName ?? string.Empty;

            // Rarity
            comboBoxRarity.Enabled = canEdit_;
            comboBoxRarity.SelectedIndex = (int)Editor.Rarity;
            RarityUpdated();

            // Level
            numericUpDownLevel.Enabled = canEdit_;
            numericUpDownLevel.Value = Editor.Level;

            // Tool strip
            toolStripButtonAdd.Enabled = canEdit_;
            toolStripButtonRemove.Enabled = canEdit_;
            toolStripButtonEdit.Enabled = canEdit_;

            // Modules
            moduleList.Scrollable = false;
            moduleList.ShowGroups = true;
            moduleList.AlwaysGroupByColumn = olvColumnKind;
            ReloadModuleList();

            // Only now start handling all the change events
            textBoxName.TextChanged += new EventHandler(textBoxName_TextChanged);
            comboBoxRarity.SelectedIndexChanged += new EventHandler(comboBoxRarity_SelectedIndexChanged);
            numericUpDownLevel.ValueChanged += new EventHandler(numericUpDownLevel_ValueChanged);
            comboBoxCategory.SelectedIndexChanged += new EventHandler(comboBoxCategory_SelectedIndexChanged);
            comboBoxType.SelectedIndexChanged += new EventHandler(comboBoxType_SelectedIndexChanged);
        }

        private void MajorItemWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDirty && !ShouldSave)
            {
                string text = $"You made changes to this item.\n\nDo you want to keep them?";
                DialogResult result = MessageBox.Show(this, text, "Keep changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ShouldSave = true;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ShouldSave = true;
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void moduleList_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            ModuleRow row = (ModuleRow)e.Model;

            if (e.Column == olvColumnEffect)
            {
                e.Item.Text = row.Effect;
                e.Item.ForeColor = Style.GetRarityColor(row.Editor.Rarity, true);
                e.Item.SelectedForeColor = e.Item.ForeColor;
                e.Item.SelectedBackColor = Style.GetRarityColor(row.Editor.Rarity, false);
            }
            else if (e.Column == olvColumnPotency1)
            {
                FormatPotencyColumn(row, 0, e);
            }
            else if (e.Column == olvColumnPotency2)
            {
                FormatPotencyColumn(row, 1, e);
            }
            else if (e.Column == olvColumnPotency3)
            {
                FormatPotencyColumn(row, 2, e);
            }
        }

        private void moduleList_BeforeCreatingGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            e.Parameters.SortItemsByPrimaryColumn = false;
            e.Parameters.PrimarySort = olvColumnRanking;
            e.Parameters.PrimarySortOrder = SortOrder.Ascending;
            e.Parameters.SecondarySort = olvColumnEffect;
            e.Parameters.SecondarySortOrder = SortOrder.Ascending;
            e.Parameters.GroupComparer = new GroupComparer();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.GivenName = textBoxName.Text;
        }

        private void comboBoxRarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.Rarity = (JSL.Rarity)comboBoxRarity.SelectedIndex;

            RarityUpdated();
        }

        private void numericUpDownLevel_ValueChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.Level = (int)numericUpDownLevel.Value;

            LevelUpdated();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.Category = (JSL.MajorItemCategory.Enum)(comboBoxCategory.SelectedIndex + 1);

            CategoryUpdated();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.RawType = ((TypeEntry)comboBoxType.SelectedItem).Raw;

            TypeUpdated();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            Debug.Assert(!canEdit_);
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            Debug.Assert(!canEdit_);
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            Debug.Assert(!canEdit_);
        }

        private void FormatPotencyColumn(ModuleRow row, int potencyIndex, FormatCellEventArgs e)
        {
            if (potencyIndex >= row.Editor.Potencies.Length)
            {
                e.SubItem.Text = "---";
                return;
            }

            double potencyPercent = row.Editor.Potencies[potencyIndex] * 100.0;
            e.SubItem.Text = potencyPercent.ToString("F2") + "%";
        }

        private void CategoryUpdated(bool cascade = true)
        {
            for (int i = 1; i < (int)JSL.MajorItemType.Enum.__COUNT__; ++i)
            {
                JSL.MajorItemType.Enum e = (JSL.MajorItemType.Enum)i;
                if (JSL.MajorItemType.GetCategory(e) == Editor.Category)
                {
                    TypeEntry entry = new TypeEntry();
                    entry.Enum = e;
                    entry.Raw = JSL.MajorItemType.GetRaw(e);
                    entry.Title = JSL.MajorItemType.GetTitle(e);
                    comboBoxType.Items.Add(entry);
                }
            }

            comboBoxType.SelectedItem = comboBoxType.Items.Cast<TypeEntry>().FirstOrDefault((item) => item.Raw == Editor.RawType);
            if (comboBoxType.SelectedItem == null && comboBoxType.Items.Count != 0)
            {
                comboBoxType.SelectedIndex = 0;
            }

            if (cascade)
            {
                TypeUpdated();
            }

            OnStateChange();
        }

        private void TypeUpdated()
        {
            JSL.MajorItemPurpose purpose = JSL.MajorItemType.GetPurpose(Editor.Type);
            if (purpose != purpose_)
            {
                while (Editor.ModuleCount != 0)
                {
                    Editor.RemoveModule(0);
                }

                ReloadModuleList();
            }

            OnStateChange();
        }

        private void LevelUpdated()
        {
            Editor.ResetActivePips();
        }

        private void RarityUpdated()
        {
            this.BackColor = Style.GetRarityColor(Editor.Rarity, false);
        }

        void OnStateChange()
        {
            buttonOK.Enabled = comboBoxCategory.SelectedIndex != -1 &&
                               comboBoxType.SelectedIndex != -1;
        }

        private void ReloadModuleList()
        {
            moduleRows_ = new List<ModuleRow>();

            for (int i = 0; i < Editor.ModuleCount; ++i)
            {
                ModuleRow row = new ModuleRow();
                row.Editor = Editor.GetModule(i);
                row.Effect = row.Editor.TypeName ?? "Unknown";
                row.Kind = row.Editor.Kind;
                row.Ranking = i;
                row.Potency1 = row.Editor.Potencies.Length >= 1 ? (double?)row.Editor.Potencies[0] : null;
                row.Potency2 = row.Editor.Potencies.Length >= 2 ? (double?)row.Editor.Potencies[1] : null;
                row.Potency3 = row.Editor.Potencies.Length >= 3 ? (double?)row.Editor.Potencies[2] : null;
                moduleRows_.Add(row);
            }

            moduleList.SetObjects(moduleRows_);
        }

        private JSL.MajorItemEditor editor_;
        private JSL.MajorItemPurpose purpose_ = JSL.MajorItemPurpose.Unknown; // determines what modules fit, loosely speaking
        bool canEdit_;
        bool isDirty_;
        private List<ModuleRow> moduleRows_;
    }
}
