using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw
{
    internal static class GraphicsMath
    {
        public static float Distance(PointF point1, PointF point2)
        {
            float xd = point2.X - point1.X;
            float yd = point2.Y - point1.Y;
            return (float)Math.Sqrt(xd * xd + yd * yd);
        }
    }
}
