using System.Windows;
using System.Windows.Media;

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
