namespace ImageMatch
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.lblColorShow = new System.Windows.Forms.Label();
            this.txtBxRadius = new System.Windows.Forms.TextBox();
            this.pctrBxMain = new System.Windows.Forms.PictureBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblRadius = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.clrDlgMain = new System.Windows.Forms.ColorDialog();
            this.opFlDlgMain = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenImage.Location = new System.Drawing.Point(576, 12);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(116, 23);
            this.btnOpenImage.TabIndex = 0;
            this.btnOpenImage.Text = "打开图像(&O)";
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // lblColorShow
            // 
            this.lblColorShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColorShow.BackColor = System.Drawing.Color.White;
            this.lblColorShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblColorShow.Location = new System.Drawing.Point(632, 38);
            this.lblColorShow.Name = "lblColorShow";
            this.lblColorShow.Size = new System.Drawing.Size(60, 23);
            this.lblColorShow.TabIndex = 2;
            this.lblColorShow.Click += new System.EventHandler(this.lblColorShow_Click);
            // 
            // txtBxRadius
            // 
            this.txtBxRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxRadius.Location = new System.Drawing.Point(632, 64);
            this.txtBxRadius.Name = "txtBxRadius";
            this.txtBxRadius.Size = new System.Drawing.Size(60, 23);
            this.txtBxRadius.TabIndex = 4;
            this.txtBxRadius.Text = "32";
            this.txtBxRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pctrBxMain
            // 
            this.pctrBxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctrBxMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pctrBxMain.Location = new System.Drawing.Point(12, 12);
            this.pctrBxMain.Name = "pctrBxMain";
            this.pctrBxMain.Size = new System.Drawing.Size(558, 455);
            this.pctrBxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctrBxMain.TabIndex = 6;
            this.pctrBxMain.TabStop = false;
            // 
            // lblColor
            // 
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(576, 41);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(50, 15);
            this.lblColor.TabIndex = 1;
            this.lblColor.Text = "颜色(&C):";
            // 
            // lblRadius
            // 
            this.lblRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(577, 67);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(49, 15);
            this.lblRadius.TabIndex = 3;
            this.lblRadius.Text = "半径(&R):";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(576, 93);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(116, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查找(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // opFlDlgMain
            // 
            this.opFlDlgMain.Filter = "图像文件 (*.bmp; *.dib; *.gif; *.ico; *.jfif; *.jpe; *.jpeg; *.jpg; *.png; *.tif; *.t" +
    "iff)|*.bmp;*.dib;*.gif;*.ico;*.jfif;*.jpe;*.jpeg;*.jpg;*.png;*.tif;*.tiff|所有文件 (" +
    "*.*)|*.*";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 479);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBxRadius);
            this.Controls.Add(this.lblRadius);
            this.Controls.Add(this.lblColorShow);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.pctrBxMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Match";
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Label lblColorShow;
        private System.Windows.Forms.TextBox txtBxRadius;
        private System.Windows.Forms.PictureBox pctrBxMain;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ColorDialog clrDlgMain;
        private System.Windows.Forms.OpenFileDialog opFlDlgMain;
    }
}

