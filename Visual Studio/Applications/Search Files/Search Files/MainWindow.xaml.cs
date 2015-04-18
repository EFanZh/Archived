using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SearchFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool shouldStop = false;
        private Task currentTask = Task.FromResult(0);

        public MainWindow()
        {
            InitializeComponent();

            Model = new Model();

            Model.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Folder" || e.PropertyName == "Keyword")
                {
                    NewSearch();
                }
            };
        }

        public Model Model
        {
            get;
            set;
        }

        private async void NewSearch()
        {
            shouldStop = true;
            await currentTask;

            string folder = Model.Folder;
            Func<string, bool> predicate = s => Path.GetFileName(s).Contains(Model.Keyword);
            var progress = new Progress<string>();

            progress.ProgressChanged += (sender, e) => Model.Result.Add(e);

            Model.Result.Clear();

            shouldStop = false;

            Model.State = "Searching…";

            currentTask = Task.Run(() =>
            {
                Dfs(folder, predicate, progress);
            });

            await currentTask;
            Model.State = "Done.";
        }

        private void Dfs(string folder, Func<string, bool> predicate, IProgress<string> progress)
        {
            var s = new Stack<string>();

            s.Push(folder);

            while (s.Count > 0)
            {
                var current = s.Pop();

                if (predicate(current))
                {
                    progress.Report(current);
                }

                try
                {
                    foreach (var file in Directory.GetFiles(current).Where(predicate))
                    {
                        progress.Report(file);

                        if (shouldStop)
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    foreach (var directory in Directory.GetDirectories(current).Reverse())
                    {
                        s.Push(directory);

                        if (shouldStop)
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }

                if (shouldStop)
                {
                    break;
                }
            }
        }
    }
}
