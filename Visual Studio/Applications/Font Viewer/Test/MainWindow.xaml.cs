using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Test
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Document = new FontListDocument(Fonts.SystemFontFamilies.OrderBy(f => f.FamilyNames.First().Value));
        }

        public FlowDocument Document
        {
            get;
            private set;
        }
    }
}
