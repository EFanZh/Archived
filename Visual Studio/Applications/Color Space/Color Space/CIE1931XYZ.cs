using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System;

namespace ColorSpace
{
    class CIE1931XYZ
    {
        private const string cmfFile = "ciexyz31_1.csv";

        public static Dictionary<int, double[]> GetColorMatchFunction()
        {
            Dictionary<int, double[]> dict = new Dictionary<int, double[]>();
            string[] lines = File.ReadAllLines(cmfFile);
            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                dict.Add(int.Parse(items[0]), new double[] { double.Parse(items[1]), double.Parse(items[2]), double.Parse(items[3]) });
            }
            return dict;
        }

        public static Bitmap GeneratePoints(int length, Dictionary<int, double[]> data)
        {
            Bitmap bmp = new Bitmap(length, length, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            SolidBrush b = new SolidBrush(Color.Black);
            {
                //double[] v = data[829];
                //double t = v[0] + v[1] + v[2];
                //float x = (float)(length * v[0] / t);
                //float y = (float)(length - length * v[1] / t);
                //g.FillEllipse(new SolidBrush(Color.FromArgb(255,0,0)), x - 8.0f, y - 8.0f, 16.0f, 16.0f);

                //v = data[521];
                //t = v[0] + v[1] + v[2];
                //x = (float)(length * v[0] / t);
                //y = (float)(length - length * v[1] / t);

                //g.FillEllipse(new SolidBrush(Color.FromArgb(0, 255, 0)), x - 8.0f, y - 8.0f, 16.0f, 16.0f);
                //v = data[458];
                //t = v[0] + v[1] + v[2];
                //x = (float)(length * v[0] / t);
                //y = (float)(length - length * v[1] / t);
                //g.FillEllipse(new SolidBrush(Color.FromArgb(0, 0, 255)), x - 8.0f, y - 8.0f, 16.0f, 16.0f);
            }
            float x00 = 0.0f;
            float y00 = 0.0f;
            float x0 = 0.0f;
            float y0 = 0.0f;
            foreach (KeyValuePair<int, double[]> kvp in data)
            {
                double[] v = kvp.Value;
                double t = v[0] + v[1] + v[2];
                float x = (float)(length * v[0] / t);
                float y = (float)(length - length * v[1] / t);
                if (x0 == 0.0f)
                {
                    x0 = x;
                    y0 = y;
                    x00 = x;
                    y00 = y;
                }
                else
                {
                    g.DrawLine(new Pen(Color.Black), x0, y0, x, y);
                    Console.Write("{0},{1} ", length * v[0] / t, length - length * v[1] / t);
                    x0 = x;
                    y0 = y;
                }
            }
            g.DrawLine(new Pen(Color.Black), x00, y00, x0, y0);
            return bmp;
        }
    }
}
