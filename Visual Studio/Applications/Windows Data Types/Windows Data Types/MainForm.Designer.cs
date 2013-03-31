namespace WindowsDataTypes
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
            this.checkBoxWin64 = new System.Windows.Forms.CheckBox();
            this.checkBoxMIX86 = new System.Windows.Forms.CheckBox();
            this.checkBoxMSCVer = new System.Windows.Forms.CheckBox();
            this.checkBoxWinVer = new System.Windows.Forms.CheckBox();
            this.checkBoxUnicode = new System.Windows.Forms.CheckBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.listViewResult = new System.Windows.Forms.ListView();
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEquivalentType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderResolvedType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRawData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelControl = new System.Windows.Forms.Panel();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxWin64
            // 
            this.checkBoxWin64.AutoSize = true;
            this.checkBoxWin64.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxWin64.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxWin64.Font = new System.Drawing.Font("Consolas", 9F);
            this.checkBoxWin64.Location = new System.Drawing.Point(4, 0);
            this.checkBoxWin64.Name = "checkBoxWin64";
            this.checkBoxWin64.Size = new System.Drawing.Size(74, 22);
            this.checkBoxWin64.TabIndex = 0;
            this.checkBoxWin64.Text = "_WIN64";
            this.checkBoxWin64.UseVisualStyleBackColor = true;
            this.checkBoxWin64.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // checkBoxMIX86
            // 
            this.checkBoxMIX86.AutoSize = true;
            this.checkBoxMIX86.Checked = true;
            this.checkBoxMIX86.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMIX86.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxMIX86.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxMIX86.Font = new System.Drawing.Font("Consolas", 9F);
            this.checkBoxMIX86.Location = new System.Drawing.Point(78, 0);
            this.checkBoxMIX86.Name = "checkBoxMIX86";
            this.checkBoxMIX86.Size = new System.Drawing.Size(81, 22);
            this.checkBoxMIX86.TabIndex = 1;
            this.checkBoxMIX86.Text = "_M_IX86";
            this.checkBoxMIX86.UseVisualStyleBackColor = true;
            this.checkBoxMIX86.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // checkBoxMSCVer
            // 
            this.checkBoxMSCVer.AutoSize = true;
            this.checkBoxMSCVer.Checked = true;
            this.checkBoxMSCVer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMSCVer.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxMSCVer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxMSCVer.Font = new System.Drawing.Font("Consolas", 9F);
            this.checkBoxMSCVer.Location = new System.Drawing.Point(159, 0);
            this.checkBoxMSCVer.Name = "checkBoxMSCVer";
            this.checkBoxMSCVer.Size = new System.Drawing.Size(144, 22);
            this.checkBoxMSCVer.TabIndex = 2;
            this.checkBoxMSCVer.Text = "_MSC_VER >= 1300";
            this.checkBoxMSCVer.UseVisualStyleBackColor = true;
            this.checkBoxMSCVer.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // checkBoxWinVer
            // 
            this.checkBoxWinVer.AutoSize = true;
            this.checkBoxWinVer.Checked = true;
            this.checkBoxWinVer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWinVer.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxWinVer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxWinVer.Font = new System.Drawing.Font("Consolas", 9F);
            this.checkBoxWinVer.Location = new System.Drawing.Point(303, 0);
            this.checkBoxWinVer.Name = "checkBoxWinVer";
            this.checkBoxWinVer.Size = new System.Drawing.Size(144, 22);
            this.checkBoxWinVer.TabIndex = 3;
            this.checkBoxWinVer.Text = "WINVER >= 0x0500";
            this.checkBoxWinVer.UseVisualStyleBackColor = true;
            this.checkBoxWinVer.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // checkBoxUnicode
            // 
            this.checkBoxUnicode.AutoSize = true;
            this.checkBoxUnicode.Checked = true;
            this.checkBoxUnicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUnicode.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxUnicode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxUnicode.Font = new System.Drawing.Font("Consolas", 9F);
            this.checkBoxUnicode.Location = new System.Drawing.Point(447, 0);
            this.checkBoxUnicode.Name = "checkBoxUnicode";
            this.checkBoxUnicode.Size = new System.Drawing.Size(81, 22);
            this.checkBoxUnicode.TabIndex = 4;
            this.checkBoxUnicode.Text = "UNICODE";
            this.checkBoxUnicode.UseVisualStyleBackColor = true;
            this.checkBoxUnicode.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilter.Font = new System.Drawing.Font("Consolas", 9F);
            this.textBoxFilter.Location = new System.Drawing.Point(528, 0);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(416, 22);
            this.textBoxFilter.TabIndex = 5;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // listViewResult
            // 
            this.listViewResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderType,
            this.columnHeaderEquivalentType,
            this.columnHeaderResolvedType,
            this.columnHeaderRawData});
            this.listViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewResult.Font = new System.Drawing.Font("Consolas", 9F);
            this.listViewResult.FullRowSelect = true;
            this.listViewResult.Location = new System.Drawing.Point(0, 22);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.Size = new System.Drawing.Size(944, 499);
            this.listViewResult.TabIndex = 6;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            // 
            // columnHeaderEquivalentType
            // 
            this.columnHeaderEquivalentType.Text = "Equivalent Type";
            // 
            // columnHeaderResolvedType
            // 
            this.columnHeaderResolvedType.Text = "Resolved Type";
            // 
            // columnHeaderRawData
            // 
            this.columnHeaderRawData.Text = "Raw Data";
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.textBoxFilter);
            this.panelControl.Controls.Add(this.checkBoxUnicode);
            this.panelControl.Controls.Add(this.checkBoxWinVer);
            this.panelControl.Controls.Add(this.checkBoxMSCVer);
            this.panelControl.Controls.Add(this.checkBoxMIX86);
            this.panelControl.Controls.Add(this.checkBoxWin64);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.panelControl.Size = new System.Drawing.Size(944, 22);
            this.panelControl.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 521);
            this.Controls.Add(this.listViewResult);
            this.Controls.Add(this.panelControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Data Types";
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxWin64;
        private System.Windows.Forms.CheckBox checkBoxMIX86;
        private System.Windows.Forms.CheckBox checkBoxMSCVer;
        private System.Windows.Forms.CheckBox checkBoxWinVer;
        private System.Windows.Forms.CheckBox checkBoxUnicode;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.ListView listViewResult;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderEquivalentType;
        private System.Windows.Forms.ColumnHeader columnHeaderResolvedType;
        private System.Windows.Forms.ColumnHeader columnHeaderRawData;
    }
}

