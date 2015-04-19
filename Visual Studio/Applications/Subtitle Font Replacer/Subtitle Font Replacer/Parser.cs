using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubtitleFontReplacer
{
    internal static class Parser
    {
        private const RegexOptions CommonRegexOptions = RegexOptions.Multiline | RegexOptions.Compiled;
        private static readonly Regex StyleSectionLineRegex = new Regex(@"^\s*\[V4\+? Styles\]\s*\r?$", CommonRegexOptions);
        private static readonly Regex EventsSectionLineRegex = new Regex(@"^\s*\[Events\]\s*\r?$", CommonRegexOptions);
        private static readonly Regex FormatLineRegex = new Regex(@"^\s*Format:.*\r?$", CommonRegexOptions);
        private static readonly Regex FormatLineLabelRegex = new Regex(@"^\s*Format:", CommonRegexOptions);
        private static readonly Regex StyleLineOrNextSectionRegex = new Regex(@"^\s*(Style:|\[).*\r?$", CommonRegexOptions);
        private static readonly Regex StyleLineLabelRegex = new Regex(@"^\s*Style:", CommonRegexOptions);
        private static readonly Regex FieldRegex = new Regex(@"(\{[^\{\}]*\}|[^,$\{\}])*(,|$)", CommonRegexOptions);
        private static readonly Regex DialogueLineOrNextSectionRegex = new Regex(@"^\s*(Dialogue:|\[).*\r?$", CommonRegexOptions);
        private static readonly Regex DialogueLineLabelRegex = new Regex(@"^\s*Dialogue:", CommonRegexOptions);
        private static readonly Regex ControlCodeRegex = new Regex(@"\{[^\{\}]*\}", CommonRegexOptions);
        private static readonly Regex FontNameOverridePrefixRegex = new Regex(@"\\fn", CommonRegexOptions);
        private static readonly Regex FontNameRegex = new Regex(@"[^\s@]([^\{\}\\]*[^\s\{\}\\])?", CommonRegexOptions);

        public static KeyValuePair<string, int>[] Parse(string content)
        {
            var fontNames = new List<KeyValuePair<string, int>>();

            // Find style section.
            Match styleSectionLineMatch = StyleSectionLineRegex.Match(content);

            if (!styleSectionLineMatch.Success)
            {
                goto End;
            }

            Match formatLineMatch = FormatLineRegex.Match(content, styleSectionLineMatch.Index + styleSectionLineMatch.Length);

            if (!formatLineMatch.Success)
            {
                goto End;
            }

            Match formatLineLabelMatch = FormatLineLabelRegex.Match(formatLineMatch.Value); // Cannot fail.
            var format = formatLineMatch.Value.Substring(formatLineLabelMatch.Length).Split(',').Select(s => s.Trim()).ToArray();
            int fontNameIndex = Array.IndexOf(format, "Fontname");

            if (fontNameIndex == -1)
            {
                goto End;
            }

            Match styleMatch = StyleLineOrNextSectionRegex.Match(content, formatLineMatch.Index + formatLineMatch.Length);

            while (styleMatch.Success)
            {
                Match styleLineLabelMatch = StyleLineLabelRegex.Match(styleMatch.Value);

                if (styleLineLabelMatch.Success)
                {
                    Match fieldMatch = FieldRegex.Match(styleMatch.Value, styleLineLabelMatch.Length);

                    for (int i = 0; i < fontNameIndex; i++)
                    {
                        fieldMatch = fieldMatch.NextMatch();
                    }

                    string fontName = fieldMatch.Value;

                    if (fontName[fontName.Length - 1] == ',')
                    {
                        fontName = fontName.Substring(0, fontName.Length - 1);
                    }

                    int offset = 0;

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

            if (!styleMatch.Success)
            {
                goto End;
            }

            // Events section.
            Match eventsSectionLineMatch = EventsSectionLineRegex.Match(content, styleMatch.Index);

            if (!eventsSectionLineMatch.Success)
            {
                goto End;
            }

            formatLineMatch = FormatLineRegex.Match(content, eventsSectionLineMatch.Index + eventsSectionLineMatch.Length);
            formatLineLabelMatch = FormatLineLabelRegex.Match(formatLineMatch.Value);
            format = formatLineMatch.Value.Substring(formatLineLabelMatch.Length).Split(',').Select(s => s.Trim()).ToArray();

            int textIndex = Array.IndexOf(format, "Text");

            if (textIndex == -1)
            {
                goto End;
            }

            Match dialogueMatch = DialogueLineOrNextSectionRegex.Match(content, formatLineMatch.Index + formatLineMatch.Length);

            while (dialogueMatch.Success)
            {
                Match dialogueLineLabelMatch = DialogueLineLabelRegex.Match(dialogueMatch.Value);

                if (dialogueLineLabelMatch.Success)
                {
                    Match fieldMatch = FieldRegex.Match(dialogueMatch.Value, dialogueLineLabelMatch.Length);

                    for (int i = 0; i < textIndex; i++)
                    {
                        fieldMatch = fieldMatch.NextMatch();
                    }

                    for (Match current = ControlCodeRegex.Match(fieldMatch.Value); current.Success; current = current.NextMatch())
                    {
                        for (Match prefix = FontNameOverridePrefixRegex.Match(current.Value); prefix.Success; prefix = prefix.NextMatch())
                        {
                            Match fontNameMatch = FontNameRegex.Match(current.Value, prefix.Index + prefix.Length);

                            fontNames.Add(new KeyValuePair<string, int>(fontNameMatch.Value, dialogueMatch.Index + fieldMatch.Index + current.Index + fontNameMatch.Index));
                        }
                    }
                }
                else
                {
                    break;
                }

                dialogueMatch = dialogueMatch.NextMatch();
            }

        End:
            return fontNames.ToArray();
        }
    }
}
