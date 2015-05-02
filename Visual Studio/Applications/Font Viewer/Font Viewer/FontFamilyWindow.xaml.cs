using System.Windows;
using System.Windows.Media;

namespace FontViewer
{
    /// <summary>
    /// Interaction logic for FontFamilyWindow.xaml
    /// </summary>
    public partial class FontFamilyWindow : Window
    {
        public FontFamilyWindow(FontFamily fontFamily)
        {
            InitializeComponent();

            this.DataContext = new FontFamilyWrapper(fontFamily);
        }
    }
}
