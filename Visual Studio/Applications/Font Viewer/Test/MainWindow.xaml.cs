using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DirectWriteWrapper;

namespace Test
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Factory factory = new Factory(FactoryType.Shared);

        public MainWindow()
        {
            InitializeComponent();

            var k = factory.GetSystemFontCollection(true)[4].GetFamilyNames();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => (new Window()).Show()));
        }
    }
}
