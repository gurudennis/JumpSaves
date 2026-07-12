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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runCLIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripOpenDirectoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRunCLIButton = new System.Windows.Forms.ToolStripButton();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1306, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDirectoryToolStripMenuItem,
            this.openFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpenDirectoryButton,
            this.toolStripOpenFileButton,
            this.toolStripRunCLIButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1306, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runCLIToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // runCLIToolStripMenuItem
            // 
            this.runCLIToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.RunCLI;
            this.runCLIToolStripMenuItem.Name = "runCLIToolStripMenuItem";
            this.runCLIToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.runCLIToolStripMenuItem.Text = "Run CLI";
            this.runCLIToolStripMenuItem.Click += new System.EventHandler(this.runCLIToolStripMenuItem_Click);
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
            // toolStripRunCLIButton
            // 
            this.toolStripRunCLIButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRunCLIButton.Image = global::JumpSaves.Properties.Resources.RunCLI;
            this.toolStripRunCLIButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRunCLIButton.Name = "toolStripRunCLIButton";
            this.toolStripRunCLIButton.Size = new System.Drawing.Size(29, 28);
            this.toolStripRunCLIButton.Text = "toolStripButton1";
            this.toolStripRunCLIButton.Click += new System.EventHandler(this.toolStripRunCLIButton_Click);
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.OpenDirectory;
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openDirectoryToolStripMenuItem.Text = "Open directory...";
            this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.OpenFile;
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openFileToolStripMenuItem.Text = "Open file...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1306, 707);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "JumpSaves, a JumpSpace save editor";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripOpenDirectoryButton;
        private System.Windows.Forms.ToolStripButton toolStripOpenFileButton;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runCLIToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripRunCLIButton;
    }
}

