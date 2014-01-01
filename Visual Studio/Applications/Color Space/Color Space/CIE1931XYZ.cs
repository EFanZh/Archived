using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ColorSpace
{
    internal class CIE1931XYZ
    {
        private const string cmfFile = "lin2012xyz2e_fine_7sf.csv";

        public static IEnumerable<double[]> GetColorMatchFunction()
        {
            var dict = new Dictionary<double, double[]>();

            foreach (var line in File.ReadAllLines(cmfFile))
            {
                var items = line.Split(',').Select(double.Parse);
                yield return items.ToArray();
            }
        }
    }
}
