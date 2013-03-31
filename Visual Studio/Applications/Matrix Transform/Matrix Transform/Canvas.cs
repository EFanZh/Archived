using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MatrixTransform
{
    internal class Canvas
    {
        private Brush brush = Brushes.Red;
        private float radius = 2.0f;

        public IEnumerable<PointF> Dots
        {
            get;
            set;
        }

        public Size Size
        {
            get;
            set;
        }

        public void Render(Graphics graphics, Rectangle rectangle)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(graphics, rectangle);
            Graphics g = bg.Graphics;

            g.Transform = graphics.Transform;
            g.TranslateTransform(Size.Width / 2.0f, Size.Height / 2.0f);
            g.ScaleTransform(1.0f, -1.0f);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(Color.White);

            g.DrawLine(Pens.Black, -Size.Width / 2.0f, 0.0f, Size.Width / 2.0f, 0.0f);
            g.DrawLine(Pens.Black, 0.0f, Size.Height / 2.0f, 0.0f, -Size.Height / 2.0f);

            foreach (var dot in Dots)
            {
                g.FillEllipse(brush, dot.X - radius, dot.Y - radius, radius * 2, radius * 2);
            }

            bg.Render();
        }
    }
}
