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

            var task = Task.Run(() =>
            {
                var files = GetFiles(path);
                var fonts = new HashSet<string>(files.Select(File.ReadAllText).Select(Parser.Parse).SelectMany(r => r.Select(p => p.Key)));

                return fonts.OrderBy(s => s).ToArray();
            });

            try
            {
                var results = await task;

                ExistingFonts.Clear();
                foreach (var font in results)
                {
                    ExistingFonts.Add(font);
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

                await Task.Run(() =>
                {
                    try
                    {
                        foreach (string file in GetFiles(path))
                        {
                            string content = File.ReadAllText(file);

                            File.WriteAllText(file, Process(content, dict));
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
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
            return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Where(Filter);
        }

        private static bool Filter(string file)
        {
            var upper = file.ToUpper();

            return upper.EndsWith(".ASS") || upper.EndsWith(".SSA");
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
