using System;
using System.Linq;

namespace EnumerateString
{
    internal class Program
    {
        private static string data = "abcdefghijklmnopqrstuvwxyz";

        private static string Next(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return data[0].ToString();
            }
            else
            {
                string left = str.Substring(0, str.Length - 1);
                if (str.Last() == data.Last())
                {
                    return Next(left) + data[0];
                }
                else
                {
                    return left + data[data.IndexOf(str[str.Length - 1]) + 1];
                }
            }
        }

        private static void Main(string[] args)
        {
            string s = string.Empty;
            while (true)
            {
                Console.WriteLine(s);
                s = Next(s);
            }
        }
    }
}
