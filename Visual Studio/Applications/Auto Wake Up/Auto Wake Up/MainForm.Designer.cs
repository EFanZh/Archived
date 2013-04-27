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
            this.groupBoxWakeUp = new System.Windows.Forms.GroupBox();
            this.labelWakeUpTime = new System.Windows.Forms.Label();
            this.dateTimePickerWakeUpTime = new System.Windows.Forms.DateTimePicker();
            this.labelRunProgram = new System.Windows.Forms.Label();
            this.textBoxRunProgram = new System.Windows.Forms.TextBox();
            this.buttonSelectProgram = new System.Windows.Forms.Button();
            this.labelParameter = new System.Windows.Forms.Label();
            this.textBoxParameter = new System.Windows.Forms.TextBox();
            this.groupBoxRest = new System.Windows.Forms.GroupBox();
            this.labelRestTime = new System.Windows.Forms.Label();
            this.dateTimePickerRestTime = new System.Windows.Forms.DateTimePicker();
            this.labelOperation = new System.Windows.Forms.Label();
            this.comboBoxOperation = new System.Windows.Forms.ComboBox();
            this.buttonGoToSleepAndWait = new System.Windows.Forms.Button();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxWakeUp.SuspendLayout();
            this.groupBoxRest.SuspendLayout();
            this.SuspendLayout();

            //
            // groupBoxWakeUp
            //
            this.groupBoxWakeUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWakeUp.Controls.Add(this.textBoxParameter);
            this.groupBoxWakeUp.Controls.Add(this.labelParameter);
            this.groupBoxWakeUp.Controls.Add(this.buttonSelectProgram);
            this.groupBoxWakeUp.Controls.Add(this.textBoxRunProgram);
            this.groupBoxWakeUp.Controls.Add(this.labelRunProgram);
            this.groupBoxWakeUp.Controls.Add(this.dateTimePickerWakeUpTime);
            this.groupBoxWakeUp.Controls.Add(this.labelWakeUpTime);
            this.groupBoxWakeUp.Location = new System.Drawing.Point(12, 12);
            this.groupBoxWakeUp.Name = "groupBoxWakeUp";
            this.groupBoxWakeUp.Size = new System.Drawing.Size(424, 109);
            this.groupBoxWakeUp.TabIndex = 0;
            this.groupBoxWakeUp.TabStop = false;
            this.groupBoxWakeUp.Text = "Wake Up";

            //
            // labelWakeUpTime
            //
            this.labelWakeUpTime.AutoSize = true;
            this.labelWakeUpTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelWakeUpTime.Location = new System.Drawing.Point(6, 25);
            this.labelWakeUpTime.Name = "labelWakeUpTime";
            this.labelWakeUpTime.Size = new System.Drawing.Size(87, 15);
            this.labelWakeUpTime.TabIndex = 0;
            this.labelWakeUpTime.Text = "Wake Up Time:";

            //
            // dateTimePickerWakeUpTime
            //
            this.dateTimePickerWakeUpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerWakeUpTime.Location = new System.Drawing.Point(99, 22);
            this.dateTimePickerWakeUpTime.Name = "dateTimePickerWakeUpTime";
            this.dateTimePickerWakeUpTime.ShowUpDown = true;
            this.dateTimePickerWakeUpTime.Size = new System.Drawing.Size(100, 23);
            this.dateTimePickerWakeUpTime.TabIndex = 1;

            //
            // labelRunProgram
            //
            this.labelRunProgram.AutoSize = true;
            this.labelRunProgram.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelRunProgram.Location = new System.Drawing.Point(13, 54);
            this.labelRunProgram.Name = "labelRunProgram";
            this.labelRunProgram.Size = new System.Drawing.Size(80, 15);
            this.labelRunProgram.TabIndex = 2;
            this.labelRunProgram.Text = "Run Program:";

            //
            // textBoxRunProgram
            //
            this.textBoxRunProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRunProgram.Location = new System.Drawing.Point(99, 51);
            this.textBoxRunProgram.Name = "textBoxRunProgram";
            this.textBoxRunProgram.Size = new System.Drawing.Size(290, 23);
            this.textBoxRunProgram.TabIndex = 3;

            //
            // buttonSelectProgram
            //
            this.buttonSelectProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectProgram.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSelectProgram.Location = new System.Drawing.Point(395, 51);
            this.buttonSelectProgram.Name = "buttonSelectProgram";
            this.buttonSelectProgram.Size = new System.Drawing.Size(23, 23);
            this.buttonSelectProgram.TabIndex = 4;
            this.buttonSelectProgram.Text = "...";
            this.buttonSelectProgram.UseVisualStyleBackColor = true;
            this.buttonSelectProgram.Click += new System.EventHandler(this.buttonSelectProgram_Click);

            //
            // labelParameter
            //
            this.labelParameter.AutoSize = true;
            this.labelParameter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelParameter.Location = new System.Drawing.Point(29, 83);
            this.labelParameter.Name = "labelParameter";
            this.labelParameter.Size = new System.Drawing.Size(64, 15);
            this.labelParameter.TabIndex = 5;
            this.labelParameter.Text = "Parameter:";

            //
            // textBoxParameter
            //
            this.textBoxParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParameter.Location = new System.Drawing.Point(99, 80);
            this.textBoxParameter.Name = "textBoxParameter";
            this.textBoxParameter.Size = new System.Drawing.Size(290, 23);
            this.textBoxParameter.TabIndex = 6;

            //
            // groupBoxRest
            //
            this.groupBoxRest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRest.Controls.Add(this.comboBoxOperation);
            this.groupBoxRest.Controls.Add(this.labelOperation);
            this.groupBoxRest.Controls.Add(this.dateTimePickerRestTime);
            this.groupBoxRest.Controls.Add(this.labelRestTime);
            this.groupBoxRest.Location = new System.Drawing.Point(12, 127);
            this.groupBoxRest.Name = "groupBoxRest";
            this.groupBoxRest.Size = new System.Drawing.Size(424, 80);
            this.groupBoxRest.TabIndex = 1;
            this.groupBoxRest.TabStop = false;
            this.groupBoxRest.Text = "Rest";

            //
            // labelRestTime
            //
            this.labelRestTime.AutoSize = true;
            this.labelRestTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelRestTime.Location = new System.Drawing.Point(33, 25);
            this.labelRestTime.Name = "labelRestTime";
            this.labelRestTime.Size = new System.Drawing.Size(62, 15);
            this.labelRestTime.TabIndex = 0;
            this.labelRestTime.Text = "Rest Time:";

            //
            // dateTimePickerRestTime
            //
            this.dateTimePickerRestTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerRestTime.Location = new System.Drawing.Point(99, 22);
            this.dateTimePickerRestTime.Name = "dateTimePickerRestTime";
            this.dateTimePickerRestTime.ShowUpDown = true;
            this.dateTimePickerRestTime.Size = new System.Drawing.Size(100, 23);
            this.dateTimePickerRestTime.TabIndex = 1;

            //
            // labelOperation
            //
            this.labelOperation.AutoSize = true;
            this.labelOperation.Location = new System.Drawing.Point(29, 54);
            this.labelOperation.Name = "labelOperation";
            this.labelOperation.Size = new System.Drawing.Size(63, 15);
            this.labelOperation.TabIndex = 2;
            this.labelOperation.Text = "Operation:";

            //
            // comboBoxOperation
            //
            this.comboBoxOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxOperation.FormattingEnabled = true;
            this.comboBoxOperation.Items.AddRange(new object[] {
            "Do Nothing",
            "Shutdown",
            "Sleep"});
            this.comboBoxOperation.Location = new System.Drawing.Point(99, 51);
            this.comboBoxOperation.Name = "comboBoxOperation";
            this.comboBoxOperation.Size = new System.Drawing.Size(290, 23);
            this.comboBoxOperation.TabIndex = 3;

            //
            // buttonGoToSleepAndWait
            //
            this.buttonGoToSleepAndWait.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonGoToSleepAndWait.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonGoToSleepAndWait.Location = new System.Drawing.Point(154, 213);
            this.buttonGoToSleepAndWait.Name = "buttonGoToSleepAndWait";
            this.buttonGoToSleepAndWait.Size = new System.Drawing.Size(140, 23);
            this.buttonGoToSleepAndWait.TabIndex = 2;
            this.buttonGoToSleepAndWait.Text = "Go To Sleep And Wait";
            this.buttonGoToSleepAndWait.UseVisualStyleBackColor = true;
            this.buttonGoToSleepAndWait.Click += new System.EventHandler(this.buttonGoToSleepAndWait_Click);

            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 248);
            this.Controls.Add(this.buttonGoToSleepAndWait);
            this.Controls.Add(this.groupBoxRest);
            this.Controls.Add(this.groupBoxWakeUp);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Wake Up";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBoxWakeUp.ResumeLayout(false);
            this.groupBoxWakeUp.PerformLayout();
            this.groupBoxRest.ResumeLayout(false);
            this.groupBoxRest.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Label labelWakeUpTime;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.DateTimePicker dateTimePickerWakeUpTime;
        private System.Windows.Forms.Label labelRunProgram;
        private System.Windows.Forms.TextBox textBoxRunProgram;
        private System.Windows.Forms.Button buttonSelectProgram;
        private System.Windows.Forms.Label labelParameter;
        private System.Windows.Forms.TextBox textBoxParameter;
        private System.Windows.Forms.Button buttonGoToSleepAndWait;
        private System.Windows.Forms.ComboBox comboBoxOperation;
        private System.Windows.Forms.DateTimePicker dateTimePickerRestTime;
        private System.Windows.Forms.Label labelRestTime;
        private System.Windows.Forms.Label labelOperation;
        private System.Windows.Forms.GroupBox groupBoxWakeUp;
        private System.Windows.Forms.GroupBox groupBoxRest;
    }
}
