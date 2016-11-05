using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SortImageColumns
{
    internal static class Program
    {
        private static readonly PixelFormat workingPixelFormat = PixelFormats.Rgba128Float;

        private static double GetLuminance(float[] array, int index)
        {
            const double m21 = 1063.0 / 5000.0;
            const double m22 = 447.0 / 625.0;
            const double m23 = 361.0 / 5000.0;

            var r = array[index];
            var g = array[index + 1];
            var b = array[index + 2];
            var a = array[index + 3];

            return (m21 * r + m22 * g + m23 * b) * a;
        }

        private static BitmapSource SortColumns(BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;

            if (source.Format != workingPixelFormat)
            {
                source = new FormatConvertedBitmap(source, workingPixelFormat, null, 0.5);
            }

            const int channels = 4;
            var floatsPerLine = channels * width;
            var stride = sizeof(float) * floatsPerLine;
            var buffer = new float[stride * height];

            source.CopyPixels(buffer, stride, 0);

            var working = new float[width][];

            for (var x = 0; x < working.Length; x++)
            {
                var column = new float[channels * height];

                for (var y = 0; y < height; y++)
                {
                    Array.Copy(buffer, floatsPerLine * y + channels * x, column, channels * y, channels);
                }

                working[x] = column;
            }

            var cache = new Dictionary<float[], double>();

            Func<float[], double> getKey = column =>
                                           {
                                               double result;

                                               if (cache.TryGetValue(column, out result))
                                               {
                                                   return result;
                                               }
                                               else
                                               {
                                                   double sum = 0.0;

                                                   for (var i = 0; i < column.Length; i += channels)
                                                   {
                                                       sum += GetLuminance(column, i);
                                                   }

                                                   cache.Add(column, sum);

                                                   return sum;
                                               }
                                           };

            Array.Sort(working,
                       (lhs, rhs) =>
                       {
                           return Math.Sign(getKey(lhs) - getKey(rhs));
                       });

            var target = new float[floatsPerLine * height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Array.Copy(working[x], channels * y, target, floatsPerLine * y + channels * x, channels);
                }
            }

            return BitmapSource.Create(width, height, source.DpiX, source.DpiY, workingPixelFormat, null, target, stride);
        }

        private static void SaveBitmap(BitmapSource bitmap, string destination)
        {
            BitmapEncoder encoder;

            switch (Path.GetExtension(destination).ToUpperInvariant())
            {
                case ".BMP":
                    encoder = new BmpBitmapEncoder();
                    break;

                case ".GIF":
                    encoder = new GifBitmapEncoder();
                    break;

                case ".JPG":
                    encoder = new JpegBitmapEncoder() { QualityLevel = 100 };
                    break;

                case ".PNG":
                    encoder = new PngBitmapEncoder();
                    break;

                case ".TIF":
                    encoder = new TiffBitmapEncoder() { Compression = TiffCompressOption.Zip };
                    break;

                default:
                    throw new NotSupportedException("Not supported output extension.");
            }

            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(new FileStream(destination, FileMode.Create));
        }

        private static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var source = args[0];
                var destination = args[1];

                try
                {
                    SaveBitmap(SortColumns(new BitmapImage(new Uri(Path.GetFullPath(source)))), destination);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                }
            }
            else
            {
                Console.WriteLine("Parameters: source destination");
            }
        }
    }
}
