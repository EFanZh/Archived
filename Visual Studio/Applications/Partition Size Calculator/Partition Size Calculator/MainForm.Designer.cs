namespace PartitionSizeCalculator
{
    partial class MainForm
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
            this.textBoxGb = new System.Windows.Forms.TextBox();
            this.labelConvert = new System.Windows.Forms.Label();
            this.textBoxMbNtfs1 = new System.Windows.Forms.TextBox();
            this.labelMbNtfs = new System.Windows.Forms.Label();
            this.textBoxMbFat32 = new System.Windows.Forms.TextBox();
            this.labelMbFat32 = new System.Windows.Forms.Label();
            this.labelNtfs = new System.Windows.Forms.Label();
            this.labelFat32 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxGb
            // 
            this.textBoxGb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxGb.Location = new System.Drawing.Point(12, 26);
            this.textBoxGb.Name = "textBoxGb";
            this.textBoxGb.Size = new System.Drawing.Size(100, 23);
            this.textBoxGb.TabIndex = 0;
            this.textBoxGb.TextChanged += new System.EventHandler(this.textBoxGb_TextChanged);
            this.textBoxGb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxGb_KeyDown);
            // 
            // labelConvert
            // 
            this.labelConvert.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelConvert.AutoSize = true;
            this.labelConvert.Location = new System.Drawing.Point(118, 29);
            this.labelConvert.Name = "labelConvert";
            this.labelConvert.Size = new System.Drawing.Size(41, 15);
            this.labelConvert.TabIndex = 1;
            this.labelConvert.Text = "GB =>";
            // 
            // textBoxMbNtfs1
            // 
            this.textBoxMbNtfs1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMbNtfs1.Location = new System.Drawing.Point(214, 12);
            this.textBoxMbNtfs1.Name = "textBoxMbNtfs1";
            this.textBoxMbNtfs1.ReadOnly = true;
            this.textBoxMbNtfs1.Size = new System.Drawing.Size(100, 23);
            this.textBoxMbNtfs1.TabIndex = 3;
            // 
            // labelMbNtfs
            // 
            this.labelMbNtfs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMbNtfs.AutoSize = true;
            this.labelMbNtfs.Location = new System.Drawing.Point(320, 15);
            this.labelMbNtfs.Name = "labelMbNtfs";
            this.labelMbNtfs.Size = new System.Drawing.Size(25, 15);
            this.labelMbNtfs.TabIndex = 4;
            this.labelMbNtfs.Text = "MB";
            // 
            // textBoxMbFat32
            // 
            this.textBoxMbFat32.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMbFat32.Location = new System.Drawing.Point(214, 41);
            this.textBoxMbFat32.Name = "textBoxMbFat32";
            this.textBoxMbFat32.ReadOnly = true;
            this.textBoxMbFat32.Size = new System.Drawing.Size(100, 23);
            this.textBoxMbFat32.TabIndex = 6;
            // 
            // labelMbFat32
            // 
            this.labelMbFat32.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMbFat32.AutoSize = true;
            this.labelMbFat32.Location = new System.Drawing.Point(320, 44);
            this.labelMbFat32.Name = "labelMbFat32";
            this.labelMbFat32.Size = new System.Drawing.Size(25, 15);
            this.labelMbFat32.TabIndex = 7;
            this.labelMbFat32.Text = "MB";
            // 
            // labelNtfs
            // 
            this.labelNtfs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNtfs.AutoSize = true;
            this.labelNtfs.Location = new System.Drawing.Point(170, 15);
            this.labelNtfs.Name = "labelNtfs";
            this.labelNtfs.Size = new System.Drawing.Size(38, 15);
            this.labelNtfs.TabIndex = 2;
            this.labelNtfs.Text = "NTFS:";
            // 
            // labelFat32
            // 
            this.labelFat32.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFat32.AutoSize = true;
            this.labelFat32.Location = new System.Drawing.Point(165, 44);
            this.labelFat32.Name = "labelFat32";
            this.labelFat32.Size = new System.Drawing.Size(43, 15);
            this.labelFat32.TabIndex = 5;
            this.labelFat32.Text = "FAT32:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 76);
            this.Controls.Add(this.labelMbFat32);
            this.Controls.Add(this.textBoxMbFat32);
            this.Controls.Add(this.labelFat32);
            this.Controls.Add(this.labelMbNtfs);
            this.Controls.Add(this.textBoxMbNtfs1);
            this.Controls.Add(this.labelNtfs);
            this.Controls.Add(this.labelConvert);
            this.Controls.Add(this.textBoxGb);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partition Size Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGb;
        private System.Windows.Forms.Label labelConvert;
        private System.Windows.Forms.TextBox textBoxMbNtfs1;
        private System.Windows.Forms.Label labelMbNtfs;
        private System.Windows.Forms.TextBox textBoxMbFat32;
        private System.Windows.Forms.Label labelMbFat32;
        private System.Windows.Forms.Label labelNtfs;
        private System.Windows.Forms.Label labelFat32;
    }
}

