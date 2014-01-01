using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ColorSpace
{
    internal class Program
    {
        private static Random random = new Random();

        private static int GetDitheredValue(double x)
        {
            int i = (int)x;
            double xd = x - i;
            return random.NextDouble() < xd ? i + 1 : i;
        }

        private static double[] xyYToXYZ(double[] xyY)
        {
            double t = xyY[2] / xyY[1];
            return new[] { t * xyY[0], xyY[2], t * (1 - xyY[0] - xyY[1]) };
        }

        private static double[] XYZTosRGB(double[] XYZ)
        {
            double[] rgb_l = new[]
            {
                3.2406 * XYZ[0] - 1.5372 * XYZ[1] - 0.4986 * XYZ[2],
                -0.9689 * XYZ[0] + 1.8758 * XYZ[1] + 0.0415 * XYZ[2],
                0.0557 * XYZ[0] - 0.204 * XYZ[1] + 1.057 * XYZ[2]
            };

            return rgb_l.Select(c => c <= 0.0031308 ? 12.92 * c : 1.055 * Math.Pow(c, 1.0 / 2.4) - 0.055).ToArray();
        }

        private static double[] CompressRGB(double[] rgb)
        {
            var rgb2 = rgb.Select(c => c < 0.0 ? 0.0 : c < 1.0 ? c : 1.0);
            return rgb2.ToArray();
        }

        private static Color sRGBToColor(double[] sRGB)
        {
            int[] v = sRGB.Select(c => GetDitheredValue(255.0 * c)).ToArray();

            return Color.FromArgb(v[0], v[1], v[2]);
        }

        private static Bitmap GenerateSpectrumGraph(IEnumerable<double[]> data, int size)
        {
            Bitmap bitmap = new Bitmap(data.Count(), size);
            Graphics g = Graphics.FromImage(bitmap);

            int x = 0;
            foreach (var item in data)
            {
                double t = 0.3;
                g.DrawLine(new Pen(sRGBToColor(CompressRGB(XYZTosRGB(item.Skip(1).Select(c => c * t).ToArray())))), x, 0, x, size);
                x++;
            }

            return bitmap;
        }

        private static Bitmap GenerateChromaticityGraph(int size)
        {
            Bitmap bitmap = new Bitmap(size, size);
            PointF pr = new PointF(0.64f * size, size - 0.33f * size);
            PointF pg = new PointF(0.3f * size, size - 0.6f * size);
            PointF pb = new PointF(0.15f * size, size - 0.06f * size);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    bitmap.SetPixel(x, size - 1 - y, sRGBToColor(CompressRGB(XYZTosRGB(xyYToXYZ(new[] { (x + 0.5) / size, (y + 0.5) / size, 0.0721 })))));
                }
            }

            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawPolygon(Pens.Black, new[] { pr, pg, pb });

            return bitmap;
        }

        private static void Main()
        {
            GenerateSpectrumGraph(CIE1931XYZ.GetColorMatchFunction(), 256).Save("D:\\1.png");
            GenerateChromaticityGraph(2560).Save("D:\\2.png");
        }
    }
}
