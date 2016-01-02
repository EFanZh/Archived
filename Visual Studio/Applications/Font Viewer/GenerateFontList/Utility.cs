using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectWriteWrapper;

namespace GenerateFontList
{
    internal static class Utility
    {
        private static readonly string[] PrederedLanguages = { "en-us" };
        private static readonly string[] Headers = { "Stretch", "Weight", "Style", "Name", "Language", "Sample" };

        private static readonly KeyValuePair<string, string>[] SampleTexts =
        {
             new KeyValuePair<string, string>("ASCII", @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~"),
             new KeyValuePair<string, string>("Eng", "I can eat glass and it doesn’t hurt me."),
             new KeyValuePair<string, string>("Chs", "我能吞下玻璃而不伤身体。"),
             new KeyValuePair<string, string>("Cht", "我能吞下玻璃而不傷身體。"),
             new KeyValuePair<string, string>("Jap", "私はガラスを食べられます。それは私を傷つけません。")
        };

        public static string GetPreferedString(this LocalizedStrings languageSpecificStringDictionary)
        {
            string result = null;

            return PrederedLanguages.Any(k => languageSpecificStringDictionary.TryGetValue(k, out result)) ? result : languageSpecificStringDictionary.Values.First();
        }

        private static string EscapseToLaTeX(this string text)
        {
            var sb = new StringBuilder();

            foreach (var c in text)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append(@"\textbackslash{}");
                        break;

                    case '~':
                        sb.Append(@"\textasciitilde{}");
                        break;

                    case '^':
                        sb.Append(@"\textasciicircum{}");
                        break;

                    case '\n':
                        sb.Append(@" \\ ");
                        break;

                    case '[':
                    case ']':
                        sb.Append($@"\char""{(int)c:X}{{}}");
                        break;

                    case '$':
                    case '%':
                    case '_':
                    case '#':
                    case '}':
                    case '{':
                    case '&':
                        sb.Append('\\');
                        sb.Append(c);
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }

        private static IEnumerable<Tuple<Font, KeyValuePair<string, string>>> GetFonts(this FontFamily fontFamily)
        {
            var fonts = fontFamily.GetMatchingFonts(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal);

            return (from font in fonts
                    from sampleText in SampleTexts
                    where sampleText.Value.All(c => font.HasCharacter(c))
                    select Tuple.Create(font, sampleText)).ToArray();
        }

        private static string GetDisplayString(this FontStretch fontStretch)
        {
            return $"{(int)fontStretch} ({fontStretch})";
        }

        private static string GetDisplayString(this FontWeight fontWeight)
        {
            return (int)fontWeight % 100 == 0 ? $"{(int)fontWeight} ({fontWeight})" : ((int)fontWeight).ToString();
        }

        private static void AppendIndent(this StringBuilder sb, int indent)
        {
            sb.Append(' ', 4 * indent);
        }

        private static void AppendWithIndent(this StringBuilder sb, int indent, string text)
        {
            sb.AppendIndent(indent);
            sb.Append(text);
        }

        private static void AppendLineWithIndent(this StringBuilder sb, int indent, string text)
        {
            sb.AppendIndent(indent);
            sb.AppendLine(text);
        }

        private static void AppendWithIndentFormat(this StringBuilder sb, int indent, string format, params object[] args)
        {
            sb.AppendIndent(indent);
            sb.AppendFormat(format, args);
        }

        private static void AppendLineWithIndentFormat(this StringBuilder sb, int indent, string format, params object[] args)
        {
            sb.AppendWithIndentFormat(indent, format, args);
            sb.AppendLine();
        }

        public static void ToLaTeX(this FontFamily fontFamily, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendLineWithIndentFormat(indent, @"\section{{{0}}}", fontFamily.FamilyNames.GetPreferedString().EscapseToLaTeX());
            stringBuilder.AppendLine();
            stringBuilder.AppendLineWithIndentFormat(indent, @"\begin{{tabular}}{{|{0}|}}", string.Join("|", Headers.Select(s => "l")));
            stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");
            stringBuilder.AppendLineWithIndentFormat(indent + 1, @"{0} \\", string.Join(" & ", Headers));
            stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");

            var stretches = from t in fontFamily.GetFonts()
                            orderby t.Item1.Weight, t.Item1.Stretch
                            group t by t.Item1.Stretch;

            foreach (var stretch in stretches)
            {
                stretch.ToLaTeX(stringBuilder, indent + 1);
                stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");
            }
            stringBuilder.AppendLineWithIndent(indent, @"\end{tabular}");
        }

        private static void ToLaTeX(this IGrouping<FontStretch, Tuple<Font, KeyValuePair<string, string>>> stretch, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(indent, @"\multirow{{{0}}}{{*}}{{{1}}} & ", stretch.Count(), stretch.Key.GetDisplayString());

            var weights = stretch.GroupBy(t => t.Item1.Weight).ToArray();

            weights.First().ToLaTeX(stringBuilder, indent);

            foreach (var weight in weights.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{2-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& ");
                weight.ToLaTeX(stringBuilder, indent);
            }
        }

        private static void ToLaTeX(this IGrouping<FontWeight, Tuple<Font, KeyValuePair<string, string>>> weight, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", weight.Count(), weight.Key.GetDisplayString());

            var styles = weight.GroupBy(t => t.Item1.Style).ToArray();

            styles.First().ToLaTeX(stringBuilder, indent);

            foreach (var style in styles.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{3-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& & ");
                style.ToLaTeX(stringBuilder, indent);
            }
        }

        private static void ToLaTeX(this IGrouping<FontStyle, Tuple<Font, KeyValuePair<string, string>>> style, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", style.Count(), style.Key.ToString());

            var fonts = style.GroupBy(t => t.Item1).ToArray();

            fonts.First().ToLaTeX(stringBuilder, indent);

            foreach (var font in fonts.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{4-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& & & ");
                font.ToLaTeX(stringBuilder, indent);
            }
        }

        private static void ToLaTeX(this IGrouping<Font, Tuple<Font, KeyValuePair<string, string>>> font, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", font.Count(), font.Key.FaceNames.GetPreferedString());

            font.First().ToLaTeX(stringBuilder);

            foreach (var sampleText in font.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{5-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& & & & ");
                sampleText.ToLaTeX(stringBuilder);
            }
        }

        private static void ToLaTeX(this Tuple<Font, KeyValuePair<string, string>> sampleText, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLineWithIndentFormat(0,
                @"{0} & \sample{{{1}}}{{{2}}} \\",
                sampleText.Item2.Key,
                sampleText.Item1.GetInformationalStrings(InformationalStringId.PostScriptName).GetPreferedString(),
                sampleText.Item2.Value.EscapseToLaTeX());
        }
    }
}
