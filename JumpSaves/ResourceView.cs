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

        public EventHandler<EventArgs> MaybeDirty;

        public JSL.ResourceEditor Editor
        {
            get
            {
                return editor_;
            }
            set
            {
                if (editor_ != value)
                {
                    editor_ = value;
                    OnStateChange();
                }
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
                if (allowCustomization_ != value)
                {
                    allowCustomization_ = value;
                    OnStateChange();
                }
            }
        }

        private void ResourceView_Load(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void numericCredits_ValueChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                Editor.Credits = Convert.ToInt32(numericCredits.Value);
                EmitMaybeDirty();
            }
        }

        private void numericGreen_ValueChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                Editor.GreenIngots = Convert.ToInt32(numericGreen.Value);
                EmitMaybeDirty();
            }
        }

        private void numericBlue_ValueChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                Editor.BlueIngots = Convert.ToInt32(numericBlue.Value);
                EmitMaybeDirty();
            }
        }

        private void numericPurple_ValueChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                Editor.PurpleIngots = Convert.ToInt32(numericPurple.Value);
                EmitMaybeDirty();
            }
        }

        private void numericOrange_ValueChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                Editor.OrangeIngots = Convert.ToInt32(numericOrange.Value);
                EmitMaybeDirty();
            }
        }

        private void numericRed_ValueChanged(object sender, EventArgs e)
        {
            if (CanEdit)
            {
                Editor.RedIngots = Convert.ToInt32(numericRed.Value);
                EmitMaybeDirty();
            }
        }

        private void buttonMaxOut_Click(object sender, EventArgs e)
        {
            Editor.Credits = JSL.ResourceEditor.MaxCredits;
            Editor.GreenIngots = JSL.ResourceEditor.MaxIngots;
            Editor.BlueIngots = JSL.ResourceEditor.MaxIngots;
            Editor.PurpleIngots = JSL.ResourceEditor.MaxIngots;
            Editor.OrangeIngots = JSL.ResourceEditor.MaxIngots;
            Editor.RedIngots = JSL.ResourceEditor.MaxIngots;
            OnStateChange();
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            buttonMaxOut.Enabled = CanEdit && AllowCustomization;

            numericCredits.Enabled = CanEdit && AllowCustomization;
            numericGreen.Enabled = CanEdit && AllowCustomization;
            numericBlue.Enabled = CanEdit && AllowCustomization;
            numericPurple.Enabled = CanEdit && AllowCustomization;
            numericOrange.Enabled = CanEdit && AllowCustomization;
            numericRed.Enabled = CanEdit && AllowCustomization;
            
            numericCredits.Value = CanEdit ? Editor.Credits : 0;
            numericGreen.Value = CanEdit ? Editor.GreenIngots : 0;
            numericBlue.Value = CanEdit ? Editor.BlueIngots : 0;
            numericPurple.Value = CanEdit ? Editor.PurpleIngots : 0;
            numericOrange.Value = CanEdit ? Editor.OrangeIngots : 0;
            numericRed.Value = CanEdit ? Editor.RedIngots : 0;
        }

        private void EmitMaybeDirty()
        {
            MaybeDirty?.Invoke(this, null);
        }

        private JSL.ResourceEditor editor_;
        private bool allowCustomization_;
    }
}
