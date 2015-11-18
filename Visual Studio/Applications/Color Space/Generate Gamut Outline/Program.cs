using ColorSpace.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace GenerateGamutOutline
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var points = new List<PointF>();

            SpectrumData.ForEach((x, y, z) =>
            {
                decimal total = x + y + z;

                points.Add(new PointF((float)(x / total), (float)(y / total)));

                return true;
            });

            const int size = 10000;

            using (Bitmap bitmap = new Bitmap(size, size))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.TranslateTransform(0.0f, size);
                graphics.ScaleTransform(1.0f, -1.0f);

                graphics.DrawPolygon(new Pen(Brushes.Black, 10.0f), points.Select(p => new PointF(p.X * size, p.Y * size)).ToArray());

                bitmap.Save(@"E:\k.png");
            }
        }
    }
}
