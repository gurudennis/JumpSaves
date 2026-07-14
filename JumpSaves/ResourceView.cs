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
    public partial class ResourceView : UserControl
    {
        public ResourceView()
        {
            InitializeComponent();
        }

        private void ResourceView_Load(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void numericCredits_ValueChanged(object sender, EventArgs e)
        {
            // Editor.Credits = numericCredits.Value;
        }

        private void numericGreen_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericBlue_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericPurple_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOrange_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericRed_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonMaxOut_Click(object sender, EventArgs e)
        {

        }

        public JSL.ResourceEditor Editor
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

        public bool CanEdit
        {
            get
            {
                return Editor != null;
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

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            numericCredits.Enabled = CanEdit && AllowCustomization;
            numericGreen.Enabled = CanEdit && AllowCustomization;
            numericBlue.Enabled = CanEdit && AllowCustomization;
            numericPurple.Enabled = CanEdit && AllowCustomization;
            numericOrange.Enabled = CanEdit && AllowCustomization;
            numericRed.Enabled = CanEdit && AllowCustomization;
            buttonMaxOut.Enabled = CanEdit && AllowCustomization;
            
            if (Editor == null)
            {
                numericCredits.Value = 0;
                numericGreen.Value = 0;
                numericBlue.Value = 0;
                numericPurple.Value = 0;
                numericOrange.Value = 0;
                numericRed.Value = 0;
            }
        }

        private JSL.ResourceEditor editor_;
        private bool allowCustomization_;
    }
}
