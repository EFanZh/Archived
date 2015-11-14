using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ColorSpace.Common
{
    internal static class SpectrumData
    {
        private static string dataFile = "lin2012xyz2e_fine_7sf.csv";

        private static decimal ParseDecimal(string input)
        {
            return decimal.Parse(input, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);
        }

        public static void ForEach(Func<decimal, decimal, decimal, bool> func)
        {
            string basePath = Assembly.GetExecutingAssembly().Location;
            string baseFolder = Path.GetDirectoryName(basePath);
            string dataFilePath = Path.Combine(baseFolder, dataFile);

            using (StreamReader streamReader = new StreamReader(dataFilePath))
            {
                string line = streamReader.ReadLine();

                while (line != null)
                {
                    var columns = line.Split(',');

                    decimal x = ParseDecimal(columns[1]);
                    decimal y = ParseDecimal(columns[2]);
                    decimal z = ParseDecimal(columns[3]);

                    if (!func(x, y, z))
                    {
                        return;
                    }

                    line = streamReader.ReadLine();
                }
            }
        }
    }
}
