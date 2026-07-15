using System;
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

        public JSL.SaveEditor Editor
        {
            get
            {
                return editor_;
            }
            set
            {
                editor_ = value;
                OnStateChange();
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
                allowCustomization_ = value;
                OnStateChange();
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            toolStripComboBoxFilter.SelectedIndex = 0;

            OnStateChange();
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            list.Enabled = Enabled;
            list.BackColor = Enabled ? SystemColors.Control : Color.Gainsboro;

            toolStrip.Enabled = Enabled;
            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.Enabled = toolStrip.Enabled;
            }
            toolStripButtonAdd.Enabled &= AllowCustomization;
            toolStripButtonEdit.Enabled &= AllowCustomization;
        }

        private JSL.SaveEditor editor_;
        private bool allowCustomization_;
    }
}
