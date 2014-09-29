using System;
using System.Windows;
using System.Windows.Media;

namespace MyFont
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Slider_ValueChanged(null, null);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double w = WidthSlider.Value;
            double h = HeightSlider.Value;
            double t = ThicknessSlider.Value;
            double t2 = t * t;
            double four_h2_m_t2 = 4.0 * (h * h) - t2;
            double k = (2.0 * h * t * Math.Sqrt(four_h2_m_t2 + w * w) - w * t2) / four_h2_m_t2;
            double n = t / (2.0 * Math.Sqrt(1 - (t2 / (k * k))));
            double v = (n + h - t) / 2.0;

            TargetPath.Data = new CombinedGeometry(new PathGeometry(new[]
            {
                new PathFigure(new Point((w - k) / 2.0, 0.0), new PathSegment[]
                {
                    new PolyLineSegment(new[]
                    {
                        new Point((w + k) / 2.0, 0.0),
                        new Point(w, h),
                        new Point(w - k, h),
                        new Point(w / 2.0, n),
                        new Point(k, h),
                        new Point(0.0, h)
                    }, true)
                }, true)
            }), new PathGeometry(new[]
            {
                new PathFigure(new Point(w / 4.0, v), new PathSegment[]
                {
                    new PolyLineSegment(new[]
                    {
                        new Point(w * (3.0 / 4.0), v),
                        new Point(w * (3.0 / 4.0), v + t),
                        new Point(w / 4.0, v + t)
                    }, true)
                }, true)
            }));
        }
    }
}
