using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gravitation
{
    internal class VectorScene : Scene
    {
        public int TargetPointCount
        {
            get;
            set;
        }

        public double VectorLength
        {
            get;
            set;
        }

        public Color VectorColor
        {
            get;
            set;
        }

        public override void Render(Graphics graphics)
        {
            graphics.Clear(Color.Black);

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            for (int i = 0; i < TargetPointCount; i++)
            {
                float x = (float)(Size.Width * Random.NextDouble());
                float y = (float)(Size.Height * Random.NextDouble());

                double gx = 0, gy = 0;
                foreach (var mass_point in MassPoints)
                {
                    float dx = x - mass_point.Item1.X;
                    float dy = y - mass_point.Item1.Y;
                    float d2 = dx * dx + dy * dy;
                    if (d2 == 0)
                    {
                        continue;
                    }
                    double d = Math.Sqrt(d2);
                    double t = GravitationalConstant * mass_point.Item2 / (d2 * d);
                    gx += t * dx;
                    gy += t * dy;
                }

                double g = Math.Sqrt(gx * gx + gy * gy);
                Pen pen;
                if (g > MaxGravitation)
                {
                    pen = new Pen(VectorColor);
                }
                else
                {
                    pen = new Pen(Color.FromArgb(Dithering(255 * (g / MaxGravitation)), VectorColor));
                }

                double t2 = VectorLength / g;

                double vx = t2 * gx;
                double vy = t2 * gy;

                graphics.DrawLine(pen, (float)(x + vx), (float)(y + vy), (float)(x - vx), (float)(y - vy));
            }
        }
    }
}
