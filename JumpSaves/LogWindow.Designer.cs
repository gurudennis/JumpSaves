namespace JumpSaves
{
    partial class LogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
            this.list = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLevel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnOrigin = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.AllColumns.Add(this.olvColumnTime);
            this.list.AllColumns.Add(this.olvColumnLevel);
            this.list.AllColumns.Add(this.olvColumnOrigin);
            this.list.AllColumns.Add(this.olvColumnText);
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.CellEditUseWholeCell = false;
            this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnTime,
            this.olvColumnLevel,
            this.olvColumnOrigin,
            this.olvColumnText});
            this.list.Cursor = System.Windows.Forms.Cursors.Default;
            this.list.HideSelection = false;
            this.list.Location = new System.Drawing.Point(1, 36);
            this.list.Name = "list";
            this.list.ShowGroups = false;
            this.list.Size = new System.Drawing.Size(1016, 613);
            this.list.TabIndex = 0;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.VirtualMode = true;
            // 
            // olvColumnTime
            // 
            this.olvColumnTime.AspectName = "Timestamp";
            this.olvColumnTime.MaximumWidth = 140;
            this.olvColumnTime.MinimumWidth = 140;
            this.olvColumnTime.Text = "Time";
            this.olvColumnTime.Width = 140;
            // 
            // olvColumnLevel
            // 
            this.olvColumnLevel.AspectName = "Level";
            this.olvColumnLevel.MaximumWidth = 70;
            this.olvColumnLevel.MinimumWidth = 70;
            this.olvColumnLevel.Text = "Level";
            this.olvColumnLevel.Width = 70;
            // 
            // olvColumnOrigin
            // 
            this.olvColumnOrigin.AspectName = "Origin";
            this.olvColumnOrigin.MaximumWidth = 70;
            this.olvColumnOrigin.MinimumWidth = 70;
            this.olvColumnOrigin.Text = "Origin";
            this.olvColumnOrigin.Width = 70;
            // 
            // olvColumnText
            // 
            this.olvColumnText.AspectName = "Text";
            this.olvColumnText.FillsFreeSpace = true;
            this.olvColumnText.Text = "Message";
            this.olvColumnText.Width = 350;
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 649);
            this.Controls.Add(this.list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogWindow";
            this.Text = "JumpSaves Log";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogWindow_FormClosed);
            this.Load += new System.EventHandler(this.LogWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView list;
        private BrightIdeasSoftware.OLVColumn olvColumnTime;
        private BrightIdeasSoftware.OLVColumn olvColumnLevel;
        private BrightIdeasSoftware.OLVColumn olvColumnOrigin;
        private BrightIdeasSoftware.OLVColumn olvColumnText;
    }
}