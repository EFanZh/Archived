using System.Windows;
using System.Windows.Media;

namespace FontViewer
{
    /// <summary>
    /// Interaction logic for FamilyTypefaceWindow.xaml
    /// </summary>
    public partial class FamilyTypefaceWindow : Window
    {
        public FamilyTypefaceWindow(FamilyTypeface familyTypeface)
        {
            InitializeComponent();

            this.DataContext = familyTypeface;
        }
    }
}
