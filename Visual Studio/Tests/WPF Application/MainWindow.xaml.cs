using System.Windows;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopupWindow popup_window = new PopupWindow();
            popup_window.Owner = this;
            popup_window.Show();
        }
    }
}
