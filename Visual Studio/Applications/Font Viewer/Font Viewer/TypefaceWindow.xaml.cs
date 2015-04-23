using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FontViewer
{
    /// <summary>
    /// Interaction logic for TypefaceWindow.xaml
    /// </summary>
    public partial class TypefaceWindow : Window
    {
        public TypefaceWindow(Typeface typeface)
        {
            InitializeComponent();

            this.DataContext = typeface;
        }
    }
}
