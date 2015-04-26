using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace BouncingBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Scene scene = new Scene();
        private DateTime startTime = DateTime.Now;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = scene;

            timer = new DispatcherTimer(TimeSpan.FromMilliseconds(1), DispatcherPriority.Render, Timer_Tick, Dispatcher.CurrentDispatcher) { IsEnabled = true };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            scene.Time = (DateTime.Now - startTime).TotalSeconds;
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = (Canvas)sender;

            scene.Width = canvas.ActualWidth;
            scene.Height = canvas.ActualHeight;
        }
    }
}
