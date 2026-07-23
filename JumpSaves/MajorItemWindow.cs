using JSL;
using System;
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

        public bool IsDirty { get; private set; }

        public bool ShouldSave { get; private set; }

        private class TypeEntry
        {
            public MajorItemType.Enum Enum { get; set; }

            public string Raw { get; set; }

            public string Title { get; set; }

            public override string ToString()
            {
                return Title;
            }
        }

        private void MajorItemWindow_Load(object sender, EventArgs e)
        {
            // Buttons
            if (!canEdit_)
            {
                buttonOK.Enabled = false;
                buttonOK.Visible = false;
                buttonCancel.Text = "Close";
            }

            // Category
            comboBoxCategory.Enabled = canEdit_;
            for (int i = 1; i < (int)JSL.MajorItemCategory.Enum.__COUNT__; ++i)
            {
                comboBoxCategory.Items.Add(JSL.MajorItemCategory.GetTitle((JSL.MajorItemCategory.Enum)i));
            }
            comboBoxCategory.SelectedIndex = ((int)Editor.Category) - 1;

            // Type
            comboBoxType.Enabled = canEdit_;
            RepopulateType();

            // Name
            textBoxName.ReadOnly = !canEdit_;
            textBoxName.Text = Editor.GivenName ?? string.Empty;

            // Rarity
            comboBoxRarity.Enabled = canEdit_;
            comboBoxRarity.SelectedIndex = (int)Editor.Rarity;

            // Level
            numericUpDownLevel.Enabled = canEdit_;
            numericUpDownLevel.Value = Editor.Level;

            // Modules
            // ...

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
            if (!canEdit_)
            {
                buttonCancel_Click(sender, e);
                return;
            }

            if (IsDirty)
            {
                ShouldSave = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
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
        }

        private void numericUpDownLevel_ValueChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.Level = (int)numericUpDownLevel.Value;
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.Category = (JSL.MajorItemCategory.Enum)(comboBoxCategory.SelectedIndex + 1);

            RepopulateType();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            Editor.RawType = ((TypeEntry)comboBoxType.SelectedItem).Raw;
        }

        private void RepopulateType()
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
        }

        private JSL.MajorItemEditor editor_;
        bool canEdit_;
    }
}
