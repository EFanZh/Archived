using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoContrast
{
    internal static class Program
    {
        private const int workingChannelCount = 4;
        private static readonly PixelFormat workingFormat = PixelFormats.Rgba128Float;

        private static BitmapSource LoadBitmap(string path)
        {
            BitmapSource bitmapSource = new BitmapImage(new Uri(Path.GetFullPath(path)));

            if (bitmapSource.Format != workingFormat)
            {
                bitmapSource = new FormatConvertedBitmap(bitmapSource, workingFormat, null, 0.5);
            }

            return bitmapSource;
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

        private static Tuple<double, double> GetLevelRange(float[] buffer)
        {
            var min = buffer[0];
            var max = buffer[0];

            for (int i = 0; i < buffer.Length; i += workingChannelCount)
            {
                if (buffer[i] < min)
                {
                    min = buffer[i];
                }
                else if (buffer[i] > max)
                {
                    max = buffer[i];
                }

                if (buffer[i + 1] < min)
                {
                    min = buffer[i + 1];
                }
                else if (buffer[i + 1] > max)
                {
                    max = buffer[i + 1];
                }

                if (buffer[i + 2] < min)
                {
                    min = buffer[i + 2];
                }
                else if (buffer[i + 2] > max)
                {
                    max = buffer[i + 2];
                }
            }

            return Tuple.Create((double)min, (double)max);
        }

        private static BitmapSource AutoContrast(BitmapSource bitmapSource)
        {
            var floatStride = workingChannelCount * bitmapSource.PixelWidth;
            var buffer = new float[floatStride * bitmapSource.PixelHeight];

            bitmapSource.CopyPixels(buffer, sizeof(float) * floatStride, 0);

            var levelRange = GetLevelRange(buffer);
            var min = levelRange.Item1;
            var range = levelRange.Item2 - levelRange.Item1;

            if (range > 0.0)
            {
                for (int i = 0; i < buffer.Length; i += workingChannelCount)
                {
                    buffer[i] = (float)((buffer[i] - min) / range);
                    buffer[i + 1] = (float)((buffer[i + 1] - min) / range);
                    buffer[i + 2] = (float)((buffer[i + 2] - min) / range);
                }
            }

            return BitmapSource.Create(bitmapSource.PixelWidth, bitmapSource.PixelHeight, bitmapSource.DpiX, bitmapSource.DpiY, workingFormat, null, buffer, sizeof(float) * floatStride);
        }

        private static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var source = args[0];
                var destination = args[1];

                try
                {
                    SaveBitmap(AutoContrast(LoadBitmap(source)), destination);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("Parameters: source destination");
            }
        }
    }
}
