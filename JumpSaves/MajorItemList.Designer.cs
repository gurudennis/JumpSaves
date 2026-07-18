using BrightIdeasSoftware;

namespace JumpSaves
{
    partial class MajorItemList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.list = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSlotInCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRarity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLevel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModules = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTransfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelFilter = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxFilter = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.AllColumns.Add(this.olvColumnName);
            this.list.AllColumns.Add(this.olvColumnCategory);
            this.list.AllColumns.Add(this.olvColumnSlotInCategory);
            this.list.AllColumns.Add(this.olvColumnRarity);
            this.list.AllColumns.Add(this.olvColumnLevel);
            this.list.AllColumns.Add(this.olvColumnModules);
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.CellEditUseWholeCell = false;
            this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnRarity,
            this.olvColumnLevel,
            this.olvColumnModules});
            this.list.Cursor = System.Windows.Forms.Cursors.Default;
            this.list.HasCollapsibleGroups = false;
            this.list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.list.HideSelection = false;
            this.list.Location = new System.Drawing.Point(0, 32);
            this.list.Margin = new System.Windows.Forms.Padding(0);
            this.list.Name = "list";
            this.list.ShowGroups = false;
            this.list.ShowItemCountOnGroups = true;
            this.list.ShowSortIndicators = false;
            this.list.Size = new System.Drawing.Size(479, 684);
            this.list.SpaceBetweenGroups = 10;
            this.list.TabIndex = 0;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.VirtualMode = true;
            this.list.BeforeCreatingGroups += new System.EventHandler<BrightIdeasSoftware.CreateGroupsEventArgs>(this.list_BeforeCreatingGroups);
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.FillsFreeSpace = true;
            this.olvColumnName.Sortable = false;
            this.olvColumnName.Text = "Name";
            this.olvColumnName.Width = 200;
            // 
            // olvColumnCategory
            // 
            this.olvColumnCategory.AspectName = "Category";
            this.olvColumnCategory.DisplayIndex = 1;
            this.olvColumnCategory.IsVisible = false;
            this.olvColumnCategory.Text = "Category";
            this.olvColumnCategory.Width = 0;
            // 
            // olvColumnSlotInCategory
            // 
            this.olvColumnSlotInCategory.AspectName = "SlotIndex";
            this.olvColumnSlotInCategory.IsVisible = false;
            this.olvColumnSlotInCategory.Text = "Slot in category";
            // 
            // olvColumnRarity
            // 
            this.olvColumnRarity.AspectName = "Rarity";
            this.olvColumnRarity.IsEditable = false;
            this.olvColumnRarity.MaximumWidth = 50;
            this.olvColumnRarity.MinimumWidth = 50;
            this.olvColumnRarity.Sortable = false;
            this.olvColumnRarity.Text = "Rarity";
            this.olvColumnRarity.Width = 50;
            // 
            // olvColumnLevel
            // 
            this.olvColumnLevel.AspectName = "Level";
            this.olvColumnLevel.IsEditable = false;
            this.olvColumnLevel.MaximumWidth = 50;
            this.olvColumnLevel.MinimumWidth = 50;
            this.olvColumnLevel.Sortable = false;
            this.olvColumnLevel.Text = "Level";
            this.olvColumnLevel.Width = 50;
            // 
            // olvColumnModules
            // 
            this.olvColumnModules.AspectName = "Modules";
            this.olvColumnModules.IsEditable = false;
            this.olvColumnModules.MaximumWidth = 140;
            this.olvColumnModules.MinimumWidth = 140;
            this.olvColumnModules.Sortable = false;
            this.olvColumnModules.Text = "Modules";
            this.olvColumnModules.Width = 140;
            // 
            // toolStrip
            // 
            this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip.AutoSize = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonTransfer,
            this.toolStripButtonAdd,
            this.toolStripButtonRemove,
            this.toolStripButtonEdit,
            this.toolStripLabelFilter,
            this.toolStripComboBoxFilter});
            this.toolStrip.Location = new System.Drawing.Point(1, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(478, 31);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "Actions";
            // 
            // toolStripButtonTransfer
            // 
            this.toolStripButtonTransfer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTransfer.Image = global::JumpSaves.Properties.Resources.Transfer;
            this.toolStripButtonTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTransfer.Name = "toolStripButtonTransfer";
            this.toolStripButtonTransfer.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonTransfer.Text = "Transfer a copy of selected items";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = global::JumpSaves.Properties.Resources.Add;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonAdd.Text = "Add...";
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemove.Image = global::JumpSaves.Properties.Resources.Remove;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonRemove.Text = "Remove selected items";
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::JumpSaves.Properties.Resources.Edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonEdit.Text = "Edit selected item";
            // 
            // toolStripLabelFilter
            // 
            this.toolStripLabelFilter.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.toolStripLabelFilter.Name = "toolStripLabelFilter";
            this.toolStripLabelFilter.Size = new System.Drawing.Size(48, 28);
            this.toolStripLabelFilter.Text = "Show:";
            // 
            // toolStripComboBoxFilter
            // 
            this.toolStripComboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.toolStripComboBoxFilter.Items.AddRange(new object[] {
            "Stored",
            "Recent"});
            this.toolStripComboBoxFilter.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripComboBoxFilter.Name = "toolStripComboBoxFilter";
            this.toolStripComboBoxFilter.Size = new System.Drawing.Size(100, 31);
            this.toolStripComboBoxFilter.ToolTipText = "Filter";
            this.toolStripComboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxFilter_SelectedIndexChanged);
            // 
            // MajorItemList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.list);
            this.Name = "MajorItemList";
            this.Size = new System.Drawing.Size(480, 717);
            this.Load += new System.EventHandler(this.OnLoad);
            this.EnabledChanged += new System.EventHandler(this.OnEnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.ToolStripButton toolStripButtonTransfer;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFilter;
        private FastObjectListView list;
        private OLVColumn olvColumnName;
        private OLVColumn olvColumnRarity;
        private OLVColumn olvColumnLevel;
        private OLVColumn olvColumnModules;
        private OLVColumn olvColumnSlotInCategory;
        private OLVColumn olvColumnCategory;
    }
}
