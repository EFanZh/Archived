namespace ImageMatch
{
    partial class ResultForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.pctrBxMain = new System.Windows.Forms.PictureBox();
            this.svFlDlgMain = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Location = new System.Drawing.Point(12, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pctrBxMain
            // 
            this.pctrBxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctrBxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctrBxMain.Location = new System.Drawing.Point(12, 41);
            this.pctrBxMain.Name = "pctrBxMain";
            this.pctrBxMain.Size = new System.Drawing.Size(600, 426);
            this.pctrBxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctrBxMain.TabIndex = 1;
            this.pctrBxMain.TabStop = false;
            // 
            // svFlDlgMain
            // 
            this.svFlDlgMain.Filter = "PNG 图片 (*.png)|*.png";
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 479);
            this.Controls.Add(this.pctrBxMain);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ResultForm";
            this.Text = "查找结果";
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pctrBxMain;
        private System.Windows.Forms.SaveFileDialog svFlDlgMain;

    }
}