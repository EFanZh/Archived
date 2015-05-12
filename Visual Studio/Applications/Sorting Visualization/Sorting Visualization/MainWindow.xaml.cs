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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortingVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Scene<double> scene = new Scene<double>();

        public MainWindow()
        {
            InitializeComponent();

            scene.Reset += scene_Reset;
            scene.Assign += scene_Assign;
            scene.Compare += scene_Compare;
            scene.CreateItem += scene_CreateItem;
        }

        private void scene_Reset(object sender, EventArgs e)
        {
        }

        private void scene_Assign(object sender, AssignEventArgs<double> e)
        {
        }

        private void scene_Compare(object sender, CompareEventArgs<double> e)
        {
        }

        private void scene_CreateItem(object sender, CreateItemEventArgs<double> e)
        {
        }
    }
}
