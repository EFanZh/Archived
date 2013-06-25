using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextCount
{
    internal class Program
    {
        private static IOrderedEnumerable<KeyValuePair<char, int>> CountChars(string file, Predicate<char> condition)
        {
            StreamReader sr = new StreamReader(file);

            var counts = new Dictionary<char, int>();

            while (!sr.EndOfStream)
            {
                char c = (char)sr.Read();

                if (condition(c))
                {
                    if (counts.ContainsKey(c))
                    {
                        counts[c]++;
                    }
                    else
                    {
                        counts[c] = 1;
                    }
                }
            }

            sr.Close();

            return counts.OrderBy(kvp => kvp.Key).OrderBy(kvp => kvp.Value);
        }

        private static IOrderedEnumerable<KeyValuePair<string, int>> CountWords(string file, Predicate<string> condition)
        {
            StreamReader sr = new StreamReader(file);

            var counts = new Dictionary<string, int>();
            var special = new[] { '-', '_', '\'' };

            while (!sr.EndOfStream)
            {
                int c = sr.Read();

                while (c != -1 && !(char.IsLetterOrDigit((char)c) || special.Contains((char)c)))
                {
                    c = sr.Read();
                }

                if (c == -1)
                {
                    break;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    while (c != -1 && (char.IsLetterOrDigit((char)c) || special.Contains((char)c)))
                    {
                        sb.Append((char)c);
                        c = sr.Read();
                    }

                    string s = sb.ToString().ToLower();
                    if (condition(s))
                    {
                        if (counts.ContainsKey(s))
                        {
                            counts[s]++;
                        }
                        else
                        {
                            counts[s] = 1;
                        }
                    }
                }
            }

            sr.Close();

            return counts.OrderBy(kvp => kvp.Key).OrderBy(kvp => kvp.Value);
        }

        private static void Main(string[] args)
        {
            var result_1 = CountChars("D:\\1.txt", c => true);
            StreamWriter sw_1 = new StreamWriter(new FileStream("D:\\r_1.txt", FileMode.Create), new UTF8Encoding(false));

            foreach (var kvp in result_1)
            {
                sw_1.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }

            sw_1.Close();

            var result_2 = CountWords("D:\\1.txt", s => true);
            StreamWriter sw_2 = new StreamWriter(new FileStream("D:\\r_2.txt", FileMode.Create), new UTF8Encoding(false));

            foreach (var kvp in result_2)
            {
                sw_2.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }

            sw_2.Close();
        }
    }
}
