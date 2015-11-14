using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ColorSpace.Common
{
    internal static class BitmapGenerator
    {
        public static void Generate(string outputPath, string fileNameFormat, int count, int width, int height, Func<double, double, double, Color?> f)
        {
            Directory.CreateDirectory(outputPath);

            Parallel.ForEach(Enumerable.Range(0, count), i =>
            {
                double p = i / (count - 1.0);

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            var value = f(p, (x + 0.5) / width, 1.0 - (y + 0.5) / height);

                            if (value != null)
                            {
                                bitmap.SetPixel(x, y, value.Value);
                            }
                        }
                    }

                    bitmap.Save(Path.Combine(outputPath, string.Format(fileNameFormat, p)));
                }
            });
        }

        public static void Generate(string outputPath, string fileNameFormat, int count, int width, int height, Func<double, int, int, Color?> f)
        {
            Directory.CreateDirectory(outputPath);

            Parallel.ForEach(Enumerable.Range(0, count), i =>
            {
                double p = i / (count - 1.0);

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            var value = f(p, x, height - 1 - y);

                            if (value != null)
                            {
                                bitmap.SetPixel(x, y, value.Value);
                            }
                        }
                    }

                    bitmap.Save(Path.Combine(outputPath, string.Format(fileNameFormat, p)));
                }
            });
        }
    }
}
