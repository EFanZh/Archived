using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DividePoints
{
    internal static class Divider
    {
        public static HashSet<HashSet<int>> Analysis(Point[] points)
        {
            var distances = new Dictionary<Tuple<int, int>, double>();

            // Calc distances.
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    double dx = points[i].X - points[j].X;
                    double dy = points[i].Y - points[j].Y;
                    distances[new Tuple<int, int>(i, j)] = Math.Sqrt(dx * dx + dy * dy);
                }
            }

            // Sort
            var distances_sorted = distances.OrderBy(x => { return x.Value; });

            var undivided = new HashSet<int>(Enumerable.Range(0, points.Length));
            var sets = new HashSet<HashSet<int>>();
            var point_to_set = new Dictionary<int, HashSet<int>>();
            foreach (var d in distances_sorted)
            {
                if (undivided.Count > 0)
                {
                    if (undivided.Contains(d.Key.Item1))
                    {
                        if (undivided.Contains(d.Key.Item2))
                        {
                            var set = new HashSet<int>() { d.Key.Item1, d.Key.Item2 };
                            sets.Add(set);
                            point_to_set[d.Key.Item1] = set;
                            point_to_set[d.Key.Item2] = set;
                            undivided.Remove(d.Key.Item1);
                            undivided.Remove(d.Key.Item2);
                        }
                        else
                        {
                            var set_2 = point_to_set[d.Key.Item2];
                            set_2.Add(d.Key.Item1);
                            point_to_set[d.Key.Item1] = set_2;
                            undivided.Remove(d.Key.Item1);
                        }
                    }
                    else
                    {
                        if (undivided.Contains(d.Key.Item2))
                        {
                            var set_1 = point_to_set[d.Key.Item1];
                            set_1.Add(d.Key.Item2);
                            point_to_set[d.Key.Item2] = set_1;
                            undivided.Remove(d.Key.Item2);
                        }
                        else
                        {
                            var set_1 = point_to_set[d.Key.Item1];
                            var set_2 = point_to_set[d.Key.Item2];
                            if (set_1 != set_2)
                            {
                                set_1.UnionWith(set_2);
                                foreach (var p in set_2)
                                {
                                    point_to_set[p] = set_1;
                                }
                                sets.Remove(set_2);
                            }
                        }
                    }
                }
            }
            return sets;
        }

        public static Point[] ConvexHull(Point[] points)
        {
            if (points.Length < 3)
            {
                throw new ArgumentException("At least 3 points reqired", "points");
            }

            List<Point> hull = new List<Point>();

            // get leftmost point
            Point vPointOnHull = points.Where(p => p.X == points.Min(min => min.X)).First();

            Point vEndpoint;
            do
            {
                hull.Add(vPointOnHull);
                vEndpoint = points[0];

                for (int i = 1; i < points.Length; i++)
                {
                    if ((vPointOnHull == vEndpoint)
                        || (Orientation(vPointOnHull, vEndpoint, points[i]) == -1))
                    {
                        vEndpoint = points[i];
                    }
                }

                vPointOnHull = vEndpoint;
            }
            while (vEndpoint != hull[0]);

            return hull.ToArray();
        }

        private static int Orientation(Point p1, Point p2, Point p)
        {
            // Determinant
            int Orin = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);

            if (Orin > 0)
                return -1; //          (* Orientaion is to the left-hand side  *)
            if (Orin < 0)
                return 1; // (* Orientaion is to the right-hand side *)

            return 0; //  (* Orientaion is neutral aka collinear  *)
        }
    }
}
