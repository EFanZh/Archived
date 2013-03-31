namespace AutoRunCommand
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
            this.labelFileExists = new System.Windows.Forms.Label();
            this.textBoxFileExists = new System.Windows.Forms.TextBox();
            this.buttonFileExists = new System.Windows.Forms.Button();
            this.labelRunCommand = new System.Windows.Forms.Label();
            this.textBoxRunCommand = new System.Windows.Forms.TextBox();
            this.buttonRunCommand = new System.Windows.Forms.Button();
            this.labelWithParameter = new System.Windows.Forms.Label();
            this.textBoxRunCommandParameter = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelFileExists
            // 
            this.labelFileExists.AutoSize = true;
            this.labelFileExists.Location = new System.Drawing.Point(35, 15);
            this.labelFileExists.Name = "labelFileExists";
            this.labelFileExists.Size = new System.Drawing.Size(69, 15);
            this.labelFileExists.TabIndex = 0;
            this.labelFileExists.Text = "If File Exists:";
            // 
            // textBoxFileExists
            // 
            this.textBoxFileExists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileExists.Location = new System.Drawing.Point(110, 12);
            this.textBoxFileExists.Name = "textBoxFileExists";
            this.textBoxFileExists.Size = new System.Drawing.Size(209, 23);
            this.textBoxFileExists.TabIndex = 1;
            // 
            // buttonFileExists
            // 
            this.buttonFileExists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFileExists.Location = new System.Drawing.Point(325, 12);
            this.buttonFileExists.Name = "buttonFileExists";
            this.buttonFileExists.Size = new System.Drawing.Size(23, 23);
            this.buttonFileExists.TabIndex = 2;
            this.buttonFileExists.Text = "...";
            this.buttonFileExists.UseVisualStyleBackColor = true;
            // 
            // labelRunCommand
            // 
            this.labelRunCommand.AutoSize = true;
            this.labelRunCommand.Location = new System.Drawing.Point(13, 45);
            this.labelRunCommand.Name = "labelRunCommand";
            this.labelRunCommand.Size = new System.Drawing.Size(91, 15);
            this.labelRunCommand.TabIndex = 3;
            this.labelRunCommand.Text = "Run Command:";
            // 
            // textBoxRunCommand
            // 
            this.textBoxRunCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRunCommand.Location = new System.Drawing.Point(110, 41);
            this.textBoxRunCommand.Name = "textBoxRunCommand";
            this.textBoxRunCommand.Size = new System.Drawing.Size(209, 23);
            this.textBoxRunCommand.TabIndex = 4;
            // 
            // buttonRunCommand
            // 
            this.buttonRunCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRunCommand.Location = new System.Drawing.Point(325, 41);
            this.buttonRunCommand.Name = "buttonRunCommand";
            this.buttonRunCommand.Size = new System.Drawing.Size(23, 23);
            this.buttonRunCommand.TabIndex = 5;
            this.buttonRunCommand.Text = "...";
            this.buttonRunCommand.UseVisualStyleBackColor = true;
            // 
            // labelWithParameter
            // 
            this.labelWithParameter.AutoSize = true;
            this.labelWithParameter.Location = new System.Drawing.Point(12, 73);
            this.labelWithParameter.Name = "labelWithParameter";
            this.labelWithParameter.Size = new System.Drawing.Size(92, 15);
            this.labelWithParameter.TabIndex = 6;
            this.labelWithParameter.Text = "With Parameter:";
            // 
            // textBoxRunCommandParameter
            // 
            this.textBoxRunCommandParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRunCommandParameter.Location = new System.Drawing.Point(110, 70);
            this.textBoxRunCommandParameter.Name = "textBoxRunCommandParameter";
            this.textBoxRunCommandParameter.Size = new System.Drawing.Size(209, 23);
            this.textBoxRunCommandParameter.TabIndex = 7;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonStart.Location = new System.Drawing.Point(143, 99);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
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
            this.Controls.Add(this.textBoxRunCommandParameter);
            this.Controls.Add(this.labelWithParameter);
            this.Controls.Add(this.buttonRunCommand);
            this.Controls.Add(this.textBoxRunCommand);
            this.Controls.Add(this.labelRunCommand);
            this.Controls.Add(this.buttonFileExists);
            this.Controls.Add(this.textBoxFileExists);
            this.Controls.Add(this.labelFileExists);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Run Command";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFileExists;
        private System.Windows.Forms.TextBox textBoxFileExists;
        private System.Windows.Forms.Button buttonFileExists;
        private System.Windows.Forms.Label labelRunCommand;
        private System.Windows.Forms.TextBox textBoxRunCommand;
        private System.Windows.Forms.Button buttonRunCommand;
        private System.Windows.Forms.Label labelWithParameter;
        private System.Windows.Forms.TextBox textBoxRunCommandParameter;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.Timer timerMain;
    }
}

