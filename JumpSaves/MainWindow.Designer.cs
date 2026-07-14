namespace JumpSaves
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.libraryPanel = new System.Windows.Forms.Panel();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripOpenDefaultDirectoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOpenDirectoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripCloseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRunCLIButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripAboutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripGameRunningLabel = new System.Windows.Forms.ToolStripLabel();
            this.openDefaultDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runCLIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSaveButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.editorPanel.SuspendLayout();
            this.libraryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1167, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDefaultDirectoryToolStripMenuItem,
            this.openDirectoryToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runCLIToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpenDefaultDirectoryButton,
            this.toolStripOpenDirectoryButton,
            this.toolStripOpenFileButton,
            this.toolStripSaveButton,
            this.toolStripCloseButton,
            this.toolStripRunCLIButton,
            this.toolStripAboutButton,
            this.toolStripGameRunningLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1167, 31);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 59);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.editorPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.libraryPanel);
            this.splitContainer.Size = new System.Drawing.Size(1167, 662);
            this.splitContainer.SplitterDistance = 703;
            this.splitContainer.SplitterWidth = 10;
            this.splitContainer.TabIndex = 2;
            // 
            // editorPanel
            // 
            this.editorPanel.AutoSize = true;
            this.editorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorPanel.BackColor = System.Drawing.Color.Silver;
            this.editorPanel.Controls.Add(this.saveLabel);
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorPanel.Location = new System.Drawing.Point(0, 0);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Size = new System.Drawing.Size(703, 662);
            this.editorPanel.TabIndex = 0;
            // 
            // libraryPanel
            // 
            this.libraryPanel.AutoSize = true;
            this.libraryPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.libraryPanel.BackColor = System.Drawing.Color.Silver;
            this.libraryPanel.Controls.Add(this.label2);
            this.libraryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libraryPanel.Location = new System.Drawing.Point(0, 0);
            this.libraryPanel.Name = "libraryPanel";
            this.libraryPanel.Size = new System.Drawing.Size(454, 662);
            this.libraryPanel.TabIndex = 0;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // saveLabel
            // 
            this.saveLabel.AutoSize = true;
            this.saveLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveLabel.Location = new System.Drawing.Point(6, 5);
            this.saveLabel.Name = "saveLabel";
            this.saveLabel.Size = new System.Drawing.Size(96, 18);
            this.saveLabel.TabIndex = 0;
            this.saveLabel.Text = "<save path>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Library";
            // 
            // toolStripOpenDefaultDirectoryButton
            // 
            this.toolStripOpenDefaultDirectoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOpenDefaultDirectoryButton.Image = global::JumpSaves.Properties.Resources.OpenDefaultDirectory;
            this.toolStripOpenDefaultDirectoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOpenDefaultDirectoryButton.Name = "toolStripOpenDefaultDirectoryButton";
            this.toolStripOpenDefaultDirectoryButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripOpenDefaultDirectoryButton.Text = "Open Default Directory";
            this.toolStripOpenDefaultDirectoryButton.Click += new System.EventHandler(this.toolStripOpenDefaultDirectoryButton_Click);
            // 
            // toolStripOpenDirectoryButton
            // 
            this.toolStripOpenDirectoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOpenDirectoryButton.Image = global::JumpSaves.Properties.Resources.OpenDirectory;
            this.toolStripOpenDirectoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOpenDirectoryButton.Name = "toolStripOpenDirectoryButton";
            this.toolStripOpenDirectoryButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripOpenDirectoryButton.Text = "Open Directory...";
            this.toolStripOpenDirectoryButton.Click += new System.EventHandler(this.toolStripOpenDirectoryButton_Click);
            // 
            // toolStripOpenFileButton
            // 
            this.toolStripOpenFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOpenFileButton.Image = global::JumpSaves.Properties.Resources.OpenFile;
            this.toolStripOpenFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOpenFileButton.Name = "toolStripOpenFileButton";
            this.toolStripOpenFileButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripOpenFileButton.Text = "Open File...";
            this.toolStripOpenFileButton.Click += new System.EventHandler(this.toolStripOpenFileButton_Click);
            // 
            // toolStripCloseButton
            // 
            this.toolStripCloseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCloseButton.Image = global::JumpSaves.Properties.Resources.Close;
            this.toolStripCloseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCloseButton.Name = "toolStripCloseButton";
            this.toolStripCloseButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripCloseButton.Text = "Close";
            this.toolStripCloseButton.Click += new System.EventHandler(this.toolStripCloseButton_Click);
            // 
            // toolStripRunCLIButton
            // 
            this.toolStripRunCLIButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRunCLIButton.Image = global::JumpSaves.Properties.Resources.RunCLI;
            this.toolStripRunCLIButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRunCLIButton.Name = "toolStripRunCLIButton";
            this.toolStripRunCLIButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripRunCLIButton.Text = "Run CLI";
            this.toolStripRunCLIButton.Click += new System.EventHandler(this.toolStripRunCLIButton_Click);
            // 
            // toolStripAboutButton
            // 
            this.toolStripAboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAboutButton.Image = global::JumpSaves.Properties.Resources.About;
            this.toolStripAboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAboutButton.Name = "toolStripAboutButton";
            this.toolStripAboutButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripAboutButton.Text = "About...";
            this.toolStripAboutButton.Click += new System.EventHandler(this.toolStripAboutButton_Click);
            // 
            // toolStripGameRunningLabel
            // 
            this.toolStripGameRunningLabel.Image = global::JumpSaves.Properties.Resources.Running;
            this.toolStripGameRunningLabel.Margin = new System.Windows.Forms.Padding(30, 1, 0, 2);
            this.toolStripGameRunningLabel.Name = "toolStripGameRunningLabel";
            this.toolStripGameRunningLabel.Size = new System.Drawing.Size(180, 28);
            this.toolStripGameRunningLabel.Text = "Jump Space is running";
            // 
            // openDefaultDirectoryToolStripMenuItem
            // 
            this.openDefaultDirectoryToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.OpenDefaultDirectory;
            this.openDefaultDirectoryToolStripMenuItem.Name = "openDefaultDirectoryToolStripMenuItem";
            this.openDefaultDirectoryToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.openDefaultDirectoryToolStripMenuItem.Text = "Open default directory";
            this.openDefaultDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDefaultDirectoryToolStripMenuItem_Click);
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.OpenDirectory;
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.openDirectoryToolStripMenuItem.Text = "Open directory...";
            this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.OpenFile;
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.openFileToolStripMenuItem.Text = "Open file...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.Save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.Close;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // runCLIToolStripMenuItem
            // 
            this.runCLIToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.RunCLI;
            this.runCLIToolStripMenuItem.Name = "runCLIToolStripMenuItem";
            this.runCLIToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.runCLIToolStripMenuItem.Text = "Run CLI";
            this.runCLIToolStripMenuItem.Click += new System.EventHandler(this.runCLIToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.About;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSaveButton
            // 
            this.toolStripSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveButton.Image = global::JumpSaves.Properties.Resources.Save;
            this.toolStripSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveButton.Name = "toolStripSaveButton";
            this.toolStripSaveButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripSaveButton.Text = "Save";
            this.toolStripSaveButton.Click += new System.EventHandler(this.toolStripSaveButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1167, 721);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "JumpSaves, a JumpSpace save editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.onFormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.editorPanel.ResumeLayout(false);
            this.editorPanel.PerformLayout();
            this.libraryPanel.ResumeLayout(false);
            this.libraryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripOpenDirectoryButton;
        private System.Windows.Forms.ToolStripButton toolStripOpenFileButton;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runCLIToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripRunCLIButton;
        private System.Windows.Forms.ToolStripButton toolStripOpenDefaultDirectoryButton;
        private System.Windows.Forms.ToolStripMenuItem openDefaultDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripCloseButton;
        private System.Windows.Forms.ToolStripLabel toolStripGameRunningLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel editorPanel;
        private System.Windows.Forms.Panel libraryPanel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripAboutButton;
        private System.Windows.Forms.Label saveLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripSaveButton;
    }
}

