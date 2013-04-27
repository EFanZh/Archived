using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AutoRunCommand
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SetSelectFileButtonClickEvents(buttonFileExists, textBoxFileExists, "All files (*.*)|*.*");
            SetSelectFileButtonClickEvents(buttonRunCommand, textBoxRunCommand, "Executable files (*.bat; *.com; *.exe)|*.bat; *.com; *.exe|All files (*.*)|*.*");
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (string.Equals(buttonStart.Text, "Start"))
            {
                if (string.IsNullOrEmpty(textBoxFileExists.Text))
                {
                    MessageBox.Show("File to be monitored is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    timerMain.Enabled = true;
                    buttonStart.Text = "Stop";
                }
            }
            else
            {
                timerMain.Enabled = false;
                buttonStart.Text = "Start";
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (File.Exists(textBoxFileExists.Text))
            {
                try
                {
                    Process.Start(textBoxRunCommand.Text, textBoxRunCommandParameter.Text);
                    timerMain.Enabled = false;
                    buttonStart.Text = "Start";
                }
                catch (Exception)
                {
                }
            }
        }

        private void SetSelectFileButtonClickEvents(Button button, TextBox text_box, string filter)
        {
            button.Click += (sender, e) =>
            {
                openFileDialogMain.Filter = filter;
                if (openFileDialogMain.ShowDialog() == DialogResult.OK)
                {
                    text_box.Text = openFileDialogMain.FileName;
                }
            };
        }
    }
}
