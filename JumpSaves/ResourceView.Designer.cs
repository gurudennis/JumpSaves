namespace JumpSaves
{
    partial class ResourceView
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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOrange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPurple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCredits)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonMaxOut
            // 
            this.buttonMaxOut.Image = global::JumpSaves.Properties.Resources.Riches_tiny;
            this.buttonMaxOut.Location = new System.Drawing.Point(137, 182);
            this.buttonMaxOut.Name = "buttonMaxOut";
            this.buttonMaxOut.Size = new System.Drawing.Size(210, 43);
            this.buttonMaxOut.TabIndex = 16;
            this.buttonMaxOut.Text = "Max me out!";
            this.buttonMaxOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMaxOut.UseVisualStyleBackColor = true;
            this.buttonMaxOut.Click += new System.EventHandler(this.buttonMaxOut_Click);
            // 
            // numericRed
            // 
            this.numericRed.Location = new System.Drawing.Point(137, 144);
            this.numericRed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericRed.Name = "numericRed";
            this.numericRed.Size = new System.Drawing.Size(210, 22);
            this.numericRed.TabIndex = 10;
            this.numericRed.ValueChanged += new System.EventHandler(this.numericRed_ValueChanged);
            // 
            // numericOrange
            // 
            this.numericOrange.Location = new System.Drawing.Point(137, 116);
            this.numericOrange.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericOrange.Name = "numericOrange";
            this.numericOrange.Size = new System.Drawing.Size(210, 22);
            this.numericOrange.TabIndex = 11;
            this.numericOrange.ValueChanged += new System.EventHandler(this.numericOrange_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.OrangeRed;
            this.label7.Location = new System.Drawing.Point(3, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Red ingots:";
            // 
            // numericPurple
            // 
            this.numericPurple.Location = new System.Drawing.Point(137, 88);
            this.numericPurple.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericPurple.Name = "numericPurple";
            this.numericPurple.Size = new System.Drawing.Size(210, 22);
            this.numericPurple.TabIndex = 12;
            this.numericPurple.ValueChanged += new System.EventHandler(this.numericPurple_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Coral;
            this.label6.Location = new System.Drawing.Point(3, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Orange ingots:";
            // 
            // numericBlue
            // 
            this.numericBlue.Location = new System.Drawing.Point(137, 60);
            this.numericBlue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericBlue.Name = "numericBlue";
            this.numericBlue.Size = new System.Drawing.Size(210, 22);
            this.numericBlue.TabIndex = 13;
            this.numericBlue.ValueChanged += new System.EventHandler(this.numericBlue_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumPurple;
            this.label5.Location = new System.Drawing.Point(3, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Purple ingots:";
            // 
            // numericGreen
            // 
            this.numericGreen.Location = new System.Drawing.Point(137, 32);
            this.numericGreen.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericGreen.Name = "numericGreen";
            this.numericGreen.Size = new System.Drawing.Size(210, 22);
            this.numericGreen.TabIndex = 14;
            this.numericGreen.ValueChanged += new System.EventHandler(this.numericGreen_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(3, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Blue ingots:";
            // 
            // numericCredits
            // 
            this.numericCredits.Location = new System.Drawing.Point(137, 3);
            this.numericCredits.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericCredits.Name = "numericCredits";
            this.numericCredits.Size = new System.Drawing.Size(210, 22);
            this.numericCredits.TabIndex = 15;
            this.numericCredits.ValueChanged += new System.EventHandler(this.numericCredits_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(3, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Green ingots:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Credits:";
            // 
            // ResourceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonMaxOut);
            this.Controls.Add(this.numericRed);
            this.Controls.Add(this.numericOrange);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericPurple);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericBlue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericGreen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericCredits);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "ResourceView";
            this.Size = new System.Drawing.Size(354, 230);
            this.Load += new System.EventHandler(this.ResourceView_Load);
            this.EnabledChanged += new System.EventHandler(this.OnEnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numericRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOrange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPurple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCredits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMaxOut;
        private System.Windows.Forms.NumericUpDown numericRed;
        private System.Windows.Forms.NumericUpDown numericOrange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericPurple;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericBlue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericGreen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericCredits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}
