using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SubtitleFontReplacer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConfigFile = "config.txt";

        public MainWindow()
        {
            InitializeComponent();

            ExistingFonts = new ObservableCollection<string>();
            FontMapping = new ObservableCollection<FontMapping>();

            try
            {
                var fontMappings = from mapping in
                                       from line in File.ReadAllLines(ConfigFile)
                                       where line.Contains(",")
                                       select line.Split(',')
                                   select new FontMapping(mapping[0].Trim(), mapping[1].Trim());

                foreach (var mapping in fontMappings)
                {
                    FontMapping.Add(mapping);
                }
            }
            catch (Exception)
            {
            }
        }

        public ObservableCollection<string> ExistingFonts
        {
            get;
            private set;
        }

        public ObservableCollection<FontMapping> FontMapping
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                File.WriteAllLines(ConfigFile, FontMapping.Select(m => string.Format("{0}, {1}", m.Original, m.Target)));
            }
            catch (Exception)
            {
            }
        }

        private async void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).IsEnabled = false;

            string path = FolderTextBox.Text;

            ExistingFonts.Clear();

            foreach (var file in GetFiles(path))
            {
                foreach (var result in await AnalyzeFile(file))
                {
                    if (!ExistingFonts.Contains(result.Key))
                    {
                        ExistingFonts.Add(result.Key);
                    }
                }
            }

            ((UIElement)sender).IsEnabled = true;
        }

        private void ExistingFontsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ExistingFontsListBox.SelectedItem != null)
            {
                FontMapping.Add(new FontMapping((string)ExistingFontsListBox.SelectedItem, string.Empty));
            }
        }

        private async void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).IsEnabled = false;

            string path = FolderTextBox.Text;
            var dict = FontMapping.ToDictionary(pair => pair.Original, pair => pair.Target);

            try
            {
                if (dict.Any(p => string.IsNullOrWhiteSpace(p.Key) || string.IsNullOrWhiteSpace(p.Value)))
                {
                    MessageBox.Show("Fields can not be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                if (MessageBox.Show("You sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }

                foreach (string file in GetFiles(path))
                {
                    await ProcessFile(file, dict);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ((UIElement)sender).IsEnabled = true;
            }
        }

        private void AddFontMappingButton_Click(object sender, RoutedEventArgs e)
        {
            FontMapping.Add(new FontMapping("Original", "Target"));
        }

        private void RemoveFontMappingButton_Click(object sender, RoutedEventArgs e)
        {
            FontMapping.RemoveAt(CollectionViewSource.GetDefaultView(FontMapping).CurrentPosition);
        }

        private static IEnumerable<string> GetFiles(string path)
        {
            IEnumerable<string> result = new string[0];

            try
            {
                result = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Where(Filter);
            }
            catch (Exception)
            {
            }

            return result;
        }

        private static bool Filter(string file)
        {
            var upper = file.ToUpper();

            return upper.EndsWith(".ASS") || upper.EndsWith(".SSA");
        }

        private async Task<KeyValuePair<string, int>[]> AnalyzeFile(string file)
        {
            StateTextBlock.Text = file;

            return await Task.Run(() =>
            {
                try
                {
                    return Parser.Parse(File.ReadAllText(file));
                }
                catch (Exception)
                {
                    return new KeyValuePair<string, int>[0];
                }
            });
        }

        private async Task ProcessFile(string file, IDictionary<string, string> mapping)
        {
            StateTextBlock.Text = file;

            await Task.Run(() =>
            {
                try
                {
                    File.WriteAllText(file, Process(File.ReadAllText(file), mapping));
                }
                catch (Exception)
                {
                }
            });
        }

        private static string Process(string content, IDictionary<string, string> mapping)
        {
            var result = Parser.Parse(content);

            if (result.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                int prev = 0;

                foreach (var pair in result)
                {
                    if (mapping.ContainsKey(pair.Key))
                    {
                        sb.Append(content.Substring(prev, pair.Value - prev));
                        sb.Append(mapping[pair.Key]);
                    }
                    else
                    {
                        sb.Append(content.Substring(prev, pair.Value + pair.Key.Length - prev));
                    }

                    prev = pair.Value + pair.Key.Length;
                }

                sb.Append(content.Substring(prev));

                return sb.ToString();
            }
            else
            {
                return content;
            }
        }
    }
}
