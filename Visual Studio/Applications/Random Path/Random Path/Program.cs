using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RandomPath
{
    internal static class Program
    {
        private static readonly Random TheRandom = new Random();

        private static Point LeftOf(Point location)
        {
            return new Point(location.X - 1, location.Y);
        }

        private static Point TopOf(Point location)
        {
            return new Point(location.X, location.Y - 1);
        }

        private static Point RightOf(Point location)
        {
            return new Point(location.X + 1, location.Y);
        }

        private static Point BottomOf(Point location)
        {
            return new Point(location.X, location.Y + 1);
        }

        private static bool IsInScene(Size size, Point location)
        {
            return location.X >= 0 && location.X < size.Width && location.Y >= 0 && location.Y < size.Height;
        }

        private static bool IsInScene<T>(T[,] scene, Point location)
        {
            return IsInScene(new Size(scene.GetLength(0), scene.GetLength(1)), location);
        }

        private static bool IsNeighbor(Point location1, Point location2)
        {
            var dx = location1.X - location2.X;
            var dy = location1.Y - location2.Y;

            return dx * dx + dy * dy == 1;
        }

        private static bool ClearToGo(bool[,] scene, Point location)
        {
            return true;

            //var neighbors = new[]
            //                {
            //                    LeftOf(location),
            //                    TopOf(LeftOf(location)),
            //                    TopOf(location),
            //                    TopOf(RightOf(location)),
            //                    RightOf(location),
            //                    BottomOf(RightOf(location)),
            //                    BottomOf(location),
            //                    BottomOf(LeftOf(location))
            //                };

            //var used = neighbors.Where(p => IsInScene(scene, p) && scene[p.X, p.Y]).ToArray();

            //return used.Length == 1 || (used.Length == 2 && IsNeighbor(used[0], used[1]));
        }

        private static IReadOnlyDictionary<Point, int> Generate(Size size, Point location, int maxSteps = int.MaxValue)
        {
            var scene = new bool[size.Width, size.Height];
            var path = new Dictionary<Point, int>() { { location, 1 } };
            var step = 1;

            scene[location.X, location.Y] = true;

            for (;;)
            {
                var nextCandicates = new[] { LeftOf(location), TopOf(location), RightOf(location), BottomOf(location) };
                var nexts = nextCandicates.Where(p => IsInScene(size, p) && ClearToGo(scene, p)).ToArray();

                if (nexts.Length == 0)
                {
                    break;
                }

                var next = nexts[TheRandom.Next(nexts.Length)];

                scene[next.X, next.Y] = true;
                path[next] = path.ContainsKey(next) ? path[next] + 1 : 1;

                if (step == maxSteps)
                {
                    break;
                }
                else
                {
                    ++step;
                    location = next;
                }
            }

            return path;
        }

        private static void Main()
        {
            var size = new Size(1024, 1024);
            var location = new Point(512, 512);
            const int targetLength = 100000;
            var path = Generate(size, location, targetLength);
            var maxLength = path.Count;

            //while (path.Count < targetLength)
            //{
            //    path = Generate(size, location, targetLength);

            //    if (path.Count > maxLength)
            //    {
            //        maxLength = path.Count;

            //        Console.WriteLine(maxLength);
            //    }
            //}

            var max = path.Values.Max();

            using (var bitmap = new Bitmap(size.Width, size.Height))
            {
                foreach (var stop in path)
                {
                    bitmap.SetPixel(stop.Key.X, stop.Key.Y, Color.FromArgb(stop.Value * 255 / max, Color.Black));
                }

                bitmap.Save(@"E:\1.png");
            }
        }
    }
}
