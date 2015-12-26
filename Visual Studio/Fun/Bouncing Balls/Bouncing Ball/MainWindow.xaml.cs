using System;
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
        private readonly Scene scene = new Scene(g: 9.8 * 100.0 / 2.54 * 96,
                                                 ballSize: 64.0,
                                                 ballIinitialLocation: new Point(0.0, 800.0),
                                                 ballIinitialVelocity: new Vector(2500.0, 0.0),
                                                 maxAfterimageCount: 64,
                                                 afterimageInterval: TimeSpan.FromSeconds(0.005));

        private readonly DateTime startTime = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = scene;

            CreateAfterimages();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newSize = ((Canvas)sender).RenderSize;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (scene.Width != newSize.Width)
            {
                scene.Width = newSize.Width;

                scene.ClearXCache();
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (scene.Height != newSize.Height)
            {
                scene.Height = newSize.Height;

                scene.ClearYCache();
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var time = DateTime.Now - startTime;

            SetBallLocation(time);
            UpdateAfterimages(time);
        }

        private void CreateAfterimages()
        {
            for (var i = 0; i < scene.MaxAfterimageCount; ++i)
            {
                this.MainCanvas.Children.Insert(MainCanvas.Children.Count - 1,
                                                new Ellipse()
                                                {
                                                    Width = scene.BallSize,
                                                    Height = scene.BallSize,
                                                    Fill = MainBall.Fill,
                                                    StrokeThickness = MainBall.StrokeThickness,
                                                    Stroke = MainBall.Stroke
                                                });
            }
        }

        private void SetBallLocation(TimeSpan time)
        {
            var ballLocation = scene.GetCurrentPosition(time);

            MainBall.SetValue(Canvas.LeftProperty, ballLocation.X);
            MainBall.SetValue(Canvas.TopProperty, ballLocation.Y);
        }

        private void UpdateAfterimages(TimeSpan time)
        {
            var afterImagePositions = scene.GetAfterImages(time).ToArray();
            var visibleFrom = scene.MaxAfterimageCount - afterImagePositions.Length;
            var opacityInterval = 1.0 / scene.MaxAfterimageCount;
            var opacityFrom = (1.0 -
                               (time.Ticks % scene.AfterimageInterval.Ticks) / (double)scene.AfterimageInterval.Ticks) *
                              opacityInterval;
            var i = 0;

            for (; i < visibleFrom; i++)
            {
                GetAfterimage(i).Visibility = Visibility.Hidden;
            }

            for (; i < scene.MaxAfterimageCount; i++)
            {
                var afterimage = GetAfterimage(i);
                var position = afterImagePositions[i - visibleFrom];

                afterimage.Visibility = Visibility.Visible;
                afterimage.SetValue(Canvas.LeftProperty, position.X);
                afterimage.SetValue(Canvas.TopProperty, position.Y);
                afterimage.Opacity = (opacityFrom + i * opacityInterval) * 0.2;
            }
        }

        private Ellipse GetAfterimage(int index)
        {
            return (Ellipse)MainCanvas.Children[index];
        }
    }
}
