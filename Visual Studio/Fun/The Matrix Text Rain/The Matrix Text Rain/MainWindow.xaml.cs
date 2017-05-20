using Core;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheMatrixTextRain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly DateTime startTime = DateTime.Now;

        private readonly Backend backend = new Backend(characterCandidates: GenerateUnicodeRange((33, 127),
                                                                                                 (161, 741),
                                                                                                 (910, 930),
                                                                                                 (931, 1155),
                                                                                                 (1162, 1328),
                                                                                                 (1329, 1367),
                                                                                                 (1377, 1416)));

        private const float cellWidth = 24.0f;
        private const float cellHeight = 24.0f;
        private readonly Typeface headTypeface = new Typeface("Courier New Bold");
        private readonly Typeface tailTypeface = new Typeface("Courier New Bold");
        private double lastTime = 0.0;
        private readonly Brush headBrush = new SolidColorBrush(Color.FromScRgb(1.0f, 0.3f, 1.0f, 0.3f));
        private readonly Color tailColor1 = Color.FromScRgb(1.0f, 0.0f, 0.8f, 0.0f);
        private readonly Color tailColor2 = Color.FromScRgb(0.0f, 0.0f, 0.8f, 0.0f);
        private readonly Brush backgroundBrush = Brushes.Black;
        private readonly Pen backgroundPen = new Pen(Brushes.Transparent, 0.0);
        private readonly DrawingVisual canvas = new DrawingVisual();
        private readonly Border view = new Border();

        public MainWindow()
        {
            InitializeComponent();

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            view.Background = new VisualBrush(canvas);

            this.Content = view;
            this.WindowState = WindowState.Maximized;
        }

        private static float GenerateColorComponent(float from, float to, double position)
        {
            return (float)(from + (to - from) * position);
        }

        private static Color GenerateColor(Color from, Color to, double position)
        {
            return Color.FromScRgb(GenerateColorComponent(from.ScA, to.ScA, position),
                                   GenerateColorComponent(from.ScR, to.ScR, position),
                                   GenerateColorComponent(from.ScG, to.ScG, position),
                                   GenerateColorComponent(from.ScB, to.ScB, position));
        }

        private FormattedText CreateCharacter(string text, Typeface typeface, Brush brush)
        {
            return new FormattedText(text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 18.0,
                                     brush, 96.0)
            {
                TextAlignment =
                           TextAlignment.Center
            };
        }

        private void DrawRaindrop(DrawingContext drawingContext, int column, TheMatrixRaindrop raindrop)
        {
            for (var row = 0; row < raindrop.Size; row++)
            {
                var text = raindrop.Characters[row].ToString();
                var x = cellWidth / 2.0f + cellWidth * column;
                var y = cellHeight * ((int)raindrop.Position - row);
                var position = (row + raindrop.Position % 1.0) / raindrop.Size;
                var brush = new SolidColorBrush(GenerateColor(tailColor1, tailColor2,
                                                              1.0 - Math.Pow(1.0 - position, 1.6)));

                drawingContext
                    .DrawText(row == 0 ? CreateCharacter(text, headTypeface, headBrush) : CreateCharacter(text, tailTypeface, brush),
                              new Point(x, y));
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var currentTime = (DateTime.Now - startTime).TotalSeconds;
            var columns = (int)(view.ActualWidth / cellWidth);
            var rows = (int)(view.ActualHeight / cellHeight);
            var logicalView = backend.GetView(columns, rows, currentTime);

            using (var drawingContext = canvas.RenderOpen())
            {
                var workingArea = new Rect(new Point(0.0, 0.0), view.RenderSize);

                drawingContext.PushClip(new RectangleGeometry(workingArea));
                drawingContext.DrawRectangle(backgroundBrush, backgroundPen, workingArea);

                for (var column = 0; column < columns; column++)
                {
                    foreach (var raindrop in logicalView[column])
                    {
                        DrawRaindrop(drawingContext, column, raindrop);
                    }
                }
            }

            this.Title = $"Whatever - Size: {view.ActualWidth} × {view.ActualHeight}, " +
                         $"Frame Rate: {1.0 / (currentTime - lastTime)}";

            lastTime = currentTime;
        }

        private static string GenerateUnicodeRange(params ValueTuple<int, int>[] ranges)
        {
            return string.Join(string.Empty,
                               from range in ranges
                               from c in Enumerable
                                   .Range(range.Item1, range.Item2 - range.Item1).Select(char.ConvertFromUtf32)
                               select c);
        }
    }
}
