﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            Model = new Model();
            Model.Load();

            InitializeComponent();

            ExistingFontsListBox.Items.SortDescriptions.Add(new SortDescription());
        }

        public Model Model
        {
            get;
        }

        public State State
        {
            get;
        } = new State("Ready.");

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Model.Save();
        }

        private async void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            using (State.OnState("Analyzing…"))
            {
                ((UIElement)sender).IsEnabled = false;

                var path = FolderTextBox.Text;

                Model.ExistingFonts.Clear();

                foreach (var file in await GetFiles(path))
                {
                    foreach (var result in await AnalyzeFile(file))
                    {
                        var fontName = result.Key.StartsWith("@") ? result.Key.Substring(1) : result.Key;
                        var fontNameUpper = fontName.ToUpper();

                        if (Model.ExistingFonts.All(f => !f.ToUpper().Equals(fontNameUpper, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            Model.ExistingFonts.Add(fontName);
                        }
                    }
                }

                ((UIElement)sender).IsEnabled = true;
            }
        }

        private void ExistingFontsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fontName = (string)ExistingFontsListBox.SelectedItem;

            if (fontName != null)
            {
                Model.AddFontMapping(fontName, string.Empty, string.Empty);
            }
        }

        private async void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            using (State.OnState("Processing…"))
            {
                ((UIElement)sender).IsEnabled = false;

                var path = FolderTextBox.Text;

                try
                {
                    var dict = Model.CreateReplaceDictionary();

                    if (MessageBox.Show("You sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    foreach (var file in await GetFiles(path))
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
        }

        private void AddFontMappingButton_Click(object sender, RoutedEventArgs e)
        {
            Model.FontMappings.Add(new FontMapping(string.Empty, string.Empty, string.Empty));
        }

        private void RemoveFontMappingButton_Click(object sender, RoutedEventArgs e)
        {
            Model.FontMappings.RemoveAt(CollectionViewSource.GetDefaultView(Model.FontMappings).CurrentPosition);
        }

        private void AddVirtualFontButton_Click(object sender, RoutedEventArgs e)
        {
            Model.VirtualFonts.Add(new VirtualFont(string.Empty, string.Empty, string.Empty));
        }

        private void RemoveVirtualFontButton_Click(object sender, RoutedEventArgs e)
        {
            Model.VirtualFonts.RemoveAt(CollectionViewSource.GetDefaultView(Model.VirtualFonts).CurrentPosition);
        }

        private static async Task<IEnumerable<string>> GetFiles(string path)
        {
            return await Task.Run(() =>
            {
                var result = new List<string>();
                var s = new Stack<string>();

                s.Push(path);

                while (s.Count > 0)
                {
                    var current = s.Pop();

                    try
                    {
                        result.AddRange(Directory.EnumerateFiles(current).Where(Filter));

                        foreach (var next in Directory.EnumerateDirectories(current).Reverse())
                        {
                            s.Push(next);
                        }
                    }
                    catch (Exception)
                    {
                        // Ignored.
                    }
                }

                return result;
            });
        }

        private static bool Filter(string file)
        {
            var upper = file.ToUpper();

            return upper.EndsWith(".ASS") || upper.EndsWith(".SSA");
        }

        private async Task<KeyValuePair<string, int>[]> AnalyzeFile(string file)
        {
            using (State.OnState(file))
            {
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
        }

        private async Task ProcessFile(string file, IDictionary<string, string> mapping)
        {
            using (State.OnState(file))
            {
                await Task.Run(() =>
                {
                    try
                    {
                        File.WriteAllText(file, Process(File.ReadAllText(file), mapping));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        // Ignored.
                    }
                });
            }
        }

        private static string Process(string content, IDictionary<string, string> mapping)
        {
            var result = Parser.Parse(content).Select(p => new KeyValuePair<string, int>(p.Key.ToUpper(CultureInfo.InvariantCulture), p.Value)).ToArray();

            if (result.Length > 0)
            {
                var sb = new StringBuilder();
                var prev = 0;

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
