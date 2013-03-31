namespace MyColorDialog
{
    partial class MyColorDialog
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
            this.panelColorSelector1 = new System.Windows.Forms.Panel();
            this.panelColorSelector2 = new System.Windows.Forms.Panel();
            this.panelOldColorNewColor = new System.Windows.Forms.Panel();
            this.radioButtonHSBH = new System.Windows.Forms.RadioButton();
            this.radioButtonHSBS = new System.Windows.Forms.RadioButton();
            this.radioButtonHSBB = new System.Windows.Forms.RadioButton();
            this.numericUpDownRGBB = new System.Windows.Forms.NumericUpDown();
            this.radioButtonRGBR = new System.Windows.Forms.RadioButton();
            this.radioButtonRGBG = new System.Windows.Forms.RadioButton();
            this.radioButtonRGBB = new System.Windows.Forms.RadioButton();
            this.numericUpDownRGBG = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRGBR = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHSBB = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHSBS = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHSBH = new System.Windows.Forms.NumericUpDown();
            this.textBoxHexColor = new System.Windows.Forms.TextBox();
            this.labelHexColor = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRGBB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRGBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRGBR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHSBB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHSBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHSBH)).BeginInit();
            this.SuspendLayout();
            // 
            // panelColorSelector1
            // 
            this.panelColorSelector1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColorSelector1.Location = new System.Drawing.Point(12, 12);
            this.panelColorSelector1.Name = "panelColorSelector1";
            this.panelColorSelector1.Size = new System.Drawing.Size(260, 260);
            this.panelColorSelector1.TabIndex = 0;
            // 
            // panelColorSelector2
            // 
            this.panelColorSelector2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColorSelector2.Location = new System.Drawing.Point(278, 12);
            this.panelColorSelector2.Name = "panelColorSelector2";
            this.panelColorSelector2.Size = new System.Drawing.Size(24, 260);
            this.panelColorSelector2.TabIndex = 1;
            // 
            // panelOldColorNewColor
            // 
            this.panelOldColorNewColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOldColorNewColor.Location = new System.Drawing.Point(308, 12);
            this.panelOldColorNewColor.Name = "panelOldColorNewColor";
            this.panelOldColorNewColor.Size = new System.Drawing.Size(91, 57);
            this.panelOldColorNewColor.TabIndex = 2;
            // 
            // radioButtonHSBH
            // 
            this.radioButtonHSBH.AutoSize = true;
            this.radioButtonHSBH.Location = new System.Drawing.Point(308, 75);
            this.radioButtonHSBH.Name = "radioButtonHSBH";
            this.radioButtonHSBH.Size = new System.Drawing.Size(37, 19);
            this.radioButtonHSBH.TabIndex = 3;
            this.radioButtonHSBH.TabStop = true;
            this.radioButtonHSBH.Text = "H:";
            this.radioButtonHSBH.UseVisualStyleBackColor = true;
            // 
            // radioButtonHSBS
            // 
            this.radioButtonHSBS.AutoSize = true;
            this.radioButtonHSBS.Location = new System.Drawing.Point(308, 104);
            this.radioButtonHSBS.Name = "radioButtonHSBS";
            this.radioButtonHSBS.Size = new System.Drawing.Size(34, 19);
            this.radioButtonHSBS.TabIndex = 5;
            this.radioButtonHSBS.TabStop = true;
            this.radioButtonHSBS.Text = "S:";
            this.radioButtonHSBS.UseVisualStyleBackColor = true;
            // 
            // radioButtonHSBB
            // 
            this.radioButtonHSBB.AutoSize = true;
            this.radioButtonHSBB.Location = new System.Drawing.Point(308, 133);
            this.radioButtonHSBB.Name = "radioButtonHSBB";
            this.radioButtonHSBB.Size = new System.Drawing.Size(35, 19);
            this.radioButtonHSBB.TabIndex = 7;
            this.radioButtonHSBB.TabStop = true;
            this.radioButtonHSBB.Text = "B:";
            this.radioButtonHSBB.UseVisualStyleBackColor = true;
            // 
            // numericUpDownRGBB
            // 
            this.numericUpDownRGBB.Location = new System.Drawing.Point(351, 220);
            this.numericUpDownRGBB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRGBB.Name = "numericUpDownRGBB";
            this.numericUpDownRGBB.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownRGBB.TabIndex = 14;
            this.numericUpDownRGBB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioButtonRGBR
            // 
            this.radioButtonRGBR.AutoSize = true;
            this.radioButtonRGBR.Location = new System.Drawing.Point(308, 162);
            this.radioButtonRGBR.Name = "radioButtonRGBR";
            this.radioButtonRGBR.Size = new System.Drawing.Size(35, 19);
            this.radioButtonRGBR.TabIndex = 9;
            this.radioButtonRGBR.TabStop = true;
            this.radioButtonRGBR.Text = "R:";
            this.radioButtonRGBR.UseVisualStyleBackColor = true;
            // 
            // radioButtonRGBG
            // 
            this.radioButtonRGBG.AutoSize = true;
            this.radioButtonRGBG.Location = new System.Drawing.Point(308, 191);
            this.radioButtonRGBG.Name = "radioButtonRGBG";
            this.radioButtonRGBG.Size = new System.Drawing.Size(36, 19);
            this.radioButtonRGBG.TabIndex = 11;
            this.radioButtonRGBG.TabStop = true;
            this.radioButtonRGBG.Text = "G:";
            this.radioButtonRGBG.UseVisualStyleBackColor = true;
            // 
            // radioButtonRGBB
            // 
            this.radioButtonRGBB.AutoSize = true;
            this.radioButtonRGBB.Location = new System.Drawing.Point(308, 220);
            this.radioButtonRGBB.Name = "radioButtonRGBB";
            this.radioButtonRGBB.Size = new System.Drawing.Size(35, 19);
            this.radioButtonRGBB.TabIndex = 13;
            this.radioButtonRGBB.TabStop = true;
            this.radioButtonRGBB.Text = "B:";
            this.radioButtonRGBB.UseVisualStyleBackColor = true;
            // 
            // numericUpDownRGBG
            // 
            this.numericUpDownRGBG.Location = new System.Drawing.Point(351, 191);
            this.numericUpDownRGBG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRGBG.Name = "numericUpDownRGBG";
            this.numericUpDownRGBG.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownRGBG.TabIndex = 12;
            this.numericUpDownRGBG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownRGBR
            // 
            this.numericUpDownRGBR.Location = new System.Drawing.Point(351, 162);
            this.numericUpDownRGBR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRGBR.Name = "numericUpDownRGBR";
            this.numericUpDownRGBR.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownRGBR.TabIndex = 10;
            this.numericUpDownRGBR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownHSBB
            // 
            this.numericUpDownHSBB.Location = new System.Drawing.Point(351, 133);
            this.numericUpDownHSBB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownHSBB.Name = "numericUpDownHSBB";
            this.numericUpDownHSBB.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownHSBB.TabIndex = 8;
            this.numericUpDownHSBB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownHSBS
            // 
            this.numericUpDownHSBS.Location = new System.Drawing.Point(351, 104);
            this.numericUpDownHSBS.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownHSBS.Name = "numericUpDownHSBS";
            this.numericUpDownHSBS.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownHSBS.TabIndex = 6;
            this.numericUpDownHSBS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownHSBH
            // 
            this.numericUpDownHSBH.Location = new System.Drawing.Point(351, 75);
            this.numericUpDownHSBH.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownHSBH.Name = "numericUpDownHSBH";
            this.numericUpDownHSBH.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownHSBH.TabIndex = 4;
            this.numericUpDownHSBH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxHexColor
            // 
            this.textBoxHexColor.Location = new System.Drawing.Point(351, 249);
            this.textBoxHexColor.Name = "textBoxHexColor";
            this.textBoxHexColor.Size = new System.Drawing.Size(48, 23);
            this.textBoxHexColor.TabIndex = 16;
            this.textBoxHexColor.Text = "666666";
            this.textBoxHexColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelHexColor
            // 
            this.labelHexColor.AutoSize = true;
            this.labelHexColor.Location = new System.Drawing.Point(308, 252);
            this.labelHexColor.Name = "labelHexColor";
            this.labelHexColor.Size = new System.Drawing.Size(14, 15);
            this.labelHexColor.TabIndex = 15;
            this.labelHexColor.Text = "#";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(405, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(405, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ColorSelectorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 284);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxHexColor);
            this.Controls.Add(this.labelHexColor);
            this.Controls.Add(this.numericUpDownRGBB);
            this.Controls.Add(this.radioButtonRGBB);
            this.Controls.Add(this.numericUpDownRGBG);
            this.Controls.Add(this.radioButtonRGBG);
            this.Controls.Add(this.numericUpDownRGBR);
            this.Controls.Add(this.radioButtonRGBR);
            this.Controls.Add(this.numericUpDownHSBB);
            this.Controls.Add(this.radioButtonHSBB);
            this.Controls.Add(this.numericUpDownHSBS);
            this.Controls.Add(this.radioButtonHSBS);
            this.Controls.Add(this.numericUpDownHSBH);
            this.Controls.Add(this.radioButtonHSBH);
            this.Controls.Add(this.panelOldColorNewColor);
            this.Controls.Add(this.panelColorSelector2);
            this.Controls.Add(this.panelColorSelector1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorSelectorDialog";
            this.Text = "Select Color";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRGBB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRGBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRGBR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHSBB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHSBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHSBH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelColorSelector1;
        private System.Windows.Forms.Panel panelColorSelector2;
        private System.Windows.Forms.Panel panelOldColorNewColor;
        private System.Windows.Forms.RadioButton radioButtonHSBH;
        private System.Windows.Forms.RadioButton radioButtonHSBS;
        private System.Windows.Forms.RadioButton radioButtonHSBB;
        private System.Windows.Forms.NumericUpDown numericUpDownRGBB;
        private System.Windows.Forms.RadioButton radioButtonRGBR;
        private System.Windows.Forms.RadioButton radioButtonRGBG;
        private System.Windows.Forms.RadioButton radioButtonRGBB;
        private System.Windows.Forms.NumericUpDown numericUpDownRGBG;
        private System.Windows.Forms.NumericUpDown numericUpDownRGBR;
        private System.Windows.Forms.NumericUpDown numericUpDownHSBB;
        private System.Windows.Forms.NumericUpDown numericUpDownHSBS;
        private System.Windows.Forms.NumericUpDown numericUpDownHSBH;
        private System.Windows.Forms.TextBox textBoxHexColor;
        private System.Windows.Forms.Label labelHexColor;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}