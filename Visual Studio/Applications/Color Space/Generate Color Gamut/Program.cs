using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ColorSpace.Common;

namespace GenerateColorGamut
{
    class Program
    {
        static Bitmap Generate(int size)
        {
            var bitmap = new Bitmap(size, size);
            var list = new List<PointF>();
            var totalScale = 0.0192f * size;

            SpectrumData.ForEach((x, y, z) =>
            {
                list.Add(new PointF((float)(x / y), (float)(z / y)));

                return true;
            });

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.ScaleTransform(totalScale, totalScale);

                graphics.DrawPolygon(new Pen(Color.Black, 1.0f / totalScale), list.ToArray());
                graphics.DrawLines(new Pen(Color.Black, 1.0f / totalScale), new[]
                {
                    new PointF(0.0f,1.0f),
                    new PointF(1.0f,1.0f),
                    new PointF(1.0f,0.0f)
                });
            }

            return bitmap;
        }

        static void Main()
        {
            Generate(10000).Save(@"E:\1.png");
        }
    }
}
