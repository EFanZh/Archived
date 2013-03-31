using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw
{
    internal class Line : Curve
    {
        public PointF Point1
        {
            get;
            set;
        }

        public PointF Point2
        {
            get;
            set;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawLine(Stroke, Point1, Point2);
        }

        public override float Distance(Point point)
        {
            float dx0 = Point2.X - Point1.X;
            float dy0 = Point2.Y - Point1.Y;
            float d02 = dx0 * dx0 + dy0 * dy0;
            float dx, dy;
            dx = point.X - Point1.X;
            dy = point.Y - Point1.Y;
            float d12 = dx * dx + dy * dy;
            if (d02 == 0)
            {
                return (float)Math.Sqrt(d12);
            }
            dx = point.X - Point2.X;
            dy = point.Y - Point2.Y;
            float d22 = dx * dx + dy * dy;
            double a = (d02 + d22 - d12) / Math.Sqrt(d02 * d22);
            if (a <= 0)
            {
                return (float)Math.Sqrt(d22);
            }
            a = (d02 + d12 - d22) / Math.Sqrt(d02 * d12);
            if (a <= 0)
            {
                return (float)Math.Sqrt(d12);
            }
            return (float)(Math.Abs(dx0 * point.Y - point.X * dy0 + Point2.X * Point1.Y - Point1.X * Point2.Y) / Math.Sqrt(dx0 * dx0 + dy0 * dy0));
        }
    }
}
