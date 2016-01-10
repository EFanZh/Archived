using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DirectWriteWrapper;

namespace GenerateFontList
{
    internal class Program
    {
        private static readonly KeyValuePair<string, string>[] Languages =
        {
            new KeyValuePair<string, string>("ASCII", @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~"),
            new KeyValuePair<string, string>("English", "I can eat glass and it doesn’t hurt me."),
            new KeyValuePair<string, string>("Chinese Simplified", "我能吞下玻璃而不伤身体。"),
            new KeyValuePair<string, string>("Chinese Traditional", "我能吞下玻璃而不傷身體。"),
            new KeyValuePair<string, string>("Japanese", "私はガラスを食べられます。それは私を傷つけません。")
        };

        private static bool CanShowSample(Font font, string sample)
        {
            return sample.All(c => font.HasCharacter(c));
        }

        private static string GenerateLanguageFonts(KeyValuePair<string, string> language, IEnumerable<Font> fonts)
        {
            var stringBuilder = new StringBuilder($@"\section{{{language.Key}}}");

            stringBuilder.AppendLine();

            foreach (var fontFamily in fonts.GroupBy(f => f.FontFamily.FamilyNames.GetPreferedString()))
            {
                stringBuilder.AppendLine($@"\subsection{{{fontFamily.Key.EscapseToLaTeX()}}}");

                fontFamily.ToLaTeX(stringBuilder, 0, language.Value);
            }

            return stringBuilder.ToString();
        }

        private static void Main()
        {
            var factory = new Factory(FactoryType.Shared);

            var fonts = (from f in factory.GetSystemFontCollection(true)
                             //where !f.GetFirstMatchingFont(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal).IsSymbolFont
                         orderby f.FamilyNames.GetPreferedString()
                         select f).SelectMany(f => f.GetMatchingFonts(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal)).ToArray();

            foreach (var language in Languages)
            {
                var languageFonts = fonts.Where(f => CanShowSample(f, language.Value));
                var result = GenerateLanguageFonts(language, languageFonts);

                File.WriteAllText($@"E:\\{language.Key.Replace(' ', '-').ToLowerInvariant()}.tex", result, Encoding.UTF8);
            }
        }
    }
}
