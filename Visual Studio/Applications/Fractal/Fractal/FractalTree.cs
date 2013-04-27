using System;
using System.Drawing;

namespace Fractal
{
    internal class FractalTree : Fractal
    {
        private double base_length;
        private double width_factor = 0.1;

        public double Scale1
        {
            get;
            set;
        }

        public double Scale2
        {
            get;
            set;
        }

        public double OffsetAngle
        {
            get;
            set;
        }

        public double SplitAngle
        {
            get;
            set;
        }

        protected override void DoDraw(Graphics graphics)
        {
            double x = Size.Width / 2.0;
            double y = Size.Height / 2.0;
            double angle = -Math.PI / 2;
            base_length = (Scale1 - 1) * Size.Height / (Math.Pow(Scale1, MaxLevel) - 1) / 2.0;
            Draw(graphics, x, y, angle, base_length, true, true, 0);
        }

        private void Draw(Graphics graphics, double x, double y, double angle, double length, bool offset_left, bool split_left, int level)
        {
            if (length < 1.0)
            {
                return;
            }

            double end_x = x + length * Math.Cos(angle);
            double end_y = y + length * Math.Sin(angle);
            graphics.DrawLine(new Pen(this.GetInterpolationColor(1.0 - length / base_length), (float)(width_factor * length)), (float)x, (float)y, (float)end_x, (float)end_y);
            level++;

            if (level < this.MaxLevel)
            {
                if (offset_left)
                {
                    angle -= OffsetAngle;
                }
                else
                {
                    angle += OffsetAngle;
                }
                if (split_left)
                {
                    Draw(graphics, end_x, end_y, angle - SplitAngle, Scale2 * length, !split_left, split_left, level);
                }
                else
                {
                    Draw(graphics, end_x, end_y, angle + SplitAngle, Scale2 * length, !split_left, split_left, level);
                }
                Draw(graphics, end_x, end_y, angle, Scale1 * length, offset_left, !split_left, level);
            }
        }
    }
}
