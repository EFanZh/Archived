using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InfiniteFloor
{
    internal static class Program
    {
        private static uint GetColorFromTile(double x, double y)
        {
            return (x < 0.5) == (y < 0.5) ? 0u : 1u;
        }

        private static double GetPosition(double x, double range)
        {
            double result = x % range;

            return result < 0 ? result + range : result;
        }

        private static uint GetColor(double tileSize, double x, double y)
        {
            return GetColorFromTile(GetPosition(x + tileSize / 4, tileSize) / tileSize, GetPosition(y, tileSize) / tileSize);
        }

        private static Tuple<double, double> GetIntersection(double cameraZ, double cameraAngleOfView, double viewWidth, double viewHeight, double viewX, double viewY)
        {
            double k = cameraZ / (2 * viewY - viewHeight);

            return Tuple.Create((viewX * 2.0 - viewWidth) * k, viewWidth / Math.Tan(cameraAngleOfView / 2.0) * k);
        }

        private static uint[] Generate(int width, int height, double tileSize, double cameraZ, double cameraAngleOfView, uint iteration)
        {
            var random = new Random();
            var result = new uint[width * height];

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (uint i = 1; i <= iteration; ++i)
            {
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        var intersection = GetIntersection(cameraZ, cameraAngleOfView, width, height, x + random.NextDouble(), y + random.NextDouble());

                        result[width * y + x] += GetColor(tileSize, intersection.Item1, intersection.Item2);
                    }
                }

                Console.WriteLine($"Iteration {i} / {iteration}, Remaining time: {TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds * ((iteration - i) / (double)i))}");
            }

            return result;
        }

        private static WriteableBitmap ToBitmapImage(int width, int height, uint[] buffer)
        {
            var bitmap = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Gray32Float, null);
            var max = (double)buffer.Max();
            var pixelBuffer = buffer.Select(x => (float)(x / max)).ToArray();

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelBuffer, sizeof(float) * width, 0);

            return bitmap;
        }

        private static void Do(int width, int height, double tileSize, double cameraZ, double cameraAngleOfView, uint iteration, string path)
        {
            var encoder = new PngBitmapEncoder();
            var buffer = Generate(width, height, tileSize, cameraZ, cameraAngleOfView, iteration);
            var bitmap = ToBitmapImage(width, height, buffer);

            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(new FileStream(path, FileMode.Create));
        }

        private static void Main(string[] args)
        {
            try
            {
                var width = int.Parse(args[0]);
                var height = int.Parse(args[1]);
                var tileSize = double.Parse(args[2]);
                var cameraZ = double.Parse(args[3]);
                var cameraAngleOfView = double.Parse(args[4]) * (Math.PI / 180.0);
                var iterations = uint.Parse(args[5]);
                var output = args[6];

                Do(width, height, tileSize, cameraZ, cameraAngleOfView, iterations, output);
            }
            catch (Exception)
            {
                Console.WriteLine("Parameters: width height tileSize cameraZ cameraAngleOfView iterations output");
            }
        }
    }
}
