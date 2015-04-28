using System;
using FontRegistryTools.Shared;
using Microsoft.Win32;

namespace FixFontRegistry
{
    internal static class Program
    {
        private static void Main()
        {
            var key = Utility.GetFontRegistryKey(RegistryKeyPermissionCheck.ReadWriteSubTree);

            foreach (var font in Utility.GetFontList(key))
            {
                var result = Utility.Process(font.Key, font.Value);

                if (result.Item1 != ProcessResult.Fixed)
                {
                    continue;
                }

                Utility.RenameFontName(key, font.Key, result.Item2, font.Value);
                Console.Write("[");
                Utility.WriteColoredText(ConsoleColor.Green, "Fixed");
                Console.Write("]\nOld: \"");
                Utility.WriteColoredText(ConsoleColor.DarkYellow, font.Key);
                Console.Write("\" = \"{0}\"\nNew: \"", font.Value);
                Utility.WriteColoredText(ConsoleColor.DarkCyan, result.Item2);
                Console.WriteLine("\" = \"{0}\"\n", font.Value);
            }
        }
    }
}
