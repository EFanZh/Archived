using ColorSpace.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRainbow
{
    internal class Program
    {
        private static IReadOnlyList<ColorVector> GetColors()
        {
            var colors = new List<ColorVector>();

            SpectrumData.ForEach((x, y, z) =>
            {
                colors.Add(new ColorVector((double)x, (double)y, (double)z));

                return true;
            });

            return colors;
        }

        private static void Generate(IReadOnlyList<ColorVector> colors, double scaleY)
        {
            BitmapGenerator.Generate($@"E:\Rainbows\{scaleY:0.0000}", "rainbow-{0:0.000}.png", 101, colors.Count, 16, (double main, int x, int y) =>
            {
                main = 0.14 + main / 100;

                ColorVector color = colors[x];

                color.Component1 *= scaleY;
                color.Component2 *= scaleY;
                color.Component3 *= scaleY;

                ColorVector gray = new ColorVector((double)WhitePoints.D65X, (double)WhitePoints.D65Y, color.Component2);

                gray.ConvertXyyToXyz();

                double offsetX = color.Component1 - gray.Component1;
                double offsetZ = color.Component3 - gray.Component3;

                color.Component1 = gray.Component1 + main * offsetX;
                color.Component3 = gray.Component3 + main * offsetZ;

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
            });
        }

        private static void Main(string[] args)
        {
            var colors = GetColors();

            Parallel.ForEach(Enumerable.Range(0, 101), i => Generate(colors, 0.95 + i / 10000.0));
        }
    }
}
