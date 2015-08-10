using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ColorSpace
{
    internal class Program
    {
        private static Bitmap Generate(double t)
        {
            int width = 512;
            int height = 512;

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double a = 200 * ((double)x / width - 0.5);
                    double b = 200 * (1.0 - (double)y / height - 0.5);

                    ColorLab lab = new ColorLab() { L = t, A = a, B = b };
                    ColorSrgb srgb = lab.ToColorXyz().ToSrgbLinear().ToCropped().ToColorSrgb();

                    bitmap.SetPixel(x, y, Color.FromArgb(255, (int)(Math.Round(srgb.R * 255.0)), (int)(Math.Round(srgb.G * 255.0)), (int)(Math.Round(srgb.B * 255.0))));
                }
            }

            return bitmap;
        }

        private static void Main(string[] args)
        {
            const int count = 256;
            int tasks = count;

            Parallel.For(0, count, i =>
            {
                Generate(i * 100.0 / count).Save($"Colors - {i:000}.png");
                --tasks;
                Console.WriteLine(tasks);
            });
        }
    }
}
