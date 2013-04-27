namespace ImgProcCore
{
    partial class ImageScalePluginForm
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageKeepSize = new System.Windows.Forms.TabPage();
            this.labelPixel2 = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelPixel1 = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.tabPageKeepAspectRatio = new System.Windows.Forms.TabPage();
            this.labelPixel4 = new System.Windows.Forms.Label();
            this.textBoxKeepHeight = new System.Windows.Forms.TextBox();
            this.radioButtonKeepHeight = new System.Windows.Forms.RadioButton();
            this.labelPixel3 = new System.Windows.Forms.Label();
            this.textBoxKeepWidth = new System.Windows.Forms.TextBox();
            this.radioButtonKeepWidth = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxResolution = new System.Windows.Forms.TextBox();
            this.labelResolution = new System.Windows.Forms.Label();
            this.labelPixelsPerInch = new System.Windows.Forms.Label();
            this.labelSplitter = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageKeepSize.SuspendLayout();
            this.tabPageKeepAspectRatio.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageKeepSize);
            this.tabControlMain.Controls.Add(this.tabPageKeepAspectRatio);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(208, 92);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageKeepSize
            // 
            this.tabPageKeepSize.Controls.Add(this.labelPixel2);
            this.tabPageKeepSize.Controls.Add(this.textBoxHeight);
            this.tabPageKeepSize.Controls.Add(this.labelHeight);
            this.tabPageKeepSize.Controls.Add(this.labelPixel1);
            this.tabPageKeepSize.Controls.Add(this.textBoxWidth);
            this.tabPageKeepSize.Controls.Add(this.labelWidth);
            this.tabPageKeepSize.Location = new System.Drawing.Point(4, 24);
            this.tabPageKeepSize.Name = "tabPageKeepSize";
            this.tabPageKeepSize.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKeepSize.Size = new System.Drawing.Size(200, 64);
            this.tabPageKeepSize.TabIndex = 0;
            this.tabPageKeepSize.Text = "确定尺寸";
            this.tabPageKeepSize.UseVisualStyleBackColor = true;
            // 
            // labelPixel2
            // 
            this.labelPixel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel2.AutoSize = true;
            this.labelPixel2.Location = new System.Drawing.Point(163, 38);
            this.labelPixel2.Name = "labelPixel2";
            this.labelPixel2.Size = new System.Drawing.Size(31, 15);
            this.labelPixel2.TabIndex = 5;
            this.labelPixel2.Text = "像素";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeight.Location = new System.Drawing.Point(46, 35);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(111, 23);
            this.textBoxHeight.TabIndex = 4;
            this.textBoxHeight.Text = "480";
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxHeight.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(6, 38);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(34, 15);
            this.labelHeight.TabIndex = 3;
            this.labelHeight.Text = "高度:";
            // 
            // labelPixel1
            // 
            this.labelPixel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel1.AutoSize = true;
            this.labelPixel1.Location = new System.Drawing.Point(163, 9);
            this.labelPixel1.Name = "labelPixel1";
            this.labelPixel1.Size = new System.Drawing.Size(31, 15);
            this.labelPixel1.TabIndex = 2;
            this.labelPixel1.Text = "像素";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWidth.Location = new System.Drawing.Point(46, 6);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(111, 23);
            this.textBoxWidth.TabIndex = 1;
            this.textBoxWidth.Text = "640";
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(6, 9);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(34, 15);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "宽度:";
            // 
            // tabPageKeepAspectRatio
            // 
            this.tabPageKeepAspectRatio.Controls.Add(this.labelPixel4);
            this.tabPageKeepAspectRatio.Controls.Add(this.textBoxKeepHeight);
            this.tabPageKeepAspectRatio.Controls.Add(this.radioButtonKeepHeight);
            this.tabPageKeepAspectRatio.Controls.Add(this.labelPixel3);
            this.tabPageKeepAspectRatio.Controls.Add(this.textBoxKeepWidth);
            this.tabPageKeepAspectRatio.Controls.Add(this.radioButtonKeepWidth);
            this.tabPageKeepAspectRatio.Location = new System.Drawing.Point(4, 24);
            this.tabPageKeepAspectRatio.Name = "tabPageKeepAspectRatio";
            this.tabPageKeepAspectRatio.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKeepAspectRatio.Size = new System.Drawing.Size(200, 64);
            this.tabPageKeepAspectRatio.TabIndex = 1;
            this.tabPageKeepAspectRatio.Text = "保持原有比例";
            this.tabPageKeepAspectRatio.UseVisualStyleBackColor = true;
            // 
            // labelPixel4
            // 
            this.labelPixel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel4.AutoSize = true;
            this.labelPixel4.Location = new System.Drawing.Point(163, 38);
            this.labelPixel4.Name = "labelPixel4";
            this.labelPixel4.Size = new System.Drawing.Size(31, 15);
            this.labelPixel4.TabIndex = 5;
            this.labelPixel4.Text = "像素";
            // 
            // textBoxKeepHeight
            // 
            this.textBoxKeepHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeepHeight.Enabled = false;
            this.textBoxKeepHeight.Location = new System.Drawing.Point(88, 35);
            this.textBoxKeepHeight.Name = "textBoxKeepHeight";
            this.textBoxKeepHeight.Size = new System.Drawing.Size(69, 23);
            this.textBoxKeepHeight.TabIndex = 4;
            this.textBoxKeepHeight.Text = "480";
            this.textBoxKeepHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxKeepHeight.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // radioButtonKeepHeight
            // 
            this.radioButtonKeepHeight.AutoSize = true;
            this.radioButtonKeepHeight.Location = new System.Drawing.Point(6, 36);
            this.radioButtonKeepHeight.Name = "radioButtonKeepHeight";
            this.radioButtonKeepHeight.Size = new System.Drawing.Size(76, 19);
            this.radioButtonKeepHeight.TabIndex = 3;
            this.radioButtonKeepHeight.Text = "确定高度:";
            this.radioButtonKeepHeight.UseVisualStyleBackColor = true;
            this.radioButtonKeepHeight.CheckedChanged += new System.EventHandler(this.radioButtonKeep_CheckedChanged);
            // 
            // labelPixel3
            // 
            this.labelPixel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel3.AutoSize = true;
            this.labelPixel3.Location = new System.Drawing.Point(163, 9);
            this.labelPixel3.Name = "labelPixel3";
            this.labelPixel3.Size = new System.Drawing.Size(31, 15);
            this.labelPixel3.TabIndex = 2;
            this.labelPixel3.Text = "像素";
            // 
            // textBoxKeepWidth
            // 
            this.textBoxKeepWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeepWidth.Location = new System.Drawing.Point(88, 6);
            this.textBoxKeepWidth.Name = "textBoxKeepWidth";
            this.textBoxKeepWidth.Size = new System.Drawing.Size(69, 23);
            this.textBoxKeepWidth.TabIndex = 1;
            this.textBoxKeepWidth.Text = "640";
            this.textBoxKeepWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxKeepWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // radioButtonKeepWidth
            // 
            this.radioButtonKeepWidth.AutoSize = true;
            this.radioButtonKeepWidth.Checked = true;
            this.radioButtonKeepWidth.Location = new System.Drawing.Point(6, 7);
            this.radioButtonKeepWidth.Name = "radioButtonKeepWidth";
            this.radioButtonKeepWidth.Size = new System.Drawing.Size(76, 19);
            this.radioButtonKeepWidth.TabIndex = 0;
            this.radioButtonKeepWidth.TabStop = true;
            this.radioButtonKeepWidth.Text = "确定宽度:";
            this.radioButtonKeepWidth.UseVisualStyleBackColor = true;
            this.radioButtonKeepWidth.CheckedChanged += new System.EventHandler(this.radioButtonKeep_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(145, 147);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(64, 147);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxResolution
            // 
            this.textBoxResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResolution.Location = new System.Drawing.Point(64, 110);
            this.textBoxResolution.Name = "textBoxResolution";
            this.textBoxResolution.Size = new System.Drawing.Size(90, 23);
            this.textBoxResolution.TabIndex = 2;
            this.textBoxResolution.Text = "96";
            this.textBoxResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxResolution.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFloat_Validating);
            // 
            // labelResolution
            // 
            this.labelResolution.AutoSize = true;
            this.labelResolution.Location = new System.Drawing.Point(12, 113);
            this.labelResolution.Name = "labelResolution";
            this.labelResolution.Size = new System.Drawing.Size(46, 15);
            this.labelResolution.TabIndex = 1;
            this.labelResolution.Text = "分辨率:";
            // 
            // labelPixelsPerInch
            // 
            this.labelPixelsPerInch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixelsPerInch.AutoSize = true;
            this.labelPixelsPerInch.Location = new System.Drawing.Point(160, 113);
            this.labelPixelsPerInch.Name = "labelPixelsPerInch";
            this.labelPixelsPerInch.Size = new System.Drawing.Size(60, 15);
            this.labelPixelsPerInch.TabIndex = 3;
            this.labelPixelsPerInch.Text = "像素/英寸";
            // 
            // labelSplitter
            // 
            this.labelSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSplitter.Location = new System.Drawing.Point(12, 139);
            this.labelSplitter.Margin = new System.Windows.Forms.Padding(3);
            this.labelSplitter.Name = "labelSplitter";
            this.labelSplitter.Size = new System.Drawing.Size(208, 2);
            this.labelSplitter.TabIndex = 4;
            // 
            // ImageScalePluginForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(232, 182);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelSplitter);
            this.Controls.Add(this.labelPixelsPerInch);
            this.Controls.Add(this.textBoxResolution);
            this.Controls.Add(this.labelResolution);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageScalePluginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图像缩放";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageKeepSize.ResumeLayout(false);
            this.tabPageKeepSize.PerformLayout();
            this.tabPageKeepAspectRatio.ResumeLayout(false);
            this.tabPageKeepAspectRatio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageKeepSize;
        private System.Windows.Forms.TabPage tabPageKeepAspectRatio;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxResolution;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Label labelPixelsPerInch;
        private System.Windows.Forms.Label labelPixel4;
        private System.Windows.Forms.Label labelPixel3;
        private System.Windows.Forms.TextBox textBoxKeepHeight;
        private System.Windows.Forms.TextBox textBoxKeepWidth;
        private System.Windows.Forms.RadioButton radioButtonKeepHeight;
        private System.Windows.Forms.RadioButton radioButtonKeepWidth;
        private System.Windows.Forms.Label labelSplitter;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelPixel2;
        private System.Windows.Forms.Label labelPixel1;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxWidth;

    }
}
