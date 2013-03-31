namespace AutoWakeUp
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
            this.labelWakeUpAt = new System.Windows.Forms.Label();
            this.dateTimePickerWakeUpTime = new System.Windows.Forms.DateTimePicker();
            this.labelThenRun = new System.Windows.Forms.Label();
            this.textBoxRunProgram = new System.Windows.Forms.TextBox();
            this.buttonSelectProgram = new System.Windows.Forms.Button();
            this.labelWithParameter = new System.Windows.Forms.Label();
            this.textBoxRunProgramParameter = new System.Windows.Forms.TextBox();
            this.labelWaitUntil = new System.Windows.Forms.Label();
            this.dateTimePickerWaitUntilTime = new System.Windows.Forms.DateTimePicker();
            this.labelCommaThen = new System.Windows.Forms.Label();
            this.comboBoxDoStuff = new System.Windows.Forms.ComboBox();
            this.labelNow = new System.Windows.Forms.Label();
            this.buttonGoToSleepAndWait = new System.Windows.Forms.Button();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // labelWakeUpAt
            // 
            this.labelWakeUpAt.AutoSize = true;
            this.labelWakeUpAt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelWakeUpAt.Location = new System.Drawing.Point(32, 15);
            this.labelWakeUpAt.Name = "labelWakeUpAt";
            this.labelWakeUpAt.Size = new System.Drawing.Size(72, 15);
            this.labelWakeUpAt.TabIndex = 0;
            this.labelWakeUpAt.Text = "Wake Up At:";
            // 
            // dateTimePickerWakeUpTime
            // 
            this.dateTimePickerWakeUpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerWakeUpTime.Location = new System.Drawing.Point(110, 12);
            this.dateTimePickerWakeUpTime.Name = "dateTimePickerWakeUpTime";
            this.dateTimePickerWakeUpTime.ShowUpDown = true;
            this.dateTimePickerWakeUpTime.Size = new System.Drawing.Size(100, 23);
            this.dateTimePickerWakeUpTime.TabIndex = 1;
            // 
            // labelThenRun
            // 
            this.labelThenRun.AutoSize = true;
            this.labelThenRun.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelThenRun.Location = new System.Drawing.Point(44, 44);
            this.labelThenRun.Name = "labelThenRun";
            this.labelThenRun.Size = new System.Drawing.Size(61, 15);
            this.labelThenRun.TabIndex = 2;
            this.labelThenRun.Text = "Then Run:";
            // 
            // textBoxRunProgram
            // 
            this.textBoxRunProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRunProgram.Location = new System.Drawing.Point(110, 41);
            this.textBoxRunProgram.Name = "textBoxRunProgram";
            this.textBoxRunProgram.Size = new System.Drawing.Size(252, 23);
            this.textBoxRunProgram.TabIndex = 3;
            // 
            // buttonSelectProgram
            // 
            this.buttonSelectProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectProgram.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSelectProgram.Location = new System.Drawing.Point(368, 41);
            this.buttonSelectProgram.Name = "buttonSelectProgram";
            this.buttonSelectProgram.Size = new System.Drawing.Size(23, 23);
            this.buttonSelectProgram.TabIndex = 4;
            this.buttonSelectProgram.Text = "...";
            this.buttonSelectProgram.UseVisualStyleBackColor = true;
            this.buttonSelectProgram.Click += new System.EventHandler(this.buttonSelectProgram_Click);
            // 
            // labelWithParameter
            // 
            this.labelWithParameter.AutoSize = true;
            this.labelWithParameter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelWithParameter.Location = new System.Drawing.Point(12, 73);
            this.labelWithParameter.Name = "labelWithParameter";
            this.labelWithParameter.Size = new System.Drawing.Size(92, 15);
            this.labelWithParameter.TabIndex = 5;
            this.labelWithParameter.Text = "With Parameter:";
            // 
            // textBoxRunProgramParameter
            // 
            this.textBoxRunProgramParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRunProgramParameter.Location = new System.Drawing.Point(110, 70);
            this.textBoxRunProgramParameter.Name = "textBoxRunProgramParameter";
            this.textBoxRunProgramParameter.Size = new System.Drawing.Size(252, 23);
            this.textBoxRunProgramParameter.TabIndex = 6;
            // 
            // labelWaitUntil
            // 
            this.labelWaitUntil.AutoSize = true;
            this.labelWaitUntil.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelWaitUntil.Location = new System.Drawing.Point(43, 102);
            this.labelWaitUntil.Name = "labelWaitUntil";
            this.labelWaitUntil.Size = new System.Drawing.Size(62, 15);
            this.labelWaitUntil.TabIndex = 7;
            this.labelWaitUntil.Text = "Wait Until:";
            // 
            // dateTimePickerWaitUntilTime
            // 
            this.dateTimePickerWaitUntilTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerWaitUntilTime.Location = new System.Drawing.Point(110, 99);
            this.dateTimePickerWaitUntilTime.Name = "dateTimePickerWaitUntilTime";
            this.dateTimePickerWaitUntilTime.ShowUpDown = true;
            this.dateTimePickerWaitUntilTime.Size = new System.Drawing.Size(100, 23);
            this.dateTimePickerWaitUntilTime.TabIndex = 8;
            // 
            // labelCommaThen
            // 
            this.labelCommaThen.AutoSize = true;
            this.labelCommaThen.Location = new System.Drawing.Point(216, 102);
            this.labelCommaThen.Name = "labelCommaThen";
            this.labelCommaThen.Size = new System.Drawing.Size(40, 15);
            this.labelCommaThen.TabIndex = 9;
            this.labelCommaThen.Text = ", Then";
            // 
            // comboBoxDoStuff
            // 
            this.comboBoxDoStuff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDoStuff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDoStuff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxDoStuff.FormattingEnabled = true;
            this.comboBoxDoStuff.Items.AddRange(new object[] {
            "Do Nothing",
            "Shutdown",
            "Sleep"});
            this.comboBoxDoStuff.Location = new System.Drawing.Point(262, 99);
            this.comboBoxDoStuff.Name = "comboBoxDoStuff";
            this.comboBoxDoStuff.Size = new System.Drawing.Size(100, 23);
            this.comboBoxDoStuff.TabIndex = 10;
            // 
            // labelNow
            // 
            this.labelNow.AutoSize = true;
            this.labelNow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelNow.Location = new System.Drawing.Point(69, 131);
            this.labelNow.Name = "labelNow";
            this.labelNow.Size = new System.Drawing.Size(35, 15);
            this.labelNow.TabIndex = 11;
            this.labelNow.Text = "Now:";
            // 
            // buttonGoToSleepAndWait
            // 
            this.buttonGoToSleepAndWait.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonGoToSleepAndWait.Location = new System.Drawing.Point(110, 128);
            this.buttonGoToSleepAndWait.Name = "buttonGoToSleepAndWait";
            this.buttonGoToSleepAndWait.Size = new System.Drawing.Size(140, 23);
            this.buttonGoToSleepAndWait.TabIndex = 12;
            this.buttonGoToSleepAndWait.Text = "Go To Sleep And Wait";
            this.buttonGoToSleepAndWait.UseVisualStyleBackColor = true;
            this.buttonGoToSleepAndWait.Click += new System.EventHandler(this.buttonGoToSleepAndWait_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 163);
            this.Controls.Add(this.buttonGoToSleepAndWait);
            this.Controls.Add(this.labelNow);
            this.Controls.Add(this.comboBoxDoStuff);
            this.Controls.Add(this.labelCommaThen);
            this.Controls.Add(this.dateTimePickerWaitUntilTime);
            this.Controls.Add(this.labelWaitUntil);
            this.Controls.Add(this.textBoxRunProgramParameter);
            this.Controls.Add(this.labelWithParameter);
            this.Controls.Add(this.buttonSelectProgram);
            this.Controls.Add(this.textBoxRunProgram);
            this.Controls.Add(this.labelThenRun);
            this.Controls.Add(this.dateTimePickerWakeUpTime);
            this.Controls.Add(this.labelWakeUpAt);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Wake Up";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelWakeUpAt;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.DateTimePicker dateTimePickerWakeUpTime;
        private System.Windows.Forms.Label labelThenRun;
        private System.Windows.Forms.TextBox textBoxRunProgram;
        private System.Windows.Forms.Button buttonSelectProgram;
        private System.Windows.Forms.Label labelWithParameter;
        private System.Windows.Forms.TextBox textBoxRunProgramParameter;
        private System.Windows.Forms.Button buttonGoToSleepAndWait;
        private System.Windows.Forms.Label labelNow;
        private System.Windows.Forms.ComboBox comboBoxDoStuff;
        private System.Windows.Forms.Label labelCommaThen;
        private System.Windows.Forms.DateTimePicker dateTimePickerWaitUntilTime;
        private System.Windows.Forms.Label labelWaitUntil;
    }
}

