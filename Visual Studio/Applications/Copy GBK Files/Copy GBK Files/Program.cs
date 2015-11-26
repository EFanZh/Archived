using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopyGbkFiles
{
    internal class Program
    {
        private static Encoding gbkEncoding = Encoding.GetEncoding("gbk", new EncoderExceptionFallback(), new DecoderExceptionFallback());
        private static Encoding utf8Encoding = new UTF8Encoding(false, true);

        private static string TrimFolder(string path)
        {
            return path.TrimEnd('/', '\\');
        }

        private static IEnumerable<string> GetFiles(string path)
        {
            foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                yield return file;
            }
        }

        private static bool IsValidString(string s)
        {
            foreach (char ch in s)
            {
                if (char.IsControl(ch) && ch != '\r' && ch != '\n')
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsUtf8(string file)
        {
            try
            {
                File.ReadAllText(file, utf8Encoding);

                return true;
            }
            catch (DecoderFallbackException)
            {
                return false;
            }
        }

        private static string GetTargetFile(string file, string from, string to)
        {
            return to + file.Substring(from.Length);
        }

        private static void Process(string file, string from, string to)
        {
            try
            {
                string text = File.ReadAllText(file, gbkEncoding);

                if (IsValidString(text) && !IsUtf8(file))
                {
                    Console.WriteLine(file);

                    string targetFile = GetTargetFile(file, from, to);

                    Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
                    File.WriteAllText(targetFile, text, utf8Encoding);
                }
            }
            catch (DecoderFallbackException)
            {
                // Ignored.
            }
        }

        private static void CopyGbkFiles(string from, string to)
        {
            foreach (string file in GetFiles(from))
            {
                Process(file, from, to);
            }
        }

        private static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                string from = Path.GetFullPath(TrimFolder(args[0]));
                string to = Path.GetFullPath(TrimFolder(args[1]));

                CopyGbkFiles(from, to);
            }
        }
    }
}
