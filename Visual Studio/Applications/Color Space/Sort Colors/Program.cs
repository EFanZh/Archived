using ColorSpace.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortColors
{
    internal class Program
    {
        private static IComparer<TSource> GetComparer<TSource, T>(Func<TSource, T> extractKey) where T : IComparable<T>
        {
            return Comparer<TSource>.Create((lhs, rhs) => extractKey(lhs).CompareTo(extractKey(rhs)));
        }

        private static void SortColors<T1, T2, T3>(Func<Color, T1> extractKey1, Func<Color, T2> extractKey2, Func<Color, T3> extractKey3, string folder)
            where T1 : IComparable<T1>
            where T2 : IComparable<T2>
            where T3 : IComparable<T3>
        {
            const int depth = 256;
            var comparer1 = GetComparer(extractKey1);
            var comparer2 = GetComparer(extractKey2);
            var comparer3 = GetComparer(extractKey3);

            Console.Write("Generate Colors...");
            var colors = Enumerable.Range(unchecked((int)0xff000000), depth * depth * depth).Select(Color.FromArgb).ToList();
            Console.WriteLine(" Done.");

            Console.Write("Sorting Colors...");
            colors.Sort(comparer1);
            Console.WriteLine(" Done.");

            Console.Write("Generating Bitmaps...");
            Parallel.ForEach(Enumerable.Range(0, depth), i =>
            {
                const int planeSize = depth * depth;
                int offset = planeSize * i;

                colors.Sort(offset, planeSize, comparer2);

                for (int k = 0; k < depth; ++k)
                {
                    colors.Sort(offset + depth * k, depth, comparer3);
                }

                using (Bitmap bitmap = new Bitmap(depth, depth))
                {
                    for (int k = 0; k < planeSize; ++k)
                    {
                        bitmap.SetPixel(k % depth, k / depth, colors[offset + k]);
                    }

                    bitmap.Save(Path.Combine(folder, $"Colors - {i:000}.png"));
                }
            });
            Console.WriteLine(" Done.");
        }

        private static double GetSaturation(Color color)
        {
            ColorVector colorVector = new ColorVector(color.R / 255.0, color.G / 255.0, color.B / 255.0);

            colorVector.ConvertSRgbToLinearSRgb();

            ColorVector colorVector2 = colorVector;

            colorVector2.ConvertLinearSRgbToXyz();
            colorVector2.ConvertXyzToXyy();
            colorVector2.Component1 = (double)WhitePoints.D65X;
            colorVector2.Component2 = (double)WhitePoints.D65Y;
            colorVector2.ConvertXyyToLinearSRgb();

            double dr = colorVector.Component1 - colorVector2.Component1;
            double dg = colorVector.Component2 - colorVector2.Component2;
            double db = colorVector.Component3 - colorVector2.Component3;

            return dr * dr + dg * dg + db * db;
        }

        private static double GetHue(Color color)
        {
            ColorVector colorVector = new ColorVector(color.R / 255.0, color.G / 255.0, color.B / 255.0);

            colorVector.ConvertSRgbToLinearSRgb();

            return colorVector.Hue;
        }

        private static double GetLuminance(Color color)
        {
            ColorVector colorVector = new ColorVector(color.R / 255.0, color.G / 255.0, color.B / 255.0);

            colorVector.ConvertSRgbToLinearSRgb();
            colorVector.ConvertLinearSRgbToXyz();

            return colorVector.Component2;
        }

        private static void Main(string[] args)
        {
            SortColors(GetSaturation, GetLuminance, GetHue, @"E:\EFanZh\Temp\Colors");
        }
    }
}
