using System.Windows;

namespace CheckFileList
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(string missingFiles, string extraFiles)
        {
            MissingFiles = missingFiles;
            ExtraFiles = extraFiles;

            InitializeComponent();
        }

        public string MissingFiles
        {
            get;
            set;
        }

        public string ExtraFiles
        {
            get;
            set;
        }
    }
}
