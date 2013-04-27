using System;
using System.Windows;
using System.Windows.Interop;

namespace TextFileTools
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(string result)
        {
            InitializeComponent();

            this.Loaded += (sender, e) =>
            {
                NativeMethods.SetClassLongPtr(new WindowInteropHelper(this).Handle, NativeMethods.GCLP_HBRBACKGROUND, new IntPtr(NativeMethods.COLOR_WINDOW + 1));
            };

            textBoxResult.Text = result;
        }
    }
}
