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
            this.openDefaultDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runCLIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripOpenDefaultDirectoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOpenDirectoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripCloseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRunCLIButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripAboutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelMode = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripGameRunningLabel = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.editTabControl = new System.Windows.Forms.TabControl();
            this.editTabPage1 = new System.Windows.Forms.TabPage();
            this.buttonMaxOut = new System.Windows.Forms.Button();
            this.numericRed = new System.Windows.Forms.NumericUpDown();
            this.numericOrange = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericPurple = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericBlue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericGreen = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericCredits = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPlayerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPlayerNameHeading = new System.Windows.Forms.Label();
            this.saveLabel = new System.Windows.Forms.Label();
            this.libraryPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.editorMajorItemList = new JumpSaves.MajorItemList();
            this.libraryMajorItemList = new JumpSaves.MajorItemList();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.editorPanel.SuspendLayout();
            this.editTabControl.SuspendLayout();
            this.editTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOrange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPurple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCredits)).BeginInit();
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
            this.menuStrip.Size = new System.Drawing.Size(1637, 28);
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
            this.runCLIToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.runCLIToolStripMenuItem.Text = "Run CLI";
            this.runCLIToolStripMenuItem.Click += new System.EventHandler(this.runCLIToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::JumpSaves.Properties.Resources.About;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
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
            this.toolStripLabelMode,
            this.toolStripComboBoxMode,
            this.toolStripGameRunningLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1637, 31);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
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
            // toolStripLabelMode
            // 
            this.toolStripLabelMode.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.toolStripLabelMode.Name = "toolStripLabelMode";
            this.toolStripLabelMode.Size = new System.Drawing.Size(51, 28);
            this.toolStripLabelMode.Text = "Mode:";
            // 
            // toolStripComboBoxMode
            // 
            this.toolStripComboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxMode.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxMode.Items.AddRange(new object[] {
            "Transfer only",
            "Cheater"});
            this.toolStripComboBoxMode.Name = "toolStripComboBoxMode";
            this.toolStripComboBoxMode.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxMode.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxMode_SelectedIndexChanged);
            // 
            // toolStripGameRunningLabel
            // 
            this.toolStripGameRunningLabel.Image = global::JumpSaves.Properties.Resources.Running;
            this.toolStripGameRunningLabel.Margin = new System.Windows.Forms.Padding(30, 1, 0, 2);
            this.toolStripGameRunningLabel.Name = "toolStripGameRunningLabel";
            this.toolStripGameRunningLabel.Size = new System.Drawing.Size(180, 28);
            this.toolStripGameRunningLabel.Text = "Jump Space is running";
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
            this.splitContainer.Size = new System.Drawing.Size(1637, 874);
            this.splitContainer.SplitterDistance = 1005;
            this.splitContainer.SplitterWidth = 10;
            this.splitContainer.TabIndex = 2;
            this.splitContainer.TabStop = false;
            // 
            // editorPanel
            // 
            this.editorPanel.AutoSize = true;
            this.editorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editorPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.editorPanel.Controls.Add(this.editTabControl);
            this.editorPanel.Controls.Add(this.editorMajorItemList);
            this.editorPanel.Controls.Add(this.saveLabel);
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorPanel.Location = new System.Drawing.Point(0, 0);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Size = new System.Drawing.Size(1005, 874);
            this.editorPanel.TabIndex = 0;
            // 
            // editTabControl
            // 
            this.editTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.editTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.editTabControl.Controls.Add(this.editTabPage1);
            this.editTabControl.Location = new System.Drawing.Point(0, 25);
            this.editTabControl.Name = "editTabControl";
            this.editTabControl.SelectedIndex = 0;
            this.editTabControl.Size = new System.Drawing.Size(381, 851);
            this.editTabControl.TabIndex = 2;
            // 
            // editTabPage1
            // 
            this.editTabPage1.Controls.Add(this.buttonMaxOut);
            this.editTabPage1.Controls.Add(this.numericRed);
            this.editTabPage1.Controls.Add(this.numericOrange);
            this.editTabPage1.Controls.Add(this.label7);
            this.editTabPage1.Controls.Add(this.numericPurple);
            this.editTabPage1.Controls.Add(this.label6);
            this.editTabPage1.Controls.Add(this.numericBlue);
            this.editTabPage1.Controls.Add(this.label5);
            this.editTabPage1.Controls.Add(this.numericGreen);
            this.editTabPage1.Controls.Add(this.label4);
            this.editTabPage1.Controls.Add(this.numericCredits);
            this.editTabPage1.Controls.Add(this.label3);
            this.editTabPage1.Controls.Add(this.labelPlayerName);
            this.editTabPage1.Controls.Add(this.label1);
            this.editTabPage1.Controls.Add(this.labelPlayerNameHeading);
            this.editTabPage1.Location = new System.Drawing.Point(4, 28);
            this.editTabPage1.Name = "editTabPage1";
            this.editTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.editTabPage1.Size = new System.Drawing.Size(373, 819);
            this.editTabPage1.TabIndex = 0;
            this.editTabPage1.Text = "Resources";
            this.editTabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonMaxOut
            // 
            this.buttonMaxOut.Image = global::JumpSaves.Properties.Resources.Riches_tiny;
            this.buttonMaxOut.Location = new System.Drawing.Point(130, 244);
            this.buttonMaxOut.Name = "buttonMaxOut";
            this.buttonMaxOut.Size = new System.Drawing.Size(222, 43);
            this.buttonMaxOut.TabIndex = 3;
            this.buttonMaxOut.Text = "Max me out!";
            this.buttonMaxOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMaxOut.UseVisualStyleBackColor = true;
            // 
            // numericRed
            // 
            this.numericRed.Location = new System.Drawing.Point(130, 206);
            this.numericRed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericRed.Name = "numericRed";
            this.numericRed.Size = new System.Drawing.Size(222, 22);
            this.numericRed.TabIndex = 2;
            // 
            // numericOrange
            // 
            this.numericOrange.Location = new System.Drawing.Point(130, 178);
            this.numericOrange.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericOrange.Name = "numericOrange";
            this.numericOrange.Size = new System.Drawing.Size(222, 22);
            this.numericOrange.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.OrangeRed;
            this.label7.Location = new System.Drawing.Point(8, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Red ingots:";
            // 
            // numericPurple
            // 
            this.numericPurple.Location = new System.Drawing.Point(130, 150);
            this.numericPurple.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericPurple.Name = "numericPurple";
            this.numericPurple.Size = new System.Drawing.Size(222, 22);
            this.numericPurple.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Coral;
            this.label6.Location = new System.Drawing.Point(8, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Orange ingots:";
            // 
            // numericBlue
            // 
            this.numericBlue.Location = new System.Drawing.Point(130, 122);
            this.numericBlue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericBlue.Name = "numericBlue";
            this.numericBlue.Size = new System.Drawing.Size(222, 22);
            this.numericBlue.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumPurple;
            this.label5.Location = new System.Drawing.Point(8, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Purple ingots:";
            // 
            // numericGreen
            // 
            this.numericGreen.Location = new System.Drawing.Point(130, 94);
            this.numericGreen.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericGreen.Name = "numericGreen";
            this.numericGreen.Size = new System.Drawing.Size(222, 22);
            this.numericGreen.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(8, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Blue ingots:";
            // 
            // numericCredits
            // 
            this.numericCredits.Location = new System.Drawing.Point(130, 64);
            this.numericCredits.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericCredits.Name = "numericCredits";
            this.numericCredits.Size = new System.Drawing.Size(222, 22);
            this.numericCredits.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Green ingots:";
            // 
            // labelPlayerName
            // 
            this.labelPlayerName.AutoSize = true;
            this.labelPlayerName.Location = new System.Drawing.Point(127, 14);
            this.labelPlayerName.Name = "labelPlayerName";
            this.labelPlayerName.Size = new System.Drawing.Size(55, 16);
            this.labelPlayerName.TabIndex = 0;
            this.labelPlayerName.Text = "<name>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Location = new System.Drawing.Point(8, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Credits:";
            // 
            // labelPlayerNameHeading
            // 
            this.labelPlayerNameHeading.AutoSize = true;
            this.labelPlayerNameHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayerNameHeading.Location = new System.Drawing.Point(8, 14);
            this.labelPlayerNameHeading.Name = "labelPlayerNameHeading";
            this.labelPlayerNameHeading.Size = new System.Drawing.Size(56, 16);
            this.labelPlayerNameHeading.TabIndex = 0;
            this.labelPlayerNameHeading.Text = "Player:";
            // 
            // saveLabel
            // 
            this.saveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveLabel.Location = new System.Drawing.Point(4, 5);
            this.saveLabel.Name = "saveLabel";
            this.saveLabel.Size = new System.Drawing.Size(998, 18);
            this.saveLabel.TabIndex = 0;
            this.saveLabel.Text = "<save path>";
            // 
            // libraryPanel
            // 
            this.libraryPanel.AutoSize = true;
            this.libraryPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.libraryPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.libraryPanel.Controls.Add(this.libraryMajorItemList);
            this.libraryPanel.Controls.Add(this.label2);
            this.libraryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libraryPanel.Location = new System.Drawing.Point(0, 0);
            this.libraryPanel.Name = "libraryPanel";
            this.libraryPanel.Size = new System.Drawing.Size(622, 874);
            this.libraryPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "My Library";
            // 
            // editorMajorItemList
            // 
            this.editorMajorItemList.AllowCustomization = false;
            this.editorMajorItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorMajorItemList.BackColor = System.Drawing.Color.Silver;
            this.editorMajorItemList.Editor = null;
            this.editorMajorItemList.Location = new System.Drawing.Point(382, 25);
            this.editorMajorItemList.Name = "editorMajorItemList";
            this.editorMajorItemList.Size = new System.Drawing.Size(623, 850);
            this.editorMajorItemList.TabIndex = 1;
            // 
            // libraryMajorItemList
            // 
            this.libraryMajorItemList.AllowCustomization = false;
            this.libraryMajorItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.libraryMajorItemList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.libraryMajorItemList.BackColor = System.Drawing.Color.Silver;
            this.libraryMajorItemList.Editor = null;
            this.libraryMajorItemList.Location = new System.Drawing.Point(0, 25);
            this.libraryMajorItemList.Margin = new System.Windows.Forms.Padding(0);
            this.libraryMajorItemList.Name = "libraryMajorItemList";
            this.libraryMajorItemList.Size = new System.Drawing.Size(621, 848);
            this.libraryMajorItemList.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1637, 933);
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
            this.editTabControl.ResumeLayout(false);
            this.editTabPage1.ResumeLayout(false);
            this.editTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOrange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPurple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCredits)).EndInit();
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
        private MajorItemList libraryMajorItemList;
        private MajorItemList editorMajorItemList;
        private System.Windows.Forms.TabControl editTabControl;
        private System.Windows.Forms.TabPage editTabPage1;
        private System.Windows.Forms.Label labelPlayerName;
        private System.Windows.Forms.Label labelPlayerNameHeading;
        private System.Windows.Forms.NumericUpDown numericCredits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericRed;
        private System.Windows.Forms.NumericUpDown numericOrange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericPurple;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericBlue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericGreen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonMaxOut;
        private System.Windows.Forms.ToolStripLabel toolStripLabelMode;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMode;
    }
}

