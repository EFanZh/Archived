using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubtitleFontReplacer
{
    internal static class Parser
    {
        private const RegexOptions CommonRegexOptions = RegexOptions.Multiline | RegexOptions.Compiled;

        private static readonly Regex StyleFontNamePrefixRegex = new Regex(@"^[^,]*,\s*@?", CommonRegexOptions);
        private static readonly Regex FontNameRegex = new Regex(@"([^,\\]*[^,\s\\])?", CommonRegexOptions);
        private static readonly Regex SectionTitleRegex = new Regex(@"^\s*\[[\[\]]*\]\s*$", CommonRegexOptions);
        private static readonly Regex EventsSectionLineRegex = new Regex(@"^\s*\[\s*EVENTS\s*\]\s*$", CommonRegexOptions);
        private static readonly Regex DialogueTextPrefixRegex = new Regex(@"^([^,]*,){9}\s*", CommonRegexOptions);
        private static readonly Regex ControlCodeRegex = new Regex(@"\{[^\{\}]*\}", CommonRegexOptions);
        private static readonly Regex FontNameOverridePrefixRegex = new Regex(@"\\fn\s*@?", CommonRegexOptions);
        private static readonly Regex LabelRegex = new Regex(@"^[^:]*:", CommonRegexOptions);
        private static readonly Regex LineRegex = new Regex(@"^.*$", CommonRegexOptions);

        private static KeyValuePair<string, int> ExtractFontNameFromStyle(string line)
        {
            Match styleFontNamePrefixMatch = StyleFontNamePrefixRegex.Match(line);
            Match fontNameMatch = FontNameRegex.Match(line, styleFontNamePrefixMatch.Index + styleFontNamePrefixMatch.Length);

            return new KeyValuePair<string, int>(fontNameMatch.Value, fontNameMatch.Index);
        }

        private static IEnumerable<KeyValuePair<string, int>> ExtractFontNamesFromControlCodes(string controlCodes)
        {
            for (Match prefix = FontNameOverridePrefixRegex.Match(controlCodes); prefix.Success; prefix = prefix.NextMatch())
            {
                Match fontNameMatch = FontNameRegex.Match(controlCodes, prefix.Index + prefix.Length);

                yield return new KeyValuePair<string, int>(fontNameMatch.Value, fontNameMatch.Index);
            }
        }

        private static IEnumerable<KeyValuePair<string, int>> ExtractFontNamesFromDialogue(string line)
        {
            Match textPrefixMatch = DialogueTextPrefixRegex.Match(line);

            for (Match controlCode = ControlCodeRegex.Match(line, textPrefixMatch.Index + textPrefixMatch.Length); controlCode.Success; controlCode = controlCode.NextMatch())
            {
                foreach (var fontName in ExtractFontNamesFromControlCodes(line.Substring(controlCode.Index + 1, controlCode.Length - 2)))
                {
                    yield return new KeyValuePair<string, int>(fontName.Key, controlCode.Index + fontName.Value + 1);
                }
            }
        }

        public static KeyValuePair<string, int>[] Parse(string content)
        {
            var result = new List<KeyValuePair<string, int>>();
            bool hadEventsSection = false;

            for (Match lineMatch = LineRegex.Match(content); lineMatch.Success; lineMatch = lineMatch.NextMatch())
            {
                string line = lineMatch.Value;

                if (SectionTitleRegex.IsMatch(line))
                {
                    if (hadEventsSection)
                    {
                        break;
                    }

                    if (EventsSectionLineRegex.IsMatch(line.ToUpper()))
                    {
                        hadEventsSection = true;
                    }
                }
                else
                {
                    Match labelMatch = LabelRegex.Match(line);

                    switch (labelMatch.Value.Trim().ToUpper())
                    {
                        case "STYLE:":
                            {
                                var k = ExtractFontNameFromStyle(line);

                                result.Add(new KeyValuePair<string, int>(k.Key, lineMatch.Index + k.Value));
                                break;
                            }

                        case "DIALOGUE:":
                            result.AddRange(ExtractFontNamesFromDialogue(line).Select(p => new KeyValuePair<string, int>(p.Key, lineMatch.Index + p.Value)));
                            break;
                    }
                }
            }

            return result.ToArray();
        }
    }
}
