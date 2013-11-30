using System;
using System.Drawing;
using System.Linq;

namespace ColorSpace
{
    internal class Program
    {
        private static Color XYZTosRGB(double x, double y, double z)
        {
            double total = 1.0;
            double sum = x + y + z;
            x = x / sum * total;
            y = y / sum * total;
            z = z / sum * total;
            double r1 = 3.2406 * x - 1.5372 * y - 0.4986 * z;
            double g1 = -0.9689 * x + 1.8758 * y + 0.0415 * z;
            double b1 = 0.0557 * x - 0.204 * y + 1.057 * z;

            Func<double, double> f = c =>
            {
                double t;

                if (c <= 0.04045)
                {
                    t = c / 12.92;
                }
                else
                {
                    t = Math.Pow((c + 0.055) / 1.055, 2.4);
                }

                if (t < 0.0)
                {
                    return 0.0;
                }
                else if (t > 1.0)
                {
                    return 1.0;
                }
                else
                {
                    return t;
                }
            };

            Func<double, int> k = d => (int)Math.Round(255.0 * f(d));

            return Color.FromArgb(k(r1), k(g1), k(b1));
        }

        private static void Main()
        {
            var data = CIE1931XYZ.GetColorMatchFunction();
            Bitmap bitmap = new Bitmap(data.Count(), 256);
            Graphics g = Graphics.FromImage(bitmap);

            int x = 0;
            foreach (var item in data)
            {
                g.DrawLine(new Pen(XYZTosRGB(item[1], item[2], item[3])), x, 0, x, 256);
                x++;
            }

            bitmap.Save(@"D:\1.png");
        }
    }
}
