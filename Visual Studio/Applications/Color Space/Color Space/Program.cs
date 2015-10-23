using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ColorSpace
{
    internal static class Program
    {
        private static int RoundToInt(double x)
        {
            return (int)Math.Round(x);
        }

        private static Color ToColor(this ColorSrgb colorSrgb)
        {
            if (double.IsNaN(colorSrgb.R) || double.IsNaN(colorSrgb.G) || double.IsNaN(colorSrgb.B))
            {
                return Color.Transparent;
            }

            return Color.FromArgb(255,
                (int)(Math.Round(colorSrgb.R * 255.0)),
                (int)(Math.Round(colorSrgb.G * 255.0)),
                (int)(Math.Round(colorSrgb.B * 255.0)));
        }

        private static Bitmap GenerateLabPlane(Bitmap bitmap, double labLValue)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double a = 220 * ((double)x / width - 0.5);
                    double b = 220 * (1.0 - (double)y / height - 0.5);

                    ColorLabD65 lab = new ColorLabD65() { L = labLValue, A = a, B = b };
                    ColorSrgb srgb = lab.ToColorXyz().ToSrgbLinear().ToCropped().ToColorSrgb();

                    bitmap.SetPixel(x, y, srgb.ToColor());
                }
            }

            return bitmap;
        }

        private static void GenerateColorRing(Bitmap bitmap, double bigY)
        {
            double centerX = (bitmap.Width - 1) / 2.0;
            double centerY = (bitmap.Height - 1) / 2.0;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    double dx = x - centerX;
                    double dy = y - centerY;
                    double dxp = dx / (bitmap.Width / 2.0);
                    double dyp = dy / (bitmap.Height / 2.0);
                    double dp = Math.Sqrt(dxp * dxp + dyp * dyp);
                    double angle = (Math.Atan2(dy, dx) / Math.PI + 1.0) * 3.0;
                    ColorVector color = new ColorVector();

                    if (angle < 1.0) // Red To Yellow.
                    {
                        color.Component1 = 1.0;
                        color.Component2 = angle;
                    }
                    else if (angle < 2.0) // Yellow To Green.
                    {
                        color.Component1 = 2.0 - angle;
                        color.Component2 = 1.0;
                    }
                    else if (angle < 3.0) // Green To Cyan.
                    {
                        color.Component2 = 1.0;
                        color.Component3 = angle - 2.0;
                    }
                    else if (angle < 4.0)//Cyan To Blue.
                    {
                        color.Component2 = 4.0 - angle;
                        color.Component3 = 1.0;
                    }
                    else if (angle < 5.0) // Blue To Purple.
                    {
                        color.Component1 = angle - 4.0;
                        color.Component3 = 1.0;
                    }
                    else // Purple To Red.
                    {
                        color.Component1 = 1.0;
                        color.Component3 = 6.0 - angle;
                    }

                    color.Component1 = 1.0 + (color.Component1 - 1.0) * dp;
                    color.Component2 = 1.0 + (color.Component2 - 1.0) * dp;
                    color.Component3 = 1.0 + (color.Component3 - 1.0) * dp;

                    color.ConvertLinearSRgbToSRgb();

                    bitmap.SetPixel(x, y, color.ToColor());
                }
            }
        }

        private static void GenerateColorRingWithColors(Bitmap bitmap, int step)
        {
            for (int r = 0; r < 256; r += step)
            {
                for (int g = 0; g < 256; g += step)
                {
                    for (int b = 0; b < 256; b += step)
                    {
                        ColorSrgb srgb = new ColorSrgb()
                        {
                            R = r / 256.0,
                            G = g / 256.0,
                            B = b / 256.0
                        };

                        ColorXyz xyz = srgb.ToColorSrgbLinear().ToColorXyz();

                        double angle = xyz.H * Math.PI * 2.0;
                        double d = xyz.Y;

                        if (double.IsNaN(angle) || double.IsNaN(d))
                        {
                            continue;
                        }

                        double dxp = (Math.Cos(angle) * (1.0 - d) + 1.0) / 2.0;
                        double dyp = (Math.Sin(angle) * (1.0 - d) + 1.0) / 2.0;
                        int x = RoundToInt((bitmap.Width - 1.0) * dxp);
                        int y = RoundToInt((bitmap.Height - 1.0) * dyp);

                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }
        }

        private static Tuple<double, double, double> XyYToSRgb(double x, double y, double bigY)
        {
            ColorVector color = new ColorVector(x, y, bigY);

            color.ConvertXyyToSRgbSmart();

            return Tuple.Create(color.Component1, color.Component2, color.Component3);
        }

        private static Bitmap GenerateRainbow(IList<ColorVector> colors)
        {
            Bitmap bitmap = new Bitmap(colors.Count, 256);

            for (int i = 0; i < colors.Count; i++)
            {
                ColorVector color = colors[i];

                color.ConvertXyzToSRgbSmart();

                Color c = color.ToColor();

                for (int y = 0; y < bitmap.Height; y++)
                {
                    bitmap.SetPixel(i, y, c);
                }
            }

            return bitmap;
        }

        private static void GenerateXyYPlane(Bitmap bitmap, double bigY)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    double cx = (x + 0.5) / bitmap.Width;
                    double cy = (y + 0.5) / bitmap.Height;
                    ColorVector colorVector = new ColorVector(cx, cy, bigY);

                    colorVector.ConvertXyyToSRgb();
                    colorVector.CompressLuminance();
                    colorVector.ConvertLinearSRgbToSRgb();

                    bitmap.SetPixel(x, bitmap.Height - 1 - y, colorVector.ToColor());
                }
                Console.WriteLine(y);
            }
        }

        private class ColorComparer : IComparer<KeyValuePair<Color, double>>
        {
            public int Compare(KeyValuePair<Color, double> x, KeyValuePair<Color, double> y)
            {
                if (x.Value < y.Value)
                {
                    return -1;
                }
                else if (x.Value > y.Value)
                {
                    return 1;
                }
                else
                {
                    int result = x.Key.R - y.Key.R;

                    if (result != 0)
                    {
                        return result;
                    }

                    result = x.Key.G - y.Key.G;

                    if (result != 0)
                    {
                        return result;
                    }

                    return x.Key.B - y.Key.B;
                }
            }
        }

        private static IEnumerable<KeyValuePair<Color, double>> SortColors()
        {
            var result = new KeyValuePair<Color, double>[256 * 256 * 256];
            int i = 0;

            for (short r = 0; r < 256; ++r)
            {
                for (short g = 0; g < 256; ++g)
                {
                    for (short b = 0; b < 256; ++b)
                    {
                        ColorVector colorVector = new ColorVector(r / 255.0, g / 255.0, b / 255.0);

                        colorVector.ConvertSRgbToLinearSRgb();
                        colorVector.ConvertLinearSRgbToXyz();

                        result[i] = new KeyValuePair<Color, double>(Color.FromArgb(r, g, b), colorVector.Component2);

                        if (i % 1000000 == 0)
                        {
                            Console.WriteLine(i);
                        }

                        i++;
                    }
                }
            }

            return result.OrderBy(t => t, new ColorComparer());
        }

        private static void Main(string[] args)
        {
            using (Bitmap bitmap = new Bitmap(4096, 4096))
            {
                int i = 0;

                foreach (var color in SortColors())
                {
                    int row = i / 4096;
                    int column = i % 4096;

                    bitmap.SetPixel(column, row, color.Key);

                    if (column == 0)
                    {
                        Console.WriteLine(row);
                    }

                    ++i;
                }

                bitmap.Save("E:\\1.png");
            }
        }
    }
}
