using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace FontRegistryTools.Shared
{
    public static class Utility
    {
        private const string TrueTypePostfix = "(TrueType)";
        private const string OpenTypePostfix = "(OpenType)";
        private static readonly string[] TrueTypeExtensions = { ".TTF", ".TTC" };
        private static readonly string[] OpenTypeExtensions = { ".OTF" };
        private static readonly Regex PossibleDuplicateFileRegex = new Regex(@"_\d+(\.[^\.]*)?$", RegexOptions.Compiled);

        public static IEnumerable<KeyValuePair<string, string>> GetFontList(RegistryKey key)
        {
            return key.GetValueNames().Select(name => new KeyValuePair<string, string>(name, (string)key.GetValue(name)));
        }

        public static RegistryKey GetFontRegistryKey(RegistryKeyPermissionCheck permissionCheck)
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", permissionCheck);
        }

        public static void RenameFontName(RegistryKey key, string original, string target, string value)
        {
            // TODO: Use transection.

            key.SetValue(target, value);
            key.DeleteValue(original);
        }

        public static Tuple<ProcessResult, string> Process(string name, string file)
        {
            string fontName;

            if (name.EndsWith(OpenTypePostfix, StringComparison.InvariantCultureIgnoreCase))
            {
                fontName = name.Substring(0, name.Length - OpenTypePostfix.Length);
            }
            else if (name.EndsWith(TrueTypePostfix, StringComparison.InvariantCultureIgnoreCase))
            {
                fontName = name.Substring(0, name.Length - TrueTypePostfix.Length);
            }
            else
            {
                goto Unfixed;
            }

            string postfix;

            if (OpenTypeExtensions.Any(k => file.EndsWith(k, StringComparison.InvariantCultureIgnoreCase)))
            {
                postfix = OpenTypePostfix;
            }
            else if (TrueTypeExtensions.Any(k => file.EndsWith(k, StringComparison.InvariantCultureIgnoreCase)))
            {
                postfix = TrueTypePostfix;
            }
            else
            {
                goto Unfixed;
            }

            string normalizedName = string.Format("{0} {1}", fontName.Trim(), postfix);

            return normalizedName.Equals(name, StringComparison.InvariantCulture) ?
                Tuple.Create<ProcessResult, string>(ProcessResult.Good, null) :
                Tuple.Create(ProcessResult.Fixed, normalizedName);

        Unfixed:
            return Tuple.Create<ProcessResult, string>(ProcessResult.Unfixed, null);
        }

        public static void WriteColoredText(ConsoleColor color, string text)
        {
            ConsoleColor savedColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);

            Console.ForegroundColor = savedColor;
        }

        public static bool IsPossibleDuplicateFileRegex(string file)
        {
            return PossibleDuplicateFileRegex.IsMatch(file);
        }
    }
}
