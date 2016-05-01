using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace BinaryFileVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string filePath = @"E:\EFanZh\Temp\Panels_Map.jpg";
        private static readonly OpenFileDialog openFileDialog = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();

            Model = new Model(filePath);

            this.DataContext = Model;

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenCommand_Executed));
        }

        public Model Model
        {
            get;
            set;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                Model.FilePath = openFileDialog.FileName;
            }
        }

        private void View_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Model.ViewSize = e.NewSize;
        }

        private void View_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Model.ScrollPosition -= e.Delta / 5;
        }
    }
}
