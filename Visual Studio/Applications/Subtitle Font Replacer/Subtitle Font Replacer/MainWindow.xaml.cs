using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubtitleFontReplacer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex splitCommaRegex = new Regex(@"\s*,\s*");

        public MainWindow()
        {
            InitializeComponent();

            Files = new ObservableCollection<KeyValuePair<string, List<FontNamePosition>>>();
            ExistingFonts = new ObservableCollection<string>();
            Map = new ObservableCollection<KeyValuePair<string, string>>();
        }

        public ObservableCollection<KeyValuePair<string, List<FontNamePosition>>> Files
        {
            get;
            private set;
        }

        public ObservableCollection<string> ExistingFonts
        {
            get;
            private set;
        }

        public ObservableCollection<KeyValuePair<string, string>> Map
        {
            get;
            private set;
        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            string path = FolderTextBox.Text;

            var files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Where(Filter).ToArray();

            Map.Clear();
            Array.ForEach(files, f => Map.Add(new KeyValuePair<string, string>(f, f)));
        }

        private static bool Filter(string file)
        {
            var upper = file.ToUpper();

            return upper.EndsWith(".ASS") || upper.EndsWith(".SSA");
        }

        private static bool IsStyleSegment(string line)
        {
            return string.Equals(line, "[V4 STYLES]", StringComparison.InvariantCultureIgnoreCase) ||
                   string.Equals(line, "[V4+ STYLES]", StringComparison.InvariantCultureIgnoreCase);
        }

        private static int GetFontNameIndex(string line)
        {
            var row = splitCommaRegex.Split(line);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.Equals(row[i], "FONTNAME", StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        private static string[] GetFonts(string file)
        {
            var styles = new List<string[]>();
            int fontNameIndex = -1;

            using (StreamReader sr = new StreamReader(file))
            {
                string line = sr.ReadLine();

                while (line != null && !IsStyleSegment(line))
                {
                    line = sr.ReadLine();
                }

                // Now "line" is null or style header.
                if (line != null)
                {
                    line = sr.ReadLine();
                    while (line != null && !line.Trim().StartsWith("["))
                    {
                        line = line.Trim().ToUpper();

                        if (line.StartsWith("FORMAT:"))
                        {
                            fontNameIndex = GetFontNameIndex(line.Substring(7).Trim());
                        }
                        else if (line.StartsWith("STYLE:"))
                        {
                            styles.Add(line.Substring(6).Split(',').Select(s => s.Trim()).ToArray());
                        }
                    }
                }
            }

            return fontNameIndex == -1 ? new string[0] : styles.Select(s => s[fontNameIndex]).ToArray();
        }
    }
}
