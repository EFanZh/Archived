using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProbabilityDistribution
{
    internal class Program
    {
        private static Random rand = new Random();

        private static void GetResult(Func<double> func, double interval, int n, string file)
        {
            var dict = new SortedDictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                double v = func();
                int k = (int)(v / interval);
                if (dict.ContainsKey(k))
                {
                    dict[k]++;
                }
                else
                {
                    dict[k] = 1;
                }
            }

            StreamWriter sw = new StreamWriter(new FileStream(file, FileMode.Create), new UTF8Encoding(false));
            foreach (var kvp in dict)
            {
                sw.WriteLine("{0}\t{1}", kvp.Key, kvp.Value);
            }
            sw.Close();
        }

        private static double R
        {
            get
            {
                return rand.NextDouble();
            }
        }

        private static void Main(string[] args)
        {
            GetResult(() => R + R + R, 0.01, 1 << 24, "D:\\1.txt");
        }
    }
}
