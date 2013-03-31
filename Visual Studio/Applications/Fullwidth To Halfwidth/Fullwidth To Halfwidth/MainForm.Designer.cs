namespace FullwidthToHalfwidth
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
            this.buttonFullwidthToHalfwidth = new System.Windows.Forms.Button();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonFullwidthToHalfwidth
            // 
            this.buttonFullwidthToHalfwidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFullwidthToHalfwidth.Location = new System.Drawing.Point(12, 12);
            this.buttonFullwidthToHalfwidth.Name = "buttonFullwidthToHalfwidth";
            this.buttonFullwidthToHalfwidth.Size = new System.Drawing.Size(260, 23);
            this.buttonFullwidthToHalfwidth.TabIndex = 0;
            this.buttonFullwidthToHalfwidth.Text = "Fullwidth To Halfwidth";
            this.buttonFullwidthToHalfwidth.UseVisualStyleBackColor = true;
            this.buttonFullwidthToHalfwidth.Click += new System.EventHandler(this.buttonFullwidthToHalfwidth_Click);
            // 
            // textBoxContent
            // 
            this.textBoxContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContent.Location = new System.Drawing.Point(12, 41);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxContent.Size = new System.Drawing.Size(260, 208);
            this.textBoxContent.TabIndex = 1;
            this.textBoxContent.WordWrap = false;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonFullwidthToHalfwidth;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.buttonFullwidthToHalfwidth);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fullwidth To Halfwidth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFullwidthToHalfwidth;
        private System.Windows.Forms.TextBox textBoxContent;
    }
}

