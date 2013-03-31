using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    internal class TreeScene : FractalScene
    {
        public double SplitAngle
        {
            get;
            set;
        }

        public double OffsetAngle
        {
            get;
            set;
        }

        protected override double DoGetBaseSize()
        {
            return this.Size.Height;
        }

        protected override double DoGetPenWidthFactor()
        {
            return 0.05;
        }

        protected override void DoDraw(Graphics graphics)
        {
            double x = Size.Width / 2.0;
            double y = Size.Height;
            double angle = -Math.PI / 2;
            DrawTree(graphics, x, y, angle, false, true, 0);
        }

        private void DrawTree(Graphics graphics, double x, double y, double angle, bool offset_left, bool split_left, int level)
        {
            double length = this.LevelLineLength[level];
            double dx = length * Math.Cos(angle);
            double dy = length * Math.Sin(angle);
            double end_x = x + dx;
            double end_y = y + dy;
            this.DrawFractalLine(graphics, x, y, end_x, end_y, level);
            level++;
            if (level < this.MaxLevel)
            {
                double next_angle;
                if (offset_left)
                {
                    next_angle = angle - OffsetAngle;
                }
                else
                {
                    next_angle = angle + OffsetAngle;
                }

                if (level + 1 < MaxLevel)
                {
                    if (split_left)
                    {
                        DrawTree(graphics, end_x, end_y, next_angle - SplitAngle, !split_left, split_left, level + 1);
                    }
                    else
                    {
                        DrawTree(graphics, end_x, end_y, next_angle + SplitAngle, !split_left, split_left, level + 1);
                    }
                }

                DrawTree(graphics, end_x, end_y, next_angle, offset_left, !split_left, level);
            }
        }
    }
}
