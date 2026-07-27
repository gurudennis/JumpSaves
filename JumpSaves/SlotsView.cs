using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class SlotsView : UserControl
    {
        public SlotsView()
        {
            InitializeComponent();
        }

        public JSL.SaveEditor Editor
        {
            get
            {
                return editor_;
            }
            set
            {
                if (editor_ != value)
                {
                    if (editor_ != null)
                    {
                        editor_.DirtyChanged -= OnDirtyChanged;
                    }

                    editor_ = value;

                    if (editor_ != null)
                    {
                        editor_.DirtyChanged += OnDirtyChanged;
                    }

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

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Editor = null; // also unsubscribes from its notifications
        }

        private void SlotsView_Load(object sender, System.EventArgs e)
        {
            OnStateChange();
        }

        private void SlotsView_EnabledChanged(object sender, System.EventArgs e)
        {
            OnStateChange();
        }

        private void numericUpDownPW_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.PlayerWeapons, (NumericUpDown)sender);
        }

        private void numericUpDownMT_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.Multiturrets, (NumericUpDown)sender);
        }

        private void numericUpDownPC_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.PilotCannons, (NumericUpDown)sender);
        }

        private void numericUpDownSW_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.SpecialWeapons, (NumericUpDown)sender);
        }

        private void numericUpDownE_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.Engines, (NumericUpDown)sender);
        }

        private void numericUpDownSG_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.ShieldGenerators, (NumericUpDown)sender);
        }

        private void numericUpDownS_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.Sensors, (NumericUpDown)sender);
        }

        private void numericUpDownR_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.Reactors, (NumericUpDown)sender);
        }

        private void numericUpDownAG_ValueChanged(object sender, System.EventArgs e)
        {
            SetValue(JSL.MajorItemCategory.Enum.AuxGenerators, (NumericUpDown)sender);
        }

        private void buttonAll6_Click(object sender, EventArgs e)
        {
            Debug.Assert(Editor.MajorItemSlotLimits.DefaultMaxSlotCount == 6);
            SetAllValues(6);
        }

        private void buttonAll12_Click(object sender, EventArgs e)
        {
            SetAllValues(12);
        }

        private void OnDirtyChanged(object sender, EventArgs args)
        {
            OnStateChange();
        }

        private void SetValue(JSL.MajorItemCategory.Enum category, NumericUpDown upDown)
        {
            if (CanEdit)
            {
                Editor.MajorItemSlotLimits.SetMaxMajorItemSlots(category, Convert.ToInt32(upDown.Value));
            }
        }

        private void SetAllValues(int value)
        {
            for (int i = 1; i < (int)JSL.MajorItemCategory.Enum.__COUNT__; ++i)
            {
                Editor.MajorItemSlotLimits.SetMaxMajorItemSlots((JSL.MajorItemCategory.Enum)i, value);
            }

            OnStateChange();
        }

        private void OnStateChange()
        {
            buttonAll6.Enabled = CanEdit && AllowCustomization;
            buttonAll12.Enabled = CanEdit && AllowCustomization;

            numericUpDownPW.Enabled = CanEdit && AllowCustomization;
            numericUpDownMT.Enabled = CanEdit && AllowCustomization;
            numericUpDownPC.Enabled = CanEdit && AllowCustomization;
            numericUpDownSW.Enabled = CanEdit && AllowCustomization;
            numericUpDownE.Enabled = CanEdit && AllowCustomization;
            numericUpDownSG.Enabled = CanEdit && AllowCustomization;
            numericUpDownS.Enabled = CanEdit && AllowCustomization;
            numericUpDownR.Enabled = CanEdit && AllowCustomization;
            numericUpDownAG.Enabled = CanEdit && AllowCustomization;

            int def = 2;
            Debug.Assert(def == (Editor?.MajorItemSlotLimits?.DefaultMinSlotCount ?? 2));
            numericUpDownPW.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.PlayerWeapons) : def;
            numericUpDownMT.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.Multiturrets) : def;
            numericUpDownPC.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.PilotCannons) : def;
            numericUpDownSW.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.SpecialWeapons) : def;
            numericUpDownE.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.Engines) : def;
            numericUpDownSG.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.ShieldGenerators) : def;
            numericUpDownS.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.Sensors) : def;
            numericUpDownR.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.Reactors) : def;
            numericUpDownAG.Value = CanEdit ? Editor.MajorItemSlotLimits.GetMaxMajorItemSlots(JSL.MajorItemCategory.Enum.AuxGenerators) : def;
        }

        private JSL.SaveEditor editor_;
        private bool allowCustomization_;
    }
}
