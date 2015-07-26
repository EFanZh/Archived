using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CheckFileList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex LineSplitterRegex = new Regex(@"\s*[\r\n]\s*", RegexOptions.Compiled);

        public MainWindow()
        {
            InitializeComponent();

            NamedSegment.NameComparison = StringComparison.OrdinalIgnoreCase;
        }

        public string TargetFolder
        {
            get;
            set;
        } = string.Empty;

        public string FileList
        {
            get;
            set;
        } = string.Empty;

        private async void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await Task.Run(() =>
            {
                var actualFiles = new List<Path>();
                var fileList = new List<Path>();

                Path basePath = Path.ParseNormalized(TargetFolder);

                try
                {
                    actualFiles.AddRange(Directory.GetFiles(TargetFolder, "*", SearchOption.AllDirectories).Select(
                        f =>
                        {
                            Path path = Path.Parse(f);

                            path.MakeRelativeTo(basePath);

                            return path;
                        }));
                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {
                    fileList.AddRange(LineSplitterRegex.Split(FileList).Select(Path.ParseNormalized));
                }
                catch (Exception)
                {
                    // ignored
                }

                var missingFiles = fileList.Except(actualFiles);
                var extraFiles = actualFiles.Except(fileList);

                return Tuple.Create(FileListToLines(missingFiles), FileListToLines(extraFiles));
            });

            (new ResultWindow(result.Item1, result.Item2)).Show();
        }

        private static string FileListToLines(IEnumerable<Path> files)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var file in files.OrderBy(f => f))
            {
                sb.AppendLine(file.ToString());
            }

            return sb.ToString();
        }
    }
}
