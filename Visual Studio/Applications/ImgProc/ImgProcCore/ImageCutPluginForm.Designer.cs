namespace ImgProcCore
{
    partial class ImageCutPluginForm
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
            this.radioButtonTL = new System.Windows.Forms.RadioButton();
            this.radioButtonTC = new System.Windows.Forms.RadioButton();
            this.radioButtonTR = new System.Windows.Forms.RadioButton();
            this.radioButtonML = new System.Windows.Forms.RadioButton();
            this.radioButtonMC = new System.Windows.Forms.RadioButton();
            this.radioButtonMR = new System.Windows.Forms.RadioButton();
            this.radioButtonBL = new System.Windows.Forms.RadioButton();
            this.radioButtonBC = new System.Windows.Forms.RadioButton();
            this.radioButtonBR = new System.Windows.Forms.RadioButton();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPixel1 = new System.Windows.Forms.Label();
            this.labelPixel = new System.Windows.Forms.Label();
            this.labelSplitter = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelFillColor = new System.Windows.Forms.Label();
            this.labelFillColorLabel = new System.Windows.Forms.Label();
            this.colorDialogMain = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // radioButtonTL
            // 
            this.radioButtonTL.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonTL.Checked = true;
            this.radioButtonTL.Location = new System.Drawing.Point(64, 70);
            this.radioButtonTL.Name = "radioButtonTL";
            this.radioButtonTL.Size = new System.Drawing.Size(23, 23);
            this.radioButtonTL.TabIndex = 7;
            this.radioButtonTL.TabStop = true;
            this.radioButtonTL.UseVisualStyleBackColor = true;
            // 
            // radioButtonTC
            // 
            this.radioButtonTC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonTC.Location = new System.Drawing.Point(93, 70);
            this.radioButtonTC.Name = "radioButtonTC";
            this.radioButtonTC.Size = new System.Drawing.Size(23, 23);
            this.radioButtonTC.TabIndex = 8;
            this.radioButtonTC.UseVisualStyleBackColor = true;
            // 
            // radioButtonTR
            // 
            this.radioButtonTR.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonTR.Location = new System.Drawing.Point(122, 70);
            this.radioButtonTR.Name = "radioButtonTR";
            this.radioButtonTR.Size = new System.Drawing.Size(23, 23);
            this.radioButtonTR.TabIndex = 9;
            this.radioButtonTR.UseVisualStyleBackColor = true;
            // 
            // radioButtonML
            // 
            this.radioButtonML.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonML.Location = new System.Drawing.Point(64, 99);
            this.radioButtonML.Name = "radioButtonML";
            this.radioButtonML.Size = new System.Drawing.Size(23, 23);
            this.radioButtonML.TabIndex = 10;
            this.radioButtonML.UseVisualStyleBackColor = true;
            // 
            // radioButtonMC
            // 
            this.radioButtonMC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMC.Location = new System.Drawing.Point(93, 99);
            this.radioButtonMC.Name = "radioButtonMC";
            this.radioButtonMC.Size = new System.Drawing.Size(23, 23);
            this.radioButtonMC.TabIndex = 11;
            this.radioButtonMC.UseVisualStyleBackColor = true;
            // 
            // radioButtonMR
            // 
            this.radioButtonMR.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMR.Location = new System.Drawing.Point(122, 99);
            this.radioButtonMR.Name = "radioButtonMR";
            this.radioButtonMR.Size = new System.Drawing.Size(23, 23);
            this.radioButtonMR.TabIndex = 12;
            this.radioButtonMR.UseVisualStyleBackColor = true;
            // 
            // radioButtonBL
            // 
            this.radioButtonBL.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBL.Location = new System.Drawing.Point(64, 128);
            this.radioButtonBL.Name = "radioButtonBL";
            this.radioButtonBL.Size = new System.Drawing.Size(23, 23);
            this.radioButtonBL.TabIndex = 13;
            this.radioButtonBL.UseVisualStyleBackColor = true;
            // 
            // radioButtonBC
            // 
            this.radioButtonBC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBC.Location = new System.Drawing.Point(93, 128);
            this.radioButtonBC.Name = "radioButtonBC";
            this.radioButtonBC.Size = new System.Drawing.Size(23, 23);
            this.radioButtonBC.TabIndex = 14;
            this.radioButtonBC.UseVisualStyleBackColor = true;
            // 
            // radioButtonBR
            // 
            this.radioButtonBR.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBR.Location = new System.Drawing.Point(122, 128);
            this.radioButtonBR.Name = "radioButtonBR";
            this.radioButtonBR.Size = new System.Drawing.Size(23, 23);
            this.radioButtonBR.TabIndex = 15;
            this.radioButtonBR.UseVisualStyleBackColor = true;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(24, 15);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(34, 15);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "宽度:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(24, 44);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(34, 15);
            this.labelHeight.TabIndex = 3;
            this.labelHeight.Text = "高度:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWidth.Location = new System.Drawing.Point(64, 12);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(119, 23);
            this.textBoxWidth.TabIndex = 1;
            this.textBoxWidth.Text = "640";
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeight.Location = new System.Drawing.Point(64, 41);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(119, 23);
            this.textBoxHeight.TabIndex = 4;
            this.textBoxHeight.Text = "480";
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxHeight.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInteger_Validating);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(64, 194);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(145, 194);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelPixel1
            // 
            this.labelPixel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel1.AutoSize = true;
            this.labelPixel1.Location = new System.Drawing.Point(189, 15);
            this.labelPixel1.Name = "labelPixel1";
            this.labelPixel1.Size = new System.Drawing.Size(31, 15);
            this.labelPixel1.TabIndex = 2;
            this.labelPixel1.Text = "像素";
            // 
            // labelPixel
            // 
            this.labelPixel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixel.AutoSize = true;
            this.labelPixel.Location = new System.Drawing.Point(189, 44);
            this.labelPixel.Name = "labelPixel";
            this.labelPixel.Size = new System.Drawing.Size(31, 15);
            this.labelPixel.TabIndex = 5;
            this.labelPixel.Text = "像素";
            // 
            // labelSplitter
            // 
            this.labelSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSplitter.Location = new System.Drawing.Point(12, 186);
            this.labelSplitter.Margin = new System.Windows.Forms.Padding(3);
            this.labelSplitter.Name = "labelSplitter";
            this.labelSplitter.Size = new System.Drawing.Size(208, 2);
            this.labelSplitter.TabIndex = 18;
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(24, 73);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(34, 15);
            this.labelPosition.TabIndex = 6;
            this.labelPosition.Text = "定位:";
            // 
            // labelFillColor
            // 
            this.labelFillColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFillColor.BackColor = System.Drawing.Color.White;
            this.labelFillColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelFillColor.Location = new System.Drawing.Point(64, 157);
            this.labelFillColor.Margin = new System.Windows.Forms.Padding(3);
            this.labelFillColor.Name = "labelFillColor";
            this.labelFillColor.Size = new System.Drawing.Size(119, 23);
            this.labelFillColor.TabIndex = 17;
            this.labelFillColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelFillColor.Click += new System.EventHandler(this.labelFillColor_Click);
            // 
            // labelFillColorLabel
            // 
            this.labelFillColorLabel.AutoSize = true;
            this.labelFillColorLabel.Location = new System.Drawing.Point(12, 161);
            this.labelFillColorLabel.Name = "labelFillColorLabel";
            this.labelFillColorLabel.Size = new System.Drawing.Size(46, 15);
            this.labelFillColorLabel.TabIndex = 16;
            this.labelFillColorLabel.Text = "填充色:";
            // 
            // ImageCutPluginForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(232, 229);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelSplitter);
            this.Controls.Add(this.labelFillColor);
            this.Controls.Add(this.labelFillColorLabel);
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
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelPixel1);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelWidth);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageCutPluginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "裁剪";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonTL;
        private System.Windows.Forms.RadioButton radioButtonTC;
        private System.Windows.Forms.RadioButton radioButtonTR;
        private System.Windows.Forms.RadioButton radioButtonML;
        private System.Windows.Forms.RadioButton radioButtonMC;
        private System.Windows.Forms.RadioButton radioButtonMR;
        private System.Windows.Forms.RadioButton radioButtonBL;
        private System.Windows.Forms.RadioButton radioButtonBC;
        private System.Windows.Forms.RadioButton radioButtonBR;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPixel1;
        private System.Windows.Forms.Label labelPixel;
        private System.Windows.Forms.Label labelSplitter;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelFillColor;
        private System.Windows.Forms.Label labelFillColorLabel;
        private System.Windows.Forms.ColorDialog colorDialogMain;


    }
}
