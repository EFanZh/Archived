namespace AutoMoveFiles
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
            this.components = new System.ComponentModel.Container();
            this.labelSourceFolder = new System.Windows.Forms.Label();
            this.textBoxSourceFolder = new System.Windows.Forms.TextBox();
            this.buttonSourceFolder = new System.Windows.Forms.Button();
            this.labelDestinationFolder = new System.Windows.Forms.Label();
            this.textBoxDestinationFolder = new System.Windows.Forms.TextBox();
            this.buttonDestinationFolder = new System.Windows.Forms.Button();
            this.labelExcludeRegex = new System.Windows.Forms.Label();
            this.textBoxExcludeRegex = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.folderBrowserDialogMain = new System.Windows.Forms.FolderBrowserDialog();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelSourceFolder
            // 
            this.labelSourceFolder.AutoSize = true;
            this.labelSourceFolder.Location = new System.Drawing.Point(36, 15);
            this.labelSourceFolder.Name = "labelSourceFolder";
            this.labelSourceFolder.Size = new System.Drawing.Size(82, 15);
            this.labelSourceFolder.TabIndex = 0;
            this.labelSourceFolder.Text = "Source Folder:";
            // 
            // textBoxSourceFolder
            // 
            this.textBoxSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSourceFolder.Location = new System.Drawing.Point(124, 12);
            this.textBoxSourceFolder.Name = "textBoxSourceFolder";
            this.textBoxSourceFolder.Size = new System.Drawing.Size(195, 23);
            this.textBoxSourceFolder.TabIndex = 1;
            // 
            // buttonSourceFolder
            // 
            this.buttonSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSourceFolder.Location = new System.Drawing.Point(325, 12);
            this.buttonSourceFolder.Name = "buttonSourceFolder";
            this.buttonSourceFolder.Size = new System.Drawing.Size(23, 23);
            this.buttonSourceFolder.TabIndex = 2;
            this.buttonSourceFolder.Text = "...";
            this.buttonSourceFolder.UseVisualStyleBackColor = true;
            // 
            // labelDestinationFolder
            // 
            this.labelDestinationFolder.AutoSize = true;
            this.labelDestinationFolder.Location = new System.Drawing.Point(12, 44);
            this.labelDestinationFolder.Name = "labelDestinationFolder";
            this.labelDestinationFolder.Size = new System.Drawing.Size(106, 15);
            this.labelDestinationFolder.TabIndex = 3;
            this.labelDestinationFolder.Text = "Destination Folder:";
            // 
            // textBoxDestinationFolder
            // 
            this.textBoxDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDestinationFolder.Location = new System.Drawing.Point(124, 41);
            this.textBoxDestinationFolder.Name = "textBoxDestinationFolder";
            this.textBoxDestinationFolder.Size = new System.Drawing.Size(195, 23);
            this.textBoxDestinationFolder.TabIndex = 4;
            // 
            // buttonDestinationFolder
            // 
            this.buttonDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDestinationFolder.Location = new System.Drawing.Point(325, 41);
            this.buttonDestinationFolder.Name = "buttonDestinationFolder";
            this.buttonDestinationFolder.Size = new System.Drawing.Size(23, 23);
            this.buttonDestinationFolder.TabIndex = 5;
            this.buttonDestinationFolder.Text = "...";
            this.buttonDestinationFolder.UseVisualStyleBackColor = true;
            // 
            // labelExcludeRegex
            // 
            this.labelExcludeRegex.AutoSize = true;
            this.labelExcludeRegex.Location = new System.Drawing.Point(34, 73);
            this.labelExcludeRegex.Name = "labelExcludeRegex";
            this.labelExcludeRegex.Size = new System.Drawing.Size(84, 15);
            this.labelExcludeRegex.TabIndex = 6;
            this.labelExcludeRegex.Text = "Exclude Regex:";
            // 
            // textBoxExcludeRegex
            // 
            this.textBoxExcludeRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExcludeRegex.Location = new System.Drawing.Point(124, 70);
            this.textBoxExcludeRegex.Name = "textBoxExcludeRegex";
            this.textBoxExcludeRegex.Size = new System.Drawing.Size(195, 23);
            this.textBoxExcludeRegex.TabIndex = 7;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonStart.Location = new System.Drawing.Point(142, 99);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(76, 23);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 134);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxExcludeRegex);
            this.Controls.Add(this.labelExcludeRegex);
            this.Controls.Add(this.buttonDestinationFolder);
            this.Controls.Add(this.textBoxDestinationFolder);
            this.Controls.Add(this.labelDestinationFolder);
            this.Controls.Add(this.buttonSourceFolder);
            this.Controls.Add(this.textBoxSourceFolder);
            this.Controls.Add(this.labelSourceFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Move Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSourceFolder;
        private System.Windows.Forms.TextBox textBoxSourceFolder;
        private System.Windows.Forms.Button buttonSourceFolder;
        private System.Windows.Forms.Label labelDestinationFolder;
        private System.Windows.Forms.TextBox textBoxDestinationFolder;
        private System.Windows.Forms.Button buttonDestinationFolder;
        private System.Windows.Forms.Label labelExcludeRegex;
        private System.Windows.Forms.TextBox textBoxExcludeRegex;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMain;
        private System.Windows.Forms.Timer timerMain;
    }
}

