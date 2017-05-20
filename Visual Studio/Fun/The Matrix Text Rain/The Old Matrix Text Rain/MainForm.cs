using Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace TheOldMatrixTextRain
{
    internal sealed class MainForm : Form
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
        private readonly Font headFont = new Font("Courier New", 18.0f, FontStyle.Bold);
        private readonly Font tailFont = new Font("Courier New", 18.0f, FontStyle.Bold);
        private double lastTime = 0.0;
        private readonly StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center };
        private readonly Brush headBrush = new SolidBrush(Color.FromArgb(255, 80, 255, 80));
        private readonly Color tailColor1 = Color.FromArgb(255, 0, 200, 0);
        private readonly Color tailColor2 = Color.FromArgb(0, 0, 200, 0);

        public MainForm()
        {
            this.DoubleBuffered = true;
            this.Text = Application.ProductName;
            this.WindowState = FormWindowState.Maximized;

            this.Paint += MainForm_Paint;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private static byte GenerateColorComponent(byte from, byte to, double position)
        {
            return (byte)(from + (byte)((to - from + 1) * position));
        }

        private static Color GenerateColor(Color from, Color to, double position)
        {
            return Color.FromArgb(GenerateColorComponent(from.A, to.A, position),
                                  GenerateColorComponent(from.R, to.R, position),
                                  GenerateColorComponent(from.G, to.G, position),
                                  GenerateColorComponent(from.B, to.B, position));
        }

        private void DrawRaindrop(Graphics graphics, int column, TheMatrixRaindrop raindrop)
        {
            for (var row = 0; row < raindrop.Size; row++)
            {
                var text = raindrop.Characters[row].ToString();
                var x = cellWidth / 2.0f + cellWidth * column;
                var y = cellHeight * ((int)raindrop.Position - row);
                var position = (row + raindrop.Position % 1.0) / raindrop.Size;
                var brush = new SolidBrush(GenerateColor(tailColor1, tailColor2, 1.0 - Math.Pow(1.0 - position, 1.6)));

                if (row == 0)
                {
                    graphics.DrawString(text, headFont, headBrush, x, y, stringFormat);
                }
                else
                {
                    graphics.DrawString(text, tailFont, brush, x, y, stringFormat);
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var currentTime = (DateTime.Now - startTime).TotalSeconds;
            var columns = (int)(this.ClientSize.Width / cellWidth);
            var rows = (int)(this.ClientSize.Height / cellHeight);
            var view = backend.GetView(columns, rows, currentTime);

            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            for (var column = 0; column < columns; column++)
            {
                foreach (var raindrop in view[column])
                {
                    DrawRaindrop(e.Graphics, column, raindrop);
                }
            }

            this.Text = $@"{Application.ProductName} - Frame Rate: {1.0 / (currentTime - lastTime)}";

            lastTime = currentTime;

            this.Invalidate();
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
