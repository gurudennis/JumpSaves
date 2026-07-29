namespace JumpSaves
{
    partial class TutorialWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TutorialWindow));
            this.checkBoxNotAgain = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxNotAgain
            // 
            this.checkBoxNotAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxNotAgain.AutoSize = true;
            this.checkBoxNotAgain.Location = new System.Drawing.Point(13, 653);
            this.checkBoxNotAgain.Name = "checkBoxNotAgain";
            this.checkBoxNotAgain.Size = new System.Drawing.Size(154, 20);
            this.checkBoxNotAgain.TabIndex = 0;
            this.checkBoxNotAgain.Text = "Don\'t show this again";
            this.checkBoxNotAgain.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(617, 632);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(139, 41);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Got it!";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // TutorialWindow
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::JumpSaves.Properties.Resources.Tutorial;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(768, 685);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxNotAgain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TutorialWindow";
            this.Text = "How to use JumpSaves (at your own risk!)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TutorialWindow_FormClosed);
            this.Load += new System.EventHandler(this.TutorialWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxNotAgain;
        private System.Windows.Forms.Button buttonOK;
    }
}