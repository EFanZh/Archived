using System;
using System.Collections.Generic;

namespace BezierFitting
{
    internal static class Fitting
    {
        private static double sample_interval = 0.001;

        public static double CalcDeviation(List<PointD> data, Bezier b)
        {
            double sum = 0.0;
            foreach (var p in data)
            {
                double min_distance = double.MaxValue;
                for (double t = 0; t < 1.0; t += sample_interval)
                {
                    double bx = Bezier(b.Point1.X, b.Point2.X, b.Point3.X, b.Point4.X, t), by = Bezier(b.Point1.Y, b.Point2.Y, b.Point3.Y, b.Point4.Y, t), dx = bx - p.X, dy = by - p.Y;
                    double v = dx * dx + dy * dy;
                    if (v < min_distance)
                    {
                        min_distance = v;
                    }
                }
                sum += min_distance;
            }
            return sum / data.Count;
        }

        private static double Bezier(double a0, double a1, double a2, double a3, double t)
        {
            double t2 = 1.0 - t;
            return a0 * Math.Pow(t2, 3.0) + t * (3 * a1 * t2 * t2 + t * (3 * a2 - 3 * a2 * t + a3 * t));
        }
    }
}
