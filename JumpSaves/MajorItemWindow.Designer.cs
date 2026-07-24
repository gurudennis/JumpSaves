namespace JumpSaves
{
    partial class MajorItemWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MajorItemWindow));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRarity = new System.Windows.Forms.ComboBox();
            this.labelLevel = new System.Windows.Forms.Label();
            this.numericUpDownLevel = new System.Windows.Forms.NumericUpDown();
            this.labelCategory = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.moduleList = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnEffect = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPotency1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPotency2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPotency3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRanking = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnKind = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.labelModules = new System.Windows.Forms.Label();
            this.toolStripModules = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moduleList)).BeginInit();
            this.toolStripModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(492, 456);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(131, 35);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(644, 456);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(131, 35);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(18, 66);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 16);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(91, 63);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(279, 22);
            this.textBoxName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rarity:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxRarity
            // 
            this.comboBoxRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRarity.FormattingEnabled = true;
            this.comboBoxRarity.Items.AddRange(new object[] {
            "Common (Green)",
            "Uncommon (Blue)",
            "Rare (Purple)",
            "Superior (Orange)"});
            this.comboBoxRarity.Location = new System.Drawing.Point(459, 62);
            this.comboBoxRarity.Name = "comboBoxRarity";
            this.comboBoxRarity.Size = new System.Drawing.Size(164, 24);
            this.comboBoxRarity.TabIndex = 3;
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(656, 66);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(43, 16);
            this.labelLevel.TabIndex = 1;
            this.labelLevel.Text = "Level:";
            this.labelLevel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numericUpDownLevel
            // 
            this.numericUpDownLevel.Location = new System.Drawing.Point(707, 63);
            this.numericUpDownLevel.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLevel.Name = "numericUpDownLevel";
            this.numericUpDownLevel.Size = new System.Drawing.Size(68, 22);
            this.numericUpDownLevel.TabIndex = 4;
            this.numericUpDownLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(18, 23);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(65, 16);
            this.labelCategory.TabIndex = 1;
            this.labelCategory.Text = "Category:";
            this.labelCategory.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(91, 20);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(279, 24);
            this.comboBoxCategory.TabIndex = 3;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(411, 23);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(42, 16);
            this.labelType.TabIndex = 1;
            this.labelType.Text = "Type:";
            this.labelType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(459, 20);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(316, 24);
            this.comboBoxType.TabIndex = 3;
            // 
            // moduleList
            // 
            this.moduleList.AllColumns.Add(this.olvColumnEffect);
            this.moduleList.AllColumns.Add(this.olvColumnPotency1);
            this.moduleList.AllColumns.Add(this.olvColumnPotency2);
            this.moduleList.AllColumns.Add(this.olvColumnPotency3);
            this.moduleList.AllColumns.Add(this.olvColumnRanking);
            this.moduleList.AllColumns.Add(this.olvColumnKind);
            this.moduleList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moduleList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moduleList.CellEditUseWholeCell = false;
            this.moduleList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnEffect,
            this.olvColumnPotency1,
            this.olvColumnPotency2,
            this.olvColumnPotency3});
            this.moduleList.Cursor = System.Windows.Forms.Cursors.Default;
            this.moduleList.HasCollapsibleGroups = false;
            this.moduleList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.moduleList.HideSelection = false;
            this.moduleList.Location = new System.Drawing.Point(21, 177);
            this.moduleList.Name = "moduleList";
            this.moduleList.ShowGroups = false;
            this.moduleList.ShowSortIndicators = false;
            this.moduleList.Size = new System.Drawing.Size(754, 261);
            this.moduleList.SpaceBetweenGroups = 10;
            this.moduleList.TabIndex = 5;
            this.moduleList.UseCellFormatEvents = true;
            this.moduleList.UseCompatibleStateImageBehavior = false;
            this.moduleList.View = System.Windows.Forms.View.Details;
            this.moduleList.VirtualMode = true;
            this.moduleList.BeforeCreatingGroups += new System.EventHandler<BrightIdeasSoftware.CreateGroupsEventArgs>(this.moduleList_BeforeCreatingGroups);
            this.moduleList.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.moduleList_FormatCell);
            // 
            // olvColumnEffect
            // 
            this.olvColumnEffect.AspectName = "Effect";
            this.olvColumnEffect.FillsFreeSpace = true;
            this.olvColumnEffect.IsEditable = false;
            this.olvColumnEffect.Sortable = false;
            this.olvColumnEffect.Text = "Effect";
            this.olvColumnEffect.Width = 140;
            // 
            // olvColumnPotency1
            // 
            this.olvColumnPotency1.AspectName = "Potency1";
            this.olvColumnPotency1.MaximumWidth = 90;
            this.olvColumnPotency1.MinimumWidth = 90;
            this.olvColumnPotency1.Text = "Potency roll";
            this.olvColumnPotency1.Width = 90;
            // 
            // olvColumnPotency2
            // 
            this.olvColumnPotency2.AspectName = "Potency2";
            this.olvColumnPotency2.MaximumWidth = 90;
            this.olvColumnPotency2.MinimumWidth = 90;
            this.olvColumnPotency2.Text = "";
            this.olvColumnPotency2.Width = 90;
            // 
            // olvColumnPotency3
            // 
            this.olvColumnPotency3.AspectName = "Potency3";
            this.olvColumnPotency3.MaximumWidth = 90;
            this.olvColumnPotency3.MinimumWidth = 90;
            this.olvColumnPotency3.Text = "";
            this.olvColumnPotency3.Width = 90;
            // 
            // olvColumnRanking
            // 
            this.olvColumnRanking.AspectName = "Ranking";
            this.olvColumnRanking.IsVisible = false;
            this.olvColumnRanking.MaximumWidth = 0;
            this.olvColumnRanking.MinimumWidth = 0;
            this.olvColumnRanking.Text = "";
            this.olvColumnRanking.Width = 0;
            // 
            // olvColumnKind
            // 
            this.olvColumnKind.AspectName = "Kind";
            this.olvColumnKind.IsVisible = false;
            this.olvColumnKind.MaximumWidth = 0;
            this.olvColumnKind.MinimumWidth = 0;
            this.olvColumnKind.Text = "";
            this.olvColumnKind.Width = 0;
            // 
            // labelModules
            // 
            this.labelModules.AutoSize = true;
            this.labelModules.Location = new System.Drawing.Point(18, 110);
            this.labelModules.Name = "labelModules";
            this.labelModules.Size = new System.Drawing.Size(62, 16);
            this.labelModules.TabIndex = 1;
            this.labelModules.Text = "Modules:";
            this.labelModules.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // toolStripModules
            // 
            this.toolStripModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripModules.AutoSize = false;
            this.toolStripModules.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripModules.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripModules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonRemove,
            this.toolStripButtonEdit});
            this.toolStripModules.Location = new System.Drawing.Point(21, 146);
            this.toolStripModules.Name = "toolStripModules";
            this.toolStripModules.Size = new System.Drawing.Size(754, 31);
            this.toolStripModules.TabIndex = 6;
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = global::JumpSaves.Properties.Resources.Add;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonAdd.Text = "Add module";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemove.Image = global::JumpSaves.Properties.Resources.Remove;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonRemove.Text = "Remove selected module";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.toolStripButtonRemove_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::JumpSaves.Properties.Resources.Edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonEdit.Text = "Edit selected module";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // MajorItemWindow
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(794, 505);
            this.Controls.Add(this.toolStripModules);
            this.Controls.Add(this.moduleList);
            this.Controls.Add(this.numericUpDownLevel);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.comboBoxRarity);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.labelModules);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MajorItemWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MajorItemWindow_FormClosing);
            this.Load += new System.EventHandler(this.MajorItemWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moduleList)).EndInit();
            this.toolStripModules.ResumeLayout(false);
            this.toolStripModules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxRarity;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownLevel;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private BrightIdeasSoftware.FastObjectListView moduleList;
        private BrightIdeasSoftware.OLVColumn olvColumnEffect;
        private BrightIdeasSoftware.OLVColumn olvColumnPotency1;
        private BrightIdeasSoftware.OLVColumn olvColumnPotency2;
        private BrightIdeasSoftware.OLVColumn olvColumnPotency3;
        private System.Windows.Forms.Label labelModules;
        private BrightIdeasSoftware.OLVColumn olvColumnRanking;
        private BrightIdeasSoftware.OLVColumn olvColumnKind;
        private System.Windows.Forms.ToolStrip toolStripModules;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
    }
}