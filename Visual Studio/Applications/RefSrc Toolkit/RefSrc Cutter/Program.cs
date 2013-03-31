using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RefSrcCutter
{
    internal class Program
    {
        private static void Cut(string input, string output)
        {
            Regex regex = new Regex("^[0-9]+,");
            using (StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (regex.IsMatch(line))
                    {
                        string[] lineSplit = line.Split(',');
                        int length = int.Parse(lineSplit[3]);
                        char[] buffer = new char[length];
                        sr.Read(buffer, 0, length);
                        string path = output + lineSplit[1];
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.Write(buffer);
                            sw.Close();
                        }
                        lineSplit = null;
                        buffer = null;
                    }
                    line = null;
                    GC.Collect();
                }
                sr.Close();
            }
        }

        private static void Main(string[] args)
        {
            // Cut(@"E:\source_3.5.src", @"E:\Net\3.5.50727.3053");
            // Cut(@"E:\source_4.src", @"E:\.Net\4.0");
            Cut(@"D:\EFanZh\Temp\source\source", @"D:\n");
        }
    }
}
