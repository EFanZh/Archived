using System;
using System.Drawing;

namespace ThreeDDrawing
{
    internal static class Utilities
    {
        public static T GetNonNullObject<T>(T obj) where T : new()
        {
            return obj == null ? new T() : obj;
        }

        public static double GetDistance(PointF p1, PointF p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
