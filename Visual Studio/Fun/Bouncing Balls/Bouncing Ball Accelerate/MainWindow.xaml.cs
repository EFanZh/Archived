using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BouncingBallAccelerate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Scene scene = new Scene(ballSize: 64.0,
                                                 initialBallLocation: new Point(0.0, 0.0),
                                                 acceleration: new Vector(16.0, 16.0));

        private readonly DateTime startTime = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = scene;

            CompositionTarget.Rendering += CompositionTargetOnRendering;
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scene.SetSize(((Canvas)sender).RenderSize);
        }

        private void CompositionTargetOnRendering(object sender, EventArgs e)
        {
            var time = DateTime.Now - startTime;
            var location = scene.GetBallLocation(time);

            MainBall.SetValue(Canvas.LeftProperty, location.X);
            MainBall.SetValue(Canvas.TopProperty, location.Y);
        }
    }
}
