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
            this.components = new System.ComponentModel.Container();
            this.list = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSlotInCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRarity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLevel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModule1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModule2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModule3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModule4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModule5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTransfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClone = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBrowse = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelFilter = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxFilter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelTotal = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelMonitor = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxMonitor = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllSuperiorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllRareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllUncommonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllCommonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.AllColumns.Add(this.olvColumnName);
            this.list.AllColumns.Add(this.olvColumnCategory);
            this.list.AllColumns.Add(this.olvColumnSlotInCategory);
            this.list.AllColumns.Add(this.olvColumnRarity);
            this.list.AllColumns.Add(this.olvColumnLevel);
            this.list.AllColumns.Add(this.olvColumnModule1);
            this.list.AllColumns.Add(this.olvColumnModule2);
            this.list.AllColumns.Add(this.olvColumnModule3);
            this.list.AllColumns.Add(this.olvColumnModule4);
            this.list.AllColumns.Add(this.olvColumnModule5);
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.CellEditUseWholeCell = false;
            this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnRarity,
            this.olvColumnLevel,
            this.olvColumnModule1,
            this.olvColumnModule2,
            this.olvColumnModule3,
            this.olvColumnModule4,
            this.olvColumnModule5});
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
            this.list.Size = new System.Drawing.Size(800, 684);
            this.list.SpaceBetweenGroups = 10;
            this.list.TabIndex = 0;
            this.list.UseCellFormatEvents = true;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.VirtualMode = true;
            this.list.BeforeCreatingGroups += new System.EventHandler<BrightIdeasSoftware.CreateGroupsEventArgs>(this.list_BeforeCreatingGroups);
            this.list.CellClick += new System.EventHandler<BrightIdeasSoftware.CellClickEventArgs>(this.list_CellClick);
            this.list.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.list_CellToolTipShowing);
            this.list.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.list_FormatCell);
            this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
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
            this.olvColumnRarity.MaximumWidth = 65;
            this.olvColumnRarity.MinimumWidth = 65;
            this.olvColumnRarity.Sortable = false;
            this.olvColumnRarity.Text = "Rarity";
            this.olvColumnRarity.Width = 65;
            // 
            // olvColumnLevel
            // 
            this.olvColumnLevel.AspectName = "Level";
            this.olvColumnLevel.IsEditable = false;
            this.olvColumnLevel.MaximumWidth = 45;
            this.olvColumnLevel.MinimumWidth = 45;
            this.olvColumnLevel.Sortable = false;
            this.olvColumnLevel.Text = "Level";
            this.olvColumnLevel.Width = 45;
            // 
            // olvColumnModule1
            // 
            this.olvColumnModule1.AspectName = "Module1";
            this.olvColumnModule1.IsEditable = false;
            this.olvColumnModule1.MaximumWidth = 45;
            this.olvColumnModule1.MinimumWidth = 45;
            this.olvColumnModule1.Sortable = false;
            this.olvColumnModule1.Text = "Mods";
            this.olvColumnModule1.Width = 45;
            // 
            // olvColumnModule2
            // 
            this.olvColumnModule2.AspectName = "Module2";
            this.olvColumnModule2.IsEditable = false;
            this.olvColumnModule2.MaximumWidth = 45;
            this.olvColumnModule2.MinimumWidth = 45;
            this.olvColumnModule2.Text = "";
            this.olvColumnModule2.Width = 45;
            // 
            // olvColumnModule3
            // 
            this.olvColumnModule3.AspectName = "Module3";
            this.olvColumnModule3.IsEditable = false;
            this.olvColumnModule3.MaximumWidth = 45;
            this.olvColumnModule3.MinimumWidth = 45;
            this.olvColumnModule3.Text = "";
            this.olvColumnModule3.Width = 45;
            // 
            // olvColumnModule4
            // 
            this.olvColumnModule4.AspectName = "Module4";
            this.olvColumnModule4.IsEditable = false;
            this.olvColumnModule4.MaximumWidth = 45;
            this.olvColumnModule4.MinimumWidth = 45;
            this.olvColumnModule4.Text = "";
            this.olvColumnModule4.Width = 45;
            // 
            // olvColumnModule5
            // 
            this.olvColumnModule5.AspectName = "Module5";
            this.olvColumnModule5.IsEditable = false;
            this.olvColumnModule5.MaximumWidth = 45;
            this.olvColumnModule5.MinimumWidth = 45;
            this.olvColumnModule5.Text = "";
            this.olvColumnModule5.Width = 45;
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
            this.toolStripButtonClone,
            this.toolStripButtonRemove,
            this.toolStripButtonEdit,
            this.toolStripButtonExport,
            this.toolStripButtonImport,
            this.toolStripButtonReload,
            this.toolStripButtonBrowse,
            this.toolStripLabelFilter,
            this.toolStripComboBoxFilter,
            this.toolStripLabelTotal,
            this.toolStripLabelMonitor,
            this.toolStripComboBoxMonitor});
            this.toolStrip.Location = new System.Drawing.Point(1, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(799, 31);
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
            this.toolStripButtonTransfer.Click += new System.EventHandler(this.toolStripButtonTransfer_Click);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = global::JumpSaves.Properties.Resources.Add;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonAdd.Text = "Add...";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonClone
            // 
            this.toolStripButtonClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClone.Image = global::JumpSaves.Properties.Resources.Clone;
            this.toolStripButtonClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClone.Name = "toolStripButtonClone";
            this.toolStripButtonClone.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonClone.Text = "Clone selected items";
            this.toolStripButtonClone.Click += new System.EventHandler(this.toolStripButtonClone_Click);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemove.Image = global::JumpSaves.Properties.Resources.Remove;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonRemove.Text = "Remove selected items";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.toolStripButtonRemove_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::JumpSaves.Properties.Resources.Edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonEdit.Text = "Properties of selected item";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExport.Image = global::JumpSaves.Properties.Resources.Export;
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonExport.Text = "Export selected items to a file";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripButtonImport
            // 
            this.toolStripButtonImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImport.Image = global::JumpSaves.Properties.Resources.Import;
            this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImport.Name = "toolStripButtonImport";
            this.toolStripButtonImport.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonImport.Text = "Import items from a file";
            this.toolStripButtonImport.Click += new System.EventHandler(this.toolStripButtonImport_Click);
            // 
            // toolStripButtonReload
            // 
            this.toolStripButtonReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReload.Image = global::JumpSaves.Properties.Resources.Reload;
            this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReload.Name = "toolStripButtonReload";
            this.toolStripButtonReload.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonReload.Text = "Reload from disk";
            this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
            // 
            // toolStripButtonBrowse
            // 
            this.toolStripButtonBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBrowse.Image = global::JumpSaves.Properties.Resources.Browse;
            this.toolStripButtonBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBrowse.Name = "toolStripButtonBrowse";
            this.toolStripButtonBrowse.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonBrowse.Text = "Browse library directory";
            this.toolStripButtonBrowse.Click += new System.EventHandler(this.toolStripButtonBrowse_Click);
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
            // toolStripLabelTotal
            // 
            this.toolStripLabelTotal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelTotal.Name = "toolStripLabelTotal";
            this.toolStripLabelTotal.Size = new System.Drawing.Size(57, 28);
            this.toolStripLabelTotal.Text = "Total: 0";
            // 
            // toolStripLabelMonitor
            // 
            this.toolStripLabelMonitor.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.toolStripLabelMonitor.Name = "toolStripLabelMonitor";
            this.toolStripLabelMonitor.Size = new System.Drawing.Size(97, 28);
            this.toolStripLabelMonitor.Text = "Auto acquire:";
            // 
            // toolStripComboBoxMonitor
            // 
            this.toolStripComboBoxMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxMonitor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.toolStripComboBoxMonitor.Items.AddRange(new object[] {
            "Superior",
            "Rare & up",
            "All",
            "OFF"});
            this.toolStripComboBoxMonitor.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripComboBoxMonitor.Name = "toolStripComboBoxMonitor";
            this.toolStripComboBoxMonitor.Size = new System.Drawing.Size(110, 31);
            this.toolStripComboBoxMonitor.ToolTipText = "Filter";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllSuperiorToolStripMenuItem,
            this.selectAllRareToolStripMenuItem,
            this.selectAllUncommonToolStripMenuItem,
            this.selectAllCommonToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(220, 100);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // selectAllSuperiorToolStripMenuItem
            // 
            this.selectAllSuperiorToolStripMenuItem.Name = "selectAllSuperiorToolStripMenuItem";
            this.selectAllSuperiorToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.selectAllSuperiorToolStripMenuItem.Text = "Select all Superior";
            this.selectAllSuperiorToolStripMenuItem.Click += new System.EventHandler(this.selectAllSuperiorToolStripMenuItem_Click);
            // 
            // selectAllRareToolStripMenuItem
            // 
            this.selectAllRareToolStripMenuItem.Name = "selectAllRareToolStripMenuItem";
            this.selectAllRareToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.selectAllRareToolStripMenuItem.Text = "Select all Rare";
            this.selectAllRareToolStripMenuItem.Click += new System.EventHandler(this.selectAllRareToolStripMenuItem_Click);
            // 
            // selectAllUncommonToolStripMenuItem
            // 
            this.selectAllUncommonToolStripMenuItem.Name = "selectAllUncommonToolStripMenuItem";
            this.selectAllUncommonToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.selectAllUncommonToolStripMenuItem.Text = "Select all Uncommon";
            this.selectAllUncommonToolStripMenuItem.Click += new System.EventHandler(this.selectAllUncommonToolStripMenuItem_Click);
            // 
            // selectAllCommonToolStripMenuItem
            // 
            this.selectAllCommonToolStripMenuItem.Name = "selectAllCommonToolStripMenuItem";
            this.selectAllCommonToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.selectAllCommonToolStripMenuItem.Text = "Select all Common";
            this.selectAllCommonToolStripMenuItem.Click += new System.EventHandler(this.selectAllCommonToolStripMenuItem_Click);
            // 
            // MajorItemList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.list);
            this.Name = "MajorItemList";
            this.Size = new System.Drawing.Size(801, 717);
            this.Load += new System.EventHandler(this.OnLoad);
            this.EnabledChanged += new System.EventHandler(this.OnEnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
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
        private OLVColumn olvColumnModule1;
        private OLVColumn olvColumnSlotInCategory;
        private OLVColumn olvColumnCategory;
        private System.Windows.Forms.ToolStripLabel toolStripLabelTotal;
        private OLVColumn olvColumnModule2;
        private OLVColumn olvColumnModule3;
        private OLVColumn olvColumnModule4;
        private OLVColumn olvColumnModule5;
        private System.Windows.Forms.ToolStripLabel toolStripLabelMonitor;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMonitor;
        private System.Windows.Forms.ToolStripButton toolStripButtonBrowse;
        private System.Windows.Forms.ToolStripButton toolStripButtonReload;
        private System.Windows.Forms.ToolStripButton toolStripButtonClone;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private System.Windows.Forms.ToolStripButton toolStripButtonImport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectAllSuperiorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllRareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllUncommonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllCommonToolStripMenuItem;
    }
}
