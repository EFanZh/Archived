using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SortImageColors
{
    internal static class Program
    {
        private static readonly PixelFormat workingPixelFormat = PixelFormats.Rgba128Float;
        private const int channels = 4;

        private struct Rgba : IComparable<Rgba>
        {
            public float R
            {
                get;
                set;
            }

            public float G
            {
                get;
                set;
            }

            public float B
            {
                get;
                set;
            }

            public float A
            {
                get;
                set;
            }

            private double GetLuminance()
            {
                const double m21 = 1063.0 / 5000.0;
                const double m22 = 447.0 / 625.0;
                const double m23 = 361.0 / 5000.0;

                return (m21 * R + m22 * G + m23 * B) * A;
            }

            public int CompareTo(Rgba other)
            {
                return Math.Sign(GetLuminance() - other.GetLuminance());
            }
        }

        private static BitmapSource SortColors(BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;

            if (source.Format != workingPixelFormat)
            {
                source = new FormatConvertedBitmap(source, workingPixelFormat, null, 0.5);
            }

            var componentBuffer = new float[channels * width * height];
            var stride = sizeof(float) * channels * width;

            source.CopyPixels(componentBuffer, stride, 0);

            var pixelBuffer = new Rgba[width * height];

            for (int i = 0; i < pixelBuffer.Length; i++)
            {
                var bufferOffset = channels * i;

                pixelBuffer[i].R = componentBuffer[bufferOffset];
                pixelBuffer[i].G = componentBuffer[bufferOffset + 1];
                pixelBuffer[i].B = componentBuffer[bufferOffset + 2];
                pixelBuffer[i].A = componentBuffer[bufferOffset + 3];
            }

            Array.Sort(pixelBuffer);

            for (int i = 0; i < pixelBuffer.Length; i++)
            {
                var bufferOffset = channels * i;

                componentBuffer[bufferOffset] = pixelBuffer[i].R;
                componentBuffer[bufferOffset + 1] = pixelBuffer[i].G;
                componentBuffer[bufferOffset + 2] = pixelBuffer[i].B;
                componentBuffer[bufferOffset + 3] = pixelBuffer[i].A;
            }

            return BitmapSource.Create(width, height, source.DpiX, source.DpiY, workingPixelFormat, null, componentBuffer, stride);
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
                    throw new NotSupportedException("File name extension not supported.");
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
                    SaveBitmap(SortColors(new BitmapImage(new Uri(Path.GetFullPath(source)))), destination);
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
