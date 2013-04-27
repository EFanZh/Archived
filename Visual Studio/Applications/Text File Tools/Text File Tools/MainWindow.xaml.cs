using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace TextFileTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
        private TextFileProcessor tfp = new TextFileProcessor();

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (sender, e) =>
            {
                NativeMethods.SetClassLongPtr(new WindowInteropHelper(this).Handle, NativeMethods.GCLP_HBRBACKGROUND, new IntPtr(NativeMethods.COLOR_WINDOW + 1));
            };
            fbd.Description = "Select folder:";
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            buttonSelectSourceFolder.IsEnabled = false;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxSourceFolder.Text = fbd.SelectedPath;
            }
            buttonSelectSourceFolder.IsEnabled = true;
        }

        private async void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            buttonProcess.IsEnabled = false;

            // Get files.
            if (!Directory.Exists(textBoxSourceFolder.Text))
            {
                MessageBox.Show("Invalid Source Folder.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                buttonProcess.IsEnabled = true;
                return;
            }
            progressBarMain.IsIndeterminate = true;
            var files = await GetFiles(textBoxSourceFolder.Text);
            progressBarMain.IsIndeterminate = false;

            // Process files.
            var progress = new Progress<int>(value =>
            {
                progressBarMain.Value = value;
            });
            var tfpos = new List<TextFileProcessOptions>();
            if (checkBoxRemoveUTF8BOM.IsChecked == true)
            {
                tfpos.Add(TextFileProcessOptions.RemoveUTF8BOM);
            }
            if (checkBoxAddBlankLineAtFileBottom.IsChecked == true)
            {
                tfpos.Add(TextFileProcessOptions.AddBlankLineAtFileBottom);
            }
            progressBarMain.Maximum = files.Length;
            await tfp.ProcessFileAsync(files, progress, tfpos);

            // Show results.
            StringBuilder sb = new StringBuilder();
            foreach (var file in files)
            {
                sb.Append(file);
                sb.Append("\r\n");
            }
            (new ResultWindow(sb.ToString())).Show();

            buttonProcess.IsEnabled = true;
        }

        private async Task<string[]> GetFiles(string path)
        {
            Regex filter = GetFilterRegex();

            var result = await Task<string[]>.Run(() =>
            {
                return GetAllFiles(path).Where(str => filter.IsMatch(str)).ToArray();
            });

            return result;
        }

        private Regex GetFilterRegex()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(^");
            sb.Append(string.Join("$)|(^", from line in Regex.Split(textBoxFilter.Text.Trim(), @"\s*[\r\n]\s*")
                                           select Regex.Escape(line).Replace(@"\*", ".*").Replace(@"\?", ".")));
            sb.Append("$)");
            return new Regex(sb.ToString(), RegexOptions.IgnoreCase);
        }

        private static IEnumerable<string> GetAllFiles(string path)
        {
            string[] files = null;

            try
            {
                files = Directory.GetFiles(path);
            }
            catch (Exception)
            {
            }
            if (files != null)
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    yield return file;
                }
            }

            string[] dirs = null;
            try
            {
                dirs = Directory.GetDirectories(path);
            }
            catch (Exception)
            {
            }
            foreach (var dir in dirs)
            {
                foreach (var file in GetAllFiles(dir))
                {
                    yield return file;
                }
            }
        }
    }
}
