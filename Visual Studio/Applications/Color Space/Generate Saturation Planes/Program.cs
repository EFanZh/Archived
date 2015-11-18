using ColorSpace.Common;
using System;
using System.Drawing;

namespace GenerateSaturationPlanes
{
    internal class Program
    {
        private static Color? GetColor(double saturation, double hue, double bigY)
        {
            double angle = 2.0 * Math.PI * hue;
            double scale = bigY / (double)WhitePoints.D65Y;
            double d65BigX = (double)WhitePoints.D65X * scale;
            double d65BigZ = (double)WhitePoints.D65Z * scale;

            ColorVector color = new ColorVector(d65BigX + saturation * Math.Cos(angle), bigY, d65BigZ + saturation * Math.Sin(angle));

            color.ConvertXyzToLinearSRgb();

            if (color.IsCanonical())
            {
                color.ConvertLinearSRgbToSRgb();

                return color.ToColor();
            }
            else
            {
                return null;
            }
        }

        private static void Main(string[] args)
        {
            BitmapGenerator.Generate(@"E:\Colors", "colors-{0:0.000}.png", 1001, 1000, 1000, new System.Func<double, double, double, Color?>(GetColor));
        }
    }
}
