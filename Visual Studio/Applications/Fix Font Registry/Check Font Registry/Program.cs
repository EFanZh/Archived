using System;
using FontRegistryTools.Shared;
using Microsoft.Win32;

namespace CheckFontRegistry
{
    internal static class Program
    {
        private static void Main()
        {
            var key = Utility.GetFontRegistryKey(RegistryKeyPermissionCheck.Default);

            foreach (var font in Utility.GetFontList(key))
            {
                var result = Utility.Process(font.Key, font.Value);

                switch (result.Item1)
                {
                    case ProcessResult.Fixed:
                        Console.Write("[");
                        Utility.WriteColoredText(ConsoleColor.Green, "Can Fix");
                        Console.Write("]\nOld: \"");
                        Utility.WriteColoredText(ConsoleColor.DarkYellow, font.Key);
                        Console.Write("\" = \"{0}\"\nNew: \"", font.Value);
                        Utility.WriteColoredText(ConsoleColor.DarkCyan, result.Item2);
                        Console.WriteLine("\" = \"{0}\"\n", font.Value);
                        break;

                    case ProcessResult.Unfixed:
                        Console.Write("[");
                        Utility.WriteColoredText(ConsoleColor.Red, "Cannot Fix");
                        Console.Write("]\nOld: \"");
                        Utility.WriteColoredText(ConsoleColor.DarkYellow, font.Key);
                        Console.WriteLine("\" = \"{0}\"\n", font.Value);
                        break;
                }
            }
        }
    }
}
