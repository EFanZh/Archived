using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageSeparate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dict = new Dictionary<Color, List<Point>>();
            Bitmap bitmap = new Bitmap(@"D:\EFanZh\Data\Microsoft\Windows\Desktop\X.png");

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    List<Point> points;

                    if (!dict.TryGetValue(color, out points))
                    {
                        points = new List<Point>();
                        dict.Add(color, points);
                    }

                    points.Add(new Point(x, y));
                }
            }

            var colors = dict.OrderByDescending(item => item.Value.Count).ToList();
            var current = new HashSet<Point>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    current.Add(new Point(x, y));
                }
            }

            for (int i = 0; i < colors.Count; i++)
            {
                Console.WriteLine("{0} / {1}", i, colors.Count);

                using (Bitmap separatedBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    foreach (var point in current)
                    {
                        separatedBitmap.SetPixel(point.X, point.Y, colors[i].Key);
                    }

                    current.ExceptWith(colors[i].Value);

                    separatedBitmap.Save(string.Format(@"D:\EFanZh\Temp\Sep\{0:D5} - #{1:x2}{2:x2}{3:x2} [{4:P}].png",
                                                       i,
                                                       colors[i].Key.R,
                                                       colors[i].Key.G,
                                                       colors[i].Key.B,
                                                       (double)current.Count / (bitmap.Width * bitmap.Height)));
                }
            }
        }
    }
}
