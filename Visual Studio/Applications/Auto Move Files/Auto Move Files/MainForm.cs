using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoMoveFiles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            AssociateSelectFolderButtonEvents(buttonSourceFolder, textBoxSourceFolder);
            AssociateSelectFolderButtonEvents(buttonDestinationFolder, textBoxDestinationFolder);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (string.Equals(buttonStart.Text, "Start"))
            {
                buttonStart.Text = "Stop";
                timerMain.Start();
            }
            else
            {
                timerMain.Stop();
                buttonStart.Text = "Start";
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (var file in Directory.GetFiles(textBoxSourceFolder.Text))
                {
                    string file_name = Path.GetFileName(file);
                    if (string.IsNullOrEmpty(textBoxExcludeRegex.Text) || !Regex.IsMatch(file_name, textBoxExcludeRegex.Text))
                    {
                        File.Move(file, Path.Combine(textBoxDestinationFolder.Text, file_name));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void AssociateSelectFolderButtonEvents(Button button, TextBox text_box)
        {
            button.Click += (sender, e) =>
            {
                if (folderBrowserDialogMain.ShowDialog() == DialogResult.OK)
                {
                    text_box.Text = folderBrowserDialogMain.SelectedPath;
                }
            };
        }
    }
}
