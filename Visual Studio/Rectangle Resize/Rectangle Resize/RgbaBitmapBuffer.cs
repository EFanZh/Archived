using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RectangleResize
{
    internal class RgbaBitmapBuffer
    {
        private const int workingChannelCount = 4;
        private static readonly PixelFormat workingFormat = PixelFormats.Rgba128Float;
        private float[] buffer;
        private int floatStride;

        private RgbaBitmapBuffer(float[] buffer, int floatStride)
        {
            this.buffer = buffer;
            this.floatStride = floatStride;
        }

        public RgbaBitmapBuffer(int width, int height)
        {
            floatStride = workingChannelCount * width;
            buffer = new float[floatStride * height];
        }

        public Pixel GetPixel(int x, int y)
        {
            int start = floatStride * y + workingChannelCount * x;

            return new Pixel(buffer[start], buffer[start + 1], buffer[start + 2], buffer[start + 3]);
        }

        public void SetPixel(int x, int y, Pixel value)
        {
            int start = floatStride * y + workingChannelCount * x;

            buffer[start] = (float)value.C0;
            buffer[start + 1] = (float)value.C1;
            buffer[start + 2] = (float)value.C2;
            buffer[start + 3] = (float)value.C3;
        }

        public RgbaBitmapBuffer Copy()
        {
            return new RgbaBitmapBuffer(buffer.ToArray(), floatStride);
        }

        public BitmapSource ToBitmapSource(double dpiX, double dpiY)
        {
            return ToBitmapSource(floatStride / workingChannelCount, buffer.Length / floatStride);
        }

        public BitmapSource ToBitmapSource(int width, int height, double dpiX, double dpiY)
        {
            var result = new WriteableBitmap(width, height, dpiX, dpiY, workingFormat, null);

            result.WritePixels(new Int32Rect(0, 0, result.PixelWidth, result.PixelHeight), buffer, sizeof(float) * floatStride, 0);

            return result;
        }

        public static RgbaBitmapBuffer FromBitmapSource(BitmapSource bitmapSource)
        {
            if (bitmapSource.Format != workingFormat)
            {
                bitmapSource = new FormatConvertedBitmap(bitmapSource, workingFormat, null, 0.5);
            }

            var floatStride = workingChannelCount * bitmapSource.PixelWidth;
            var buffer = new float[floatStride * bitmapSource.PixelHeight];

            bitmapSource.CopyPixels(buffer, sizeof(float) * floatStride, 0);

            return new RgbaBitmapBuffer(buffer, floatStride);
        }
    }
}
