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

namespace ColorPicker
{
    /// <summary>
    /// Interaction logic for ControlChannelSlider.xaml
    /// </summary>
    public partial class ControlChannelSlider : UserControl
    {
        public ControlChannelSlider()
        {
            InitializeComponent();
        }

        public double IndicatorWidth
        {
            get;
            set;
        }

        public double Minimum
        {
            get;
            set;
        }

        public double Maximum
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }
    }
}
