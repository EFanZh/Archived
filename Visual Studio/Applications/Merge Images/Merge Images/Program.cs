using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace MergeImages
{
    internal class Program
    {
        private static Bitmap Merge(Bitmap[] bitmaps, Size target_size)
        {
            Bitmap target_bitmap = new Bitmap(target_size.Width, target_size.Height);
            int count = bitmaps.Length;
            double p = 1.0 / count;

            for (int i = 0; i < target_size.Width; i++)
            {
                for (int j = 0; j < target_size.Height; j++)
                {
                    double va = 0.0, vr = 0.0, vg = 0.0, vb = 0.0;
                    foreach (var bmp in bitmaps)
                    {
                        if (i < bmp.Width && j < bmp.Height)
                        {
                            va += p * bmp.GetPixel(i, j).A;
                            vr += p * bmp.GetPixel(i, j).R;
                            vg += p * bmp.GetPixel(i, j).G;
                            vb += p * bmp.GetPixel(i, j).B;
                        }
                    }
                    target_bitmap.SetPixel(i, j, Color.FromArgb(ToInt(va), ToInt(vr), ToInt(vg), ToInt(vb)));
                }
                Console.WriteLine("{0} / {1}", i + 1, target_size.Width);
            }

            return target_bitmap;
        }

        private static int ToInt(double x)
        {
            return (int)Math.Round(x);
        }

        private static void Main(string[] args)
        {
            int wait_time = 10;
            Console.WriteLine("Wait {0} seconds...", wait_time);
            Stopwatch sw = new Stopwatch();
            int total_time = wait_time * 1000;
            sw.Start();
            for (int i = wait_time, t = 1000; i >= 0; i--, t += 1000)
            {
                Console.WriteLine(i);
                while (sw.ElapsedMilliseconds < t)
                {
                    continue;
                }
            }

            int count = 120;
            Size size = new Size(1440, 900);
            Bitmap[] bitmaps = new Bitmap[count];
            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.CopyFromScreen(0, 0, 0, 0, b.Size, CopyPixelOperation.SourceCopy);
                }
                bitmaps[i] = b;
                Console.WriteLine("{0} / {1}", i + 1, count);
                int t = (i + 1) * 1000;
                while (sw.ElapsedMilliseconds < t)
                {
                    continue;
                }
            }
            Merge(bitmaps, size).Save("D:\\1.png");
        }
    }
}
