using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubtitleFontReplacer
{
    internal static class Parser
    {
        private static readonly Regex FormatLineRegex = new Regex(@"^\s*Format:.*$", RegexOptions.Multiline);
        private static readonly Regex FormatLineLabelRegex = new Regex(@"^\s*Format:", RegexOptions.Multiline);
        private static readonly Regex StyleLineOrNextSectionRegex = new Regex(@"^\s*(Style:|\[).*$", RegexOptions.Multiline);
        private static readonly Regex StyleLineLabelRegex = new Regex(@"^\s*Style:", RegexOptions.Multiline);
        private static readonly Regex FieldRegex = new Regex(@"[^\s,$][^,$]*(,|$)", RegexOptions.Multiline);

        public static KeyValuePair<string, int>[] Parse(string content)
        {
            Match formatLineMatch = FormatLineRegex.Match(content);

            if (!formatLineMatch.Success)
            {
                return new KeyValuePair<string, int>[0];
            }

            Match formatLineLabelMatch = FormatLineLabelRegex.Match(formatLineMatch.Value);
            var format = formatLineMatch.Value.Substring(formatLineLabelMatch.Length).Split(',').Select(s => s.Trim()).ToArray();
            int fontNameIndex = Array.IndexOf(format, "Fontname");

            var fontNames = new List<KeyValuePair<string, int>>();
            Match styleMatch = StyleLineOrNextSectionRegex.Match(content, formatLineMatch.Index + formatLineMatch.Length);

            while (styleMatch.Success)
            {
                Match styleLineLabelMatch = StyleLineLabelRegex.Match(styleMatch.Value);

                if (styleLineLabelMatch.Success)
                {
                    Match fieldMatch = FieldRegex.Match(styleMatch.Value, styleLineLabelMatch.Length);
                    int offset = 0;

                    for (int i = 0; i < fontNameIndex; i++)
                    {
                        fieldMatch = fieldMatch.NextMatch();
                    }

                    string fontName = fieldMatch.Value;

                    if (fontName[fontName.Length - 1] == ',')
                    {
                        fontName = fontName.Substring(0, fontName.Length - 1);
                    }

                    if (fontName.StartsWith("@"))
                    {
                        fontName = fontName.Substring(1);
                        offset = 1;
                    }

                    fontNames.Add(new KeyValuePair<string, int>(fontName, styleMatch.Index + fieldMatch.Index + offset));
                }
                else
                {
                    break;
                }

                styleMatch = styleMatch.NextMatch();
            }

            return fontNames.ToArray();
        }
    }
}
