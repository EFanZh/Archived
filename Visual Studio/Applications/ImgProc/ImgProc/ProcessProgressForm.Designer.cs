namespace ImgProc
{
    partial class ProcessProgressForm
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
            this.listViewImageList = new System.Windows.Forms.ListView();
            this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBarProgress = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSplitter1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewImageList
            // 
            this.listViewImageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewImageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPath,
            this.columnHeaderState,
            this.columnHeaderProgress});
            this.listViewImageList.FullRowSelect = true;
            this.listViewImageList.Location = new System.Drawing.Point(12, 12);
            this.listViewImageList.Name = "listViewImageList";
            this.listViewImageList.Size = new System.Drawing.Size(439, 255);
            this.listViewImageList.TabIndex = 0;
            this.listViewImageList.UseCompatibleStateImageBehavior = false;
            this.listViewImageList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "路径";
            // 
            // columnHeaderState
            // 
            this.columnHeaderState.Text = "状态";
            // 
            // columnHeaderProgress
            // 
            this.columnHeaderProgress.Text = "进度";
            // 
            // progressBarProgress
            // 
            this.progressBarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarProgress.Location = new System.Drawing.Point(12, 287);
            this.progressBarProgress.Name = "progressBarProgress";
            this.progressBarProgress.Size = new System.Drawing.Size(520, 23);
            this.progressBarProgress.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(457, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelSplitter1
            // 
            this.labelSplitter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSplitter1.Location = new System.Drawing.Point(12, 276);
            this.labelSplitter1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.labelSplitter1.Name = "labelSplitter1";
            this.labelSplitter1.Size = new System.Drawing.Size(520, 2);
            this.labelSplitter1.TabIndex = 2;
            // 
            // ProcessProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 322);
            this.Controls.Add(this.progressBarProgress);
            this.Controls.Add(this.labelSplitter1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.listViewImageList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "ProcessProgressForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "处理进度";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSplitter1;
        private System.Windows.Forms.ColumnHeader columnHeaderPath;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
        private System.Windows.Forms.ListView listViewImageList;
        private System.Windows.Forms.ProgressBar progressBarProgress;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColumnHeader columnHeaderProgress;
    }
}