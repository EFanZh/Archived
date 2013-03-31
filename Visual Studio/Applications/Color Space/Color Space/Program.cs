using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ColorSpace
{
    class Program
    {
        static int MyRound(double x)
        {
            return (int)Math.Round(x);
        }

        static void Main()
        {
            //Dictionary<int, double[]> data = CIE1931XYZ.GetColorMatchFunction();
            //int r = 0, g = 0, b = 0;
            //double rm = 0.0, gm = 0.0, bm = 0.0;
            //foreach (KeyValuePair<int, double[]> kvp in data)
            //{
            //    double[] v = kvp.Value;
            //    double sum = v[0] + v[1] + v[2];
            //    double t = v[0] / sum;
            //    if (t > rm)
            //    {
            //        rm = t;
            //        r = kvp.Key;
            //    }
            //    t = v[1] / sum;
            //    if (t > gm)
            //    {
            //        gm = t;
            //        g = kvp.Key;
            //    }
            //    t = v[2] / sum;
            //    if (t > bm)
            //    {
            //        bm = t;
            //        b = kvp.Key;
            //    }
            //}
            //Console.WriteLine(r);
            //Console.WriteLine(g);
            //Console.WriteLine(b);
            CIE1931XYZ.GeneratePoints(10000, CIE1931XYZ.GetColorMatchFunction()).Save("E:\\1.png");
        }
    }
}
