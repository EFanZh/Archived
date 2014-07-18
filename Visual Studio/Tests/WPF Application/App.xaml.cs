using System.Windows;
using System.Windows.Input;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void FancyWindowCaption_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Window)((FrameworkElement)sender).TemplatedParent).DragMove();
        }

        private void FancyWindowMinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((Window)((FrameworkElement)sender).TemplatedParent).WindowState = WindowState.Minimized;
        }

        private void FancyWindowMaximizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Window window = (Window)((FrameworkElement)sender).TemplatedParent;
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }

        private void FancyWindowCloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((Window)((FrameworkElement)sender).TemplatedParent).Close();
        }
    }
}
