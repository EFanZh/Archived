using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
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

        private void CopyNameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            var names = FamilyNamesListView.SelectedItems.Cast<KeyValuePair<XmlLanguage, string>>().Select(p => p.Value);

            foreach (var name in names)
            {
                sb.AppendLine(name);
            }

            Clipboard.SetText(sb.ToString());
        }
    }
}
