using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageSeparate
{
    internal static class Program
    {
        private static List<int>[] GenerateConnectionGraph(Bitmap bitmap)
        {
            var result = Enumerable.Range(0, bitmap.Width * bitmap.Height).Select(i => new List<int>(4)).ToArray();

            for (int i = 0; i < result.Length; i++)
            {
                int x = i % bitmap.Width;
                int y = i / bitmap.Width;
                Color color = bitmap.GetPixel(x, y);
                int minDistance = int.MaxValue;
                var q = new List<List<int>>();

                foreach (var next in GetNexts(x, y, bitmap.Width, bitmap.Height))
                {
                    int d = GetDistance(color, bitmap.GetPixel(next.X, next.Y));

                    if (d < minDistance)
                    {
                        minDistance = d;

                        if (q.Count == 1)
                        {
                            q.RemoveAt(0);
                        }

                        q.Add(new List<int>());

                        q.Last().Add(bitmap.Width * next.Y + next.X);
                    }
                    else if (d == minDistance)
                    {
                        q.Last().Add(bitmap.Width * next.Y + next.X);
                    }
                }

                result[i].AddRange(q.SelectMany(k => k));

                // Add reverse connection.
                foreach (var next in result[i])
                {
                    if (!result[next].Contains(i))
                    {
                        result[next].Add(i);
                    }
                }
            }

            return result;
        }

        private static int[] GetClosure(List<int>[] connectionGraph, int i, bool[] visited)
        {
            var result = new List<int>();
            var q = new Queue<int>();

            q.Enqueue(i);

            while (q.Count > 0)
            {
                int current = q.Dequeue();

                result.Add(current);

                foreach (var next in connectionGraph[current].Where(next => !visited[next]))
                {
                    visited[next] = true;

                    q.Enqueue(next);
                }
            }

            return result.ToArray();
        }

        private static int GetDistance(Color c1, Color c2)
        {
            int dr = (int)c1.R - (int)c2.R;
            int dg = (int)c1.G - (int)c2.G;
            int db = (int)c1.B - (int)c2.B;

            return dr * dr + dg * dg + db * db;
        }

        private static IEnumerable<Point> GetNexts(int x, int y, int width, int height)
        {
            if (x > 0)
            {
                yield return new Point(x - 1, y);
            }

            if (y > 0)
            {
                yield return new Point(x, y - 1);
            }

            if (x < width - 1)
            {
                yield return new Point(x + 1, y);
            }

            if (y < height - 1)
            {
                yield return new Point(x, y + 1);
            }
        }

        private static Bitmap GenereateClosureBitmap(Bitmap sourceBitmap, int[] closure)
        {
            Bitmap bitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            foreach (var i in closure)
            {
                int x = i % sourceBitmap.Width;
                int y = i / sourceBitmap.Width;

                bitmap.SetPixel(x, y, sourceBitmap.GetPixel(x, y));
            }

            return bitmap;
        }

        private static void Main(string[] args)
        {
            Bitmap bitmap = new Bitmap(@"E:\EFanZh\Temp\[C2&A.I.R.nesSub][Non_Non_Biyori][07][BDRIP][1920x1080_10bpp][AVC_FLACx2].mkv_snapshot_11.38_[2015.08.09_21.04.24] - Copy.png");
            var graph = GenerateConnectionGraph(bitmap);
            var visited = new bool[bitmap.Width * bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int i = bitmap.Width * y + x;

                    if (!visited[i])
                    {
                        visited[i] = true;

                        var closure = GetClosure(graph, i, visited);

                        if (closure.Length >= 256)
                        {
                            GenereateClosureBitmap(bitmap, closure).Save($"{y} - {x}.png");
                        }
                    }
                }

                // Temp.
                Console.WriteLine(y);
                GC.Collect();
            }
        }
    }
}
