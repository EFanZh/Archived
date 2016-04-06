using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace RectangleResize
{
    internal static class Program
    {
        private static RgbaBitmapBuffer Resize(BitmapSource input, int width, int height)
        {
            var inputBuffer = RgbaBitmapBuffer.FromBitmapSource(input);
            var outputBuffer = new RgbaBitmapBuffer(width, height);

            Resizer.Resize(input.PixelWidth,
                           input.PixelHeight,
                           inputBuffer.GetPixel,
                           width,
                           height,
                           outputBuffer.GetPixel,
                           outputBuffer.SetPixel,
                           new MathOperators<Pixel, int>(Pixel.Plus, Pixel.Multiply, Pixel.Divide));

            return outputBuffer;
        }

        private static double GetLuma(Pixel pixel)
        {
            return 0.2126 * pixel.C0 + 0.7152 * pixel.C1 + 0.0722 * pixel.C2;
        }

        private static Pixel MergeRgb(Pixel p0, Pixel p1, Pixel p2)
        {
            var from = (p0 + p1 + p2) / 3;
            var to = new Pixel(p0.C0, p1.C1, p2.C2, from.C3);
            var p = 1.0;

            return from + (to - from) * p;
        }

        private static RgbaBitmapBuffer ResizeRgb(BitmapSource input, int width, int height)
        {
            var outputBuffer = Resize(input, width * 3, height);
            var blurredBuffer = outputBuffer.Copy();

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var sum = new Pixel(0.0, 0.0, 0.0, 0.0);
                    var weight = 0.0;
                    var w0 = 3.0;
                    var w1 = 2.0;
                    var w2 = 1.0;

                    if (x - 2 >= 0)
                    {
                        sum += outputBuffer.GetPixel(x - 2, y) * w2;
                        weight += w2;
                    }

                    if (x - 1 >= 0)
                    {
                        sum += outputBuffer.GetPixel(x - 1, y) * w1;
                        weight += w1;
                    }

                    sum += outputBuffer.GetPixel(x, y) * w0;
                    weight += w0;

                    if (x + 1 < width)
                    {
                        sum += outputBuffer.GetPixel(x + 1, y) * w1;
                        weight += w1;
                    }

                    if (x + 2 < width)
                    {
                        sum += outputBuffer.GetPixel(x + 2, y) * w2;
                        weight += w2;
                    }

                    blurredBuffer.SetPixel(x, y, sum / weight);
                }
            }

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var sourceX = 3 * x;
                    var c1 = blurredBuffer.GetPixel(sourceX, y);
                    var c2 = blurredBuffer.GetPixel(sourceX + 1, y);
                    var c3 = blurredBuffer.GetPixel(sourceX + 2, y);

                    blurredBuffer.SetPixel(x, y, MergeRgb(c1, c2, c3));
                }
            }

            return blurredBuffer;
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
                    encoder = new JpegBitmapEncoder();
                    ((JpegBitmapEncoder)encoder).QualityLevel = 100;
                    break;

                case ".PNG":
                    encoder = new PngBitmapEncoder();
                    break;

                case ".TIF":
                    encoder = new TiffBitmapEncoder();
                    ((TiffBitmapEncoder)encoder).Compression = TiffCompressOption.Zip;
                    break;

                default:
                    throw new NotSupportedException("Not supported output sextension.");
            }

            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(new FileStream(destination, FileMode.Create));
        }

        private static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Parameters: source destination width height [rgb]");
            }
            else
            {
                try
                {
                    var source = args[0];
                    var destination = args[1];
                    int width = int.Parse(args[2]);
                    int height = int.Parse(args[3]);

                    var input = new BitmapImage(new Uri(Path.GetFullPath(source)));

                    RgbaBitmapBuffer output;

                    if (args.Length > 4 && args[4] == "rgb")
                    {
                        output = ResizeRgb(input, width, height);
                    }
                    else
                    {
                        output = Resize(input, width, height);
                    }

                    SaveBitmap(output.ToBitmapSource(width, height, input.DpiX, input.DpiY), destination);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
