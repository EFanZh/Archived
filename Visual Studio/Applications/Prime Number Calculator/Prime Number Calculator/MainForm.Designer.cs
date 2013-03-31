namespace PrimeNumberCalculator
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
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.checkBoxScrollToBottom = new System.Windows.Forms.CheckBox();
            this.listViewPrimeNumberList = new System.Windows.Forms.ListView();
            this.columnHeaderPrimeIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPrimeValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(12, 12);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculate.TabIndex = 0;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(93, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.AutoEllipsis = true;
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInfo.Location = new System.Drawing.Point(174, 15);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(413, 17);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "Ready.";
            // 
            // checkBoxScrollToBottom
            // 
            this.checkBoxScrollToBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxScrollToBottom.AutoSize = true;
            this.checkBoxScrollToBottom.Checked = true;
            this.checkBoxScrollToBottom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxScrollToBottom.Location = new System.Drawing.Point(593, 15);
            this.checkBoxScrollToBottom.Name = "checkBoxScrollToBottom";
            this.checkBoxScrollToBottom.Size = new System.Drawing.Size(115, 19);
            this.checkBoxScrollToBottom.TabIndex = 3;
            this.checkBoxScrollToBottom.Text = "Scroll To Bottom";
            this.checkBoxScrollToBottom.UseVisualStyleBackColor = true;
            // 
            // listViewPrimeNumberList
            // 
            this.listViewPrimeNumberList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPrimeNumberList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPrimeIndex,
            this.columnHeaderPrimeValue});
            this.listViewPrimeNumberList.FullRowSelect = true;
            this.listViewPrimeNumberList.GridLines = true;
            this.listViewPrimeNumberList.Location = new System.Drawing.Point(12, 41);
            this.listViewPrimeNumberList.Name = "listViewPrimeNumberList";
            this.listViewPrimeNumberList.Size = new System.Drawing.Size(696, 351);
            this.listViewPrimeNumberList.TabIndex = 4;
            this.listViewPrimeNumberList.UseCompatibleStateImageBehavior = false;
            this.listViewPrimeNumberList.View = System.Windows.Forms.View.Details;
            this.listViewPrimeNumberList.VirtualMode = true;
            this.listViewPrimeNumberList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewPrimeNumberList_RetrieveVirtualItem);
            // 
            // columnHeaderPrimeIndex
            // 
            this.columnHeaderPrimeIndex.Text = "Index";
            // 
            // columnHeaderPrimeValue
            // 
            this.columnHeaderPrimeValue.Text = "Value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 404);
            this.Controls.Add(this.listViewPrimeNumberList);
            this.Controls.Add(this.checkBoxScrollToBottom);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCalculate);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prime Number Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.ListView listViewPrimeNumberList;
        private System.Windows.Forms.ColumnHeader columnHeaderPrimeIndex;
        private System.Windows.Forms.ColumnHeader columnHeaderPrimeValue;
        private System.Windows.Forms.CheckBox checkBoxScrollToBottom;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelInfo;
    }
}

