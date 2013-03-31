using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = new List<double>();
            for (int i = 0; i < 1000; i++)
            {
                lines.Add(i);
            }
            double v = Math.PI / 2;
            Bitmap b = new Bitmap(10000, 10000);
            Graphics g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.HighQuality;
            bool flag = true;
            double last = b.Height;
            foreach (var line in lines)
            {
                double vv = Math.Atan(line / 10) - v / 2;
                double now = (1 - vv / v) * b.Height;
                if (flag)
                {
                    g.FillRectangle(new SolidBrush(Color.Black), 0, (float)now, b.Width, (float)(last - now));
                }
                flag = !flag;
                last = now;
            }
            b.Save("E:\\1.png");
        }
    }
}
