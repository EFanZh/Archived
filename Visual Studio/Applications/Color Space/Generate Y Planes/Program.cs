using ColorSpace.Common;
using System.Drawing;

namespace GenerateYPlanes
{
    internal class Program
    {
        private static Color? GetColor(double main, double x, double y)
        {
            ColorVector color = new ColorVector(x, main, y);

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
            BitmapGenerator.Generate(@"E:\Colors", "colors-{0:0.000}.png", 101, 2000, 2000, new System.Func<double, double, double, Color?>(GetColor));
        }
    }
}
