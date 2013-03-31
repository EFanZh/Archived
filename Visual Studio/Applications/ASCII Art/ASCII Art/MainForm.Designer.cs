namespace ASCIIArt
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonSelectImage = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.labelLines = new System.Windows.Forms.Label();
            this.textBoxLines = new System.Windows.Forms.TextBox();
            this.labelColumns = new System.Windows.Forms.Label();
            this.textBoxColumns = new System.Windows.Forms.TextBox();
            this.labelFontFamily = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxFontFamily = new System.Windows.Forms.ComboBox();
            this.labelFontSize = new System.Windows.Forms.Label();
            this.textBoxFontSize = new System.Windows.Forms.TextBox();
            this.labelImageInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonGenerate.Location = new System.Drawing.Point(636, 467);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonSelectImage
            // 
            this.buttonSelectImage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSelectImage.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectImage.Name = "buttonSelectImage";
            this.buttonSelectImage.Size = new System.Drawing.Size(92, 23);
            this.buttonSelectImage.TabIndex = 0;
            this.buttonSelectImage.Text = "Select Image";
            this.buttonSelectImage.UseVisualStyleBackColor = true;
            this.buttonSelectImage.Click += new System.EventHandler(this.buttonSelectImage_Click);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxImage.BackgroundImage = global::ASCIIArt.Properties.Resources.Transparent_Background;
            this.pictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 41);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(780, 420);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 3;
            this.pictureBoxImage.TabStop = false;
            this.pictureBoxImage.DoubleClick += new System.EventHandler(this.pictureBoxImage_DoubleClick);
            // 
            // labelLines
            // 
            this.labelLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLines.AutoSize = true;
            this.labelLines.Location = new System.Drawing.Point(157, 470);
            this.labelLines.Name = "labelLines";
            this.labelLines.Size = new System.Drawing.Size(37, 15);
            this.labelLines.TabIndex = 4;
            this.labelLines.Text = "Lines:";
            // 
            // textBoxLines
            // 
            this.textBoxLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxLines.Location = new System.Drawing.Point(200, 467);
            this.textBoxLines.Name = "textBoxLines";
            this.textBoxLines.Size = new System.Drawing.Size(75, 23);
            this.textBoxLines.TabIndex = 5;
            // 
            // labelColumns
            // 
            this.labelColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelColumns.AutoSize = true;
            this.labelColumns.Location = new System.Drawing.Point(12, 470);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(58, 15);
            this.labelColumns.TabIndex = 2;
            this.labelColumns.Text = "Columns:";
            // 
            // textBoxColumns
            // 
            this.textBoxColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxColumns.Location = new System.Drawing.Point(76, 467);
            this.textBoxColumns.Name = "textBoxColumns";
            this.textBoxColumns.Size = new System.Drawing.Size(75, 23);
            this.textBoxColumns.TabIndex = 3;
            // 
            // labelFontFamily
            // 
            this.labelFontFamily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFontFamily.AutoSize = true;
            this.labelFontFamily.Location = new System.Drawing.Point(281, 470);
            this.labelFontFamily.Name = "labelFontFamily";
            this.labelFontFamily.Size = new System.Drawing.Size(72, 15);
            this.labelFontFamily.TabIndex = 6;
            this.labelFontFamily.Text = "Font Family:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(717, 467);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // openFileDialogMain
            // 
            this.openFileDialogMain.Filter = "Pictures (*.bmp; *.gif; *.jpg; *.png; *.tif)|*.bmp; *.gif; *.jpg; *.png; *.tif";
            // 
            // comboBoxFontFamily
            // 
            this.comboBoxFontFamily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFontFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFontFamily.FormattingEnabled = true;
            this.comboBoxFontFamily.Location = new System.Drawing.Point(359, 467);
            this.comboBoxFontFamily.Name = "comboBoxFontFamily";
            this.comboBoxFontFamily.Size = new System.Drawing.Size(121, 23);
            this.comboBoxFontFamily.Sorted = true;
            this.comboBoxFontFamily.TabIndex = 7;
            // 
            // labelFontSize
            // 
            this.labelFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFontSize.AutoSize = true;
            this.labelFontSize.Location = new System.Drawing.Point(486, 470);
            this.labelFontSize.Name = "labelFontSize";
            this.labelFontSize.Size = new System.Drawing.Size(57, 15);
            this.labelFontSize.TabIndex = 8;
            this.labelFontSize.Text = "Font Size:";
            // 
            // textBoxFontSize
            // 
            this.textBoxFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFontSize.Location = new System.Drawing.Point(549, 467);
            this.textBoxFontSize.Name = "textBoxFontSize";
            this.textBoxFontSize.Size = new System.Drawing.Size(75, 23);
            this.textBoxFontSize.TabIndex = 9;
            // 
            // labelImageInfo
            // 
            this.labelImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelImageInfo.Location = new System.Drawing.Point(110, 12);
            this.labelImageInfo.Margin = new System.Windows.Forms.Padding(3);
            this.labelImageInfo.Name = "labelImageInfo";
            this.labelImageInfo.Size = new System.Drawing.Size(682, 23);
            this.labelImageInfo.TabIndex = 1;
            this.labelImageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(804, 502);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.textBoxFontSize);
            this.Controls.Add(this.labelFontSize);
            this.Controls.Add(this.comboBoxFontFamily);
            this.Controls.Add(this.labelFontFamily);
            this.Controls.Add(this.textBoxLines);
            this.Controls.Add(this.labelLines);
            this.Controls.Add(this.textBoxColumns);
            this.Controls.Add(this.labelColumns);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.labelImageInfo);
            this.Controls.Add(this.buttonSelectImage);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.Text = "ASCII Art";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonSelectImage;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Label labelLines;
        private System.Windows.Forms.TextBox textBoxLines;
        private System.Windows.Forms.Label labelColumns;
        private System.Windows.Forms.TextBox textBoxColumns;
        private System.Windows.Forms.Label labelFontFamily;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.ComboBox comboBoxFontFamily;
        private System.Windows.Forms.Label labelFontSize;
        private System.Windows.Forms.TextBox textBoxFontSize;
        private System.Windows.Forms.Label labelImageInfo;

    }
}

