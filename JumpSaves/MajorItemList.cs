using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class MajorItemList : UserControl
    {
        public MajorItemList()
        {
            InitializeComponent();
        }

        public JSL.SaveEditor Editor;

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            list.Enabled = Enabled;
            list.BackColor = Enabled ? SystemColors.Control : Color.Gainsboro;

            toolStrip.Enabled = Enabled;
            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.Enabled = toolStrip.Enabled;
            }
        }
    }
}
