using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BouncingBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Scene scene = new Scene();
        private readonly DateTime startTime = DateTime.Now;
        private readonly List<Ellipse> afterimages = new List<Ellipse>();
        private int frame = 0;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = scene;

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            afterimages.AddRange(Enumerable.Range(0, scene.AfterimageCount).Select(i => new Ellipse()
            {
                Width = scene.BallSize,
                Height = scene.BallSize,
                Fill = scene.BallBrush,
                StrokeThickness = scene.BallStrokeThickness,
                Stroke = scene.BallStroke
            }));

            foreach (var afterimage in afterimages)
            {
                MainCanvas.Children.Insert(MainCanvas.Children.Count - 1, afterimage);
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = (Canvas)sender;

            scene.Width = canvas.ActualWidth;
            scene.Height = canvas.ActualHeight;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            double time = (DateTime.Now - startTime).TotalSeconds;
            ++frame;

            App.Current.MainWindow.Title = (frame / time).ToString();

            scene.Time = time;

            var afterImagePositions = scene.Afterimages.ToArray();

            for (int i = 0; i < afterImagePositions.Length; i++)
            {
                afterimages[i].Visibility = Visibility.Visible;
                afterimages[i].SetValue(Canvas.LeftProperty, afterImagePositions[i].Value.X);
                afterimages[i].SetValue(Canvas.TopProperty, afterImagePositions[i].Value.Y);
                afterimages[i].Opacity = 0.382 * (1.0 - ((time - scene.AfterimageInterval * afterImagePositions[i].Key)) / (scene.AfterimageInterval * scene.AfterimageCount));
            }

            for (int i = afterImagePositions.Length; i < afterimages.Count; i++)
            {
                afterimages[i].Visibility = Visibility.Hidden;
            }
        }
    }
}
