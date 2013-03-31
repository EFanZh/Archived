using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DotGradient
{
    internal static class MyGradientGenerator
    {
        public static Bitmap GenerateGradient(int width, int height, Tuple<Tuple<double, double>, Color>[] data)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bitmap_data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            IntPtr scan0 = bitmap_data.Scan0;
            int data_count = data.Length;
            int[] x_offset_cache = new int[width], y_offset_cache = new int[height];

            for (int i = 0; i < width; i++)
            {
                x_offset_cache[i] = i * 4;
            }

            for (int i = 0; i < height; i++)
            {
                y_offset_cache[i] = bitmap_data.Stride * i;
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double[] distances = new double[data_count];
                    for (int i = 0; i < data_count; i++)
                    {
                        double x_distance = x - data[i].Item1.Item1;
                        double y_distance = y - data[i].Item1.Item2;
                        distances[i] = Math.Sqrt(x_distance * x_distance + y_distance * y_distance);
                    }
                    double a = 0.0;
                    double ad = 0.0, rd = 0.0, gd = 0.0, bd = 0.0;
                    for (int i = 0; i < data_count; i++)
                    {
                        double b = 1.0;
                        for (int j = 0; j < data_count; j++)
                        {
                            if (i != j)
                            {
                                b *= distances[j];
                            }
                        }
                        a += b;
                        ad += b * data[i].Item2.A;
                        rd += b * data[i].Item2.R;
                        gd += b * data[i].Item2.G;
                        bd += b * data[i].Item2.B;
                    }
                    int offset = x_offset_cache[x] + y_offset_cache[y];
                    Marshal.WriteByte(scan0, offset, (byte)(bd / a));
                    Marshal.WriteByte(scan0, offset + 1, (byte)(gd / a));
                    Marshal.WriteByte(scan0, offset + 2, (byte)(rd / a));
                    Marshal.WriteByte(scan0, offset + 3, (byte)(ad / a));
                }
            }
            bitmap.UnlockBits(bitmap_data);
            return bitmap;
        }
    }
}
