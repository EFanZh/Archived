namespace ImgProcCore
{
    partial class ImageAddTextPluginForm
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
            this.labelXOffset = new System.Windows.Forms.Label();
            this.textBoxXOffset = new System.Windows.Forms.TextBox();
            this.labelPixel1 = new System.Windows.Forms.Label();
            this.labelPixel2 = new System.Windows.Forms.Label();
            this.textBoxYOffset = new System.Windows.Forms.TextBox();
            this.labelPixel = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.radioButtonTL = new System.Windows.Forms.RadioButton();
            this.radioButtonTC = new System.Windows.Forms.RadioButton();
            this.radioButtonTR = new System.Windows.Forms.RadioButton();
            this.radioButtonML = new System.Windows.Forms.RadioButton();
            this.radioButtonMC = new System.Windows.Forms.RadioButton();
            this.radioButtonMR = new System.Windows.Forms.RadioButton();
            this.radioButtonBL = new System.Windows.Forms.RadioButton();
            this.radioButtonBC = new System.Windows.Forms.RadioButton();
            this.radioButtonBR = new System.Windows.Forms.RadioButton();
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelSplitter = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFontLabel = new System.Windows.Forms.Label();
            this.labelFont = new System.Windows.Forms.Label();
            this.labelColorLabel = new System.Windows.Forms.Label();
            this.labelColor = new System.Windows.Forms.Label();
            this.colorDialogMain = new System.Windows.Forms.ColorDialog();
            this.fontDialogMain = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // labelXOffset
            // 
            this.labelXOffset.AutoSize = true;
            this.labelXOffset.Location = new System.Drawing.Point(12, 15);
            this.labelXOffset.Name = "labelXOffset";
            this.labelXOffset.Size = new System.Drawing.Size(44, 15);
            this.labelXOffset.TabIndex = 0;
            this.labelXOffset.Text = "X 偏移:";
            // 
            // textBoxXOffset
            // 
            this.textBoxXOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXOffset.Location = new System.Drawing.Point(62, 12);
            this.textBoxXOffset.Name = "textBoxXOffset";
            this.textBoxXOffset.Size = new System.Drawing.Size(119, 23);
            this.textBoxXOffset.TabIndex = 1;
            this.textBoxXOffset.Text = "0";
            this.textBoxXOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxXOffset.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // labelPixel1
            // 
            this.labelPixel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel1.AutoSize = true;
            this.labelPixel1.Location = new System.Drawing.Point(187, 15);
            this.labelPixel1.Name = "labelPixel1";
            this.labelPixel1.Size = new System.Drawing.Size(31, 15);
            this.labelPixel1.TabIndex = 2;
            this.labelPixel1.Text = "像素";
            // 
            // labelPixel2
            // 
            this.labelPixel2.AutoSize = true;
            this.labelPixel2.Location = new System.Drawing.Point(12, 44);
            this.labelPixel2.Name = "labelPixel2";
            this.labelPixel2.Size = new System.Drawing.Size(44, 15);
            this.labelPixel2.TabIndex = 3;
            this.labelPixel2.Text = "Y 偏移:";
            // 
            // textBoxYOffset
            // 
            this.textBoxYOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYOffset.Location = new System.Drawing.Point(62, 41);
            this.textBoxYOffset.Name = "textBoxYOffset";
            this.textBoxYOffset.Size = new System.Drawing.Size(119, 23);
            this.textBoxYOffset.TabIndex = 4;
            this.textBoxYOffset.Text = "0";
            this.textBoxYOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxYOffset.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // labelPixel
            // 
            this.labelPixel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel.AutoSize = true;
            this.labelPixel.Location = new System.Drawing.Point(187, 44);
            this.labelPixel.Name = "labelPixel";
            this.labelPixel.Size = new System.Drawing.Size(31, 15);
            this.labelPixel.TabIndex = 5;
            this.labelPixel.Text = "像素";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(22, 73);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(34, 15);
            this.labelPosition.TabIndex = 6;
            this.labelPosition.Text = "定位:";
            // 
            // radioButtonTL
            // 
            this.radioButtonTL.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonTL.Checked = true;
            this.radioButtonTL.Location = new System.Drawing.Point(62, 70);
            this.radioButtonTL.Name = "radioButtonTL";
            this.radioButtonTL.Size = new System.Drawing.Size(23, 23);
            this.radioButtonTL.TabIndex = 7;
            this.radioButtonTL.TabStop = true;
            this.radioButtonTL.UseVisualStyleBackColor = true;
            // 
            // radioButtonTC
            // 
            this.radioButtonTC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonTC.Location = new System.Drawing.Point(91, 70);
            this.radioButtonTC.Name = "radioButtonTC";
            this.radioButtonTC.Size = new System.Drawing.Size(23, 23);
            this.radioButtonTC.TabIndex = 8;
            this.radioButtonTC.UseVisualStyleBackColor = true;
            // 
            // radioButtonTR
            // 
            this.radioButtonTR.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonTR.Location = new System.Drawing.Point(120, 70);
            this.radioButtonTR.Name = "radioButtonTR";
            this.radioButtonTR.Size = new System.Drawing.Size(23, 23);
            this.radioButtonTR.TabIndex = 9;
            this.radioButtonTR.UseVisualStyleBackColor = true;
            // 
            // radioButtonML
            // 
            this.radioButtonML.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonML.Location = new System.Drawing.Point(62, 99);
            this.radioButtonML.Name = "radioButtonML";
            this.radioButtonML.Size = new System.Drawing.Size(23, 23);
            this.radioButtonML.TabIndex = 10;
            this.radioButtonML.UseVisualStyleBackColor = true;
            // 
            // radioButtonMC
            // 
            this.radioButtonMC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMC.Location = new System.Drawing.Point(91, 99);
            this.radioButtonMC.Name = "radioButtonMC";
            this.radioButtonMC.Size = new System.Drawing.Size(23, 23);
            this.radioButtonMC.TabIndex = 11;
            this.radioButtonMC.UseVisualStyleBackColor = true;
            // 
            // radioButtonMR
            // 
            this.radioButtonMR.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMR.Location = new System.Drawing.Point(120, 99);
            this.radioButtonMR.Name = "radioButtonMR";
            this.radioButtonMR.Size = new System.Drawing.Size(23, 23);
            this.radioButtonMR.TabIndex = 12;
            this.radioButtonMR.UseVisualStyleBackColor = true;
            // 
            // radioButtonBL
            // 
            this.radioButtonBL.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBL.Location = new System.Drawing.Point(62, 128);
            this.radioButtonBL.Name = "radioButtonBL";
            this.radioButtonBL.Size = new System.Drawing.Size(23, 23);
            this.radioButtonBL.TabIndex = 13;
            this.radioButtonBL.UseVisualStyleBackColor = true;
            // 
            // radioButtonBC
            // 
            this.radioButtonBC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBC.Location = new System.Drawing.Point(91, 128);
            this.radioButtonBC.Name = "radioButtonBC";
            this.radioButtonBC.Size = new System.Drawing.Size(23, 23);
            this.radioButtonBC.TabIndex = 14;
            this.radioButtonBC.UseVisualStyleBackColor = true;
            // 
            // radioButtonBR
            // 
            this.radioButtonBR.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBR.Location = new System.Drawing.Point(120, 128);
            this.radioButtonBR.Name = "radioButtonBR";
            this.radioButtonBR.Size = new System.Drawing.Size(23, 23);
            this.radioButtonBR.TabIndex = 15;
            this.radioButtonBR.UseVisualStyleBackColor = true;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(22, 160);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(34, 15);
            this.labelText.TabIndex = 16;
            this.labelText.Text = "文本:";
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Location = new System.Drawing.Point(62, 157);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(156, 23);
            this.textBoxText.TabIndex = 17;
            this.textBoxText.Text = "测试文本";
            // 
            // labelSplitter
            // 
            this.labelSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSplitter.Location = new System.Drawing.Point(12, 273);
            this.labelSplitter.Margin = new System.Windows.Forms.Padding(3);
            this.labelSplitter.Name = "labelSplitter";
            this.labelSplitter.Size = new System.Drawing.Size(206, 2);
            this.labelSplitter.TabIndex = 22;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(62, 281);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 23;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(143, 281);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 24;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelFontLabel
            // 
            this.labelFontLabel.AutoSize = true;
            this.labelFontLabel.Location = new System.Drawing.Point(22, 189);
            this.labelFontLabel.Name = "labelFontLabel";
            this.labelFontLabel.Size = new System.Drawing.Size(34, 15);
            this.labelFontLabel.TabIndex = 18;
            this.labelFontLabel.Text = "字体:";
            // 
            // labelFont
            // 
            this.labelFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelFont.Location = new System.Drawing.Point(62, 186);
            this.labelFont.Margin = new System.Windows.Forms.Padding(3);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(156, 52);
            this.labelFont.TabIndex = 19;
            this.labelFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelFont.Click += new System.EventHandler(this.labelFont_Click);
            // 
            // labelColorLabel
            // 
            this.labelColorLabel.AutoSize = true;
            this.labelColorLabel.Location = new System.Drawing.Point(22, 247);
            this.labelColorLabel.Name = "labelColorLabel";
            this.labelColorLabel.Size = new System.Drawing.Size(34, 15);
            this.labelColorLabel.TabIndex = 20;
            this.labelColorLabel.Text = "颜色:";
            // 
            // labelColor
            // 
            this.labelColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColor.Location = new System.Drawing.Point(62, 244);
            this.labelColor.Margin = new System.Windows.Forms.Padding(3);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(156, 23);
            this.labelColor.TabIndex = 21;
            this.labelColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelColor.Click += new System.EventHandler(this.labelColor_Click);
            // 
            // fontDialogMain
            // 
            this.fontDialogMain.FontMustExist = true;
            // 
            // ImageAddTextPluginForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(230, 316);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelSplitter);
            this.Controls.Add(this.labelColor);
            this.Controls.Add(this.labelColorLabel);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.labelFontLabel);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.radioButtonBR);
            this.Controls.Add(this.radioButtonBC);
            this.Controls.Add(this.radioButtonBL);
            this.Controls.Add(this.radioButtonMR);
            this.Controls.Add(this.radioButtonMC);
            this.Controls.Add(this.radioButtonML);
            this.Controls.Add(this.radioButtonTR);
            this.Controls.Add(this.radioButtonTC);
            this.Controls.Add(this.radioButtonTL);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelPixel);
            this.Controls.Add(this.textBoxYOffset);
            this.Controls.Add(this.labelPixel2);
            this.Controls.Add(this.labelPixel1);
            this.Controls.Add(this.textBoxXOffset);
            this.Controls.Add(this.labelXOffset);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageAddTextPluginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加文本";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelXOffset;
        private System.Windows.Forms.TextBox textBoxXOffset;
        private System.Windows.Forms.Label labelPixel1;
        private System.Windows.Forms.Label labelPixel2;
        private System.Windows.Forms.TextBox textBoxYOffset;
        private System.Windows.Forms.Label labelPixel;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.RadioButton radioButtonTL;
        private System.Windows.Forms.RadioButton radioButtonTC;
        private System.Windows.Forms.RadioButton radioButtonTR;
        private System.Windows.Forms.RadioButton radioButtonML;
        private System.Windows.Forms.RadioButton radioButtonMC;
        private System.Windows.Forms.RadioButton radioButtonMR;
        private System.Windows.Forms.RadioButton radioButtonBL;
        private System.Windows.Forms.RadioButton radioButtonBC;
        private System.Windows.Forms.RadioButton radioButtonBR;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label labelSplitter;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFontLabel;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.Label labelColorLabel;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.ColorDialog colorDialogMain;
        private System.Windows.Forms.FontDialog fontDialogMain;
    }
}
