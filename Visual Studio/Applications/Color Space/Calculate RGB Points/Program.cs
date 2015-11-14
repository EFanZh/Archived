using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CalculateRgbPoints
{
    internal class Program
    {
        private static decimal ParseDecimal(string input)
        {
            return decimal.Parse(input, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);
        }

        private static IEnumerable<Tuple<decimal, decimal>> ReadColors(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream))
            {
                string line = streamReader.ReadLine();

                while (line != null)
                {
                    var columns = line.Split(',');

                    decimal red = ParseDecimal(columns[1]);
                    decimal green = ParseDecimal(columns[2]);
                    decimal blue = ParseDecimal(columns[3]);
                    decimal total = red + green + blue;

                    yield return Tuple.Create(red / total, green / total);

                    line = streamReader.ReadLine();
                }
            }
        }

        private static Tuple<Tuple<decimal, decimal>, Tuple<decimal, decimal>, Tuple<decimal, decimal>> ProcessFile(string path)
        {
            decimal redX = 0.0m;
            decimal redY = 0.0m;
            decimal greenX = 0.0m;
            decimal greenY = 0.0m;
            decimal blueX = 0.0m;
            decimal blueY = 0.0m;
            decimal blueZ = 0.0m;

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                foreach (var current in ReadColors(fileStream))
                {
                    decimal x = current.Item1;
                    decimal y = current.Item2;
                    decimal z = 1.0m - (x + y);

                    if (x > redX)
                    {
                        redX = x;
                        redY = y;
                    }

                    if (y > greenY)
                    {
                        greenX = x;
                        greenY = y;
                    }

                    if (z > blueZ)
                    {
                        blueX = x;
                        blueY = y;
                        blueZ = z;
                    }
                }
            }

            var redResult = Tuple.Create(redX, redY);
            var greenResult = Tuple.Create(greenX, greenY);
            var blueResult = Tuple.Create(blueX, blueY);

            return Tuple.Create(redResult, greenResult, blueResult);
        }

        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }

            var result = ProcessFile(args[0]);

            Console.WriteLine($"  Red: {result.Item1.Item1:0.0000000000000000000000000000}, {result.Item1.Item2:0.0000000000000000000000000000}");
            Console.WriteLine($"Green: {result.Item2.Item1:0.0000000000000000000000000000}, {result.Item2.Item2:0.0000000000000000000000000000}");
            Console.WriteLine($" Blue: {result.Item3.Item1:0.0000000000000000000000000000}, {result.Item3.Item2:0.0000000000000000000000000000}");
        }
    }
}
