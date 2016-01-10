using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectWriteWrapper;

namespace GenerateFontList
{
    internal static class Utility
    {
        private static readonly string[] PrederedLanguages = { "en-us" };
        private static readonly string[] Headers = { "Stretch", "Weight", "Style", "Name", "Sample" };

        public static string GetPreferedString(this LocalizedStrings languageSpecificStringDictionary)
        {
            string result = null;

            return PrederedLanguages.Any(k => languageSpecificStringDictionary.TryGetValue(k, out result)) ? result : languageSpecificStringDictionary.Values.First();
        }

        public static string EscapseToLaTeX(this string text)
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

        public static string GetDisplayString(this FontStretch fontStretch)
        {
            return $"{(int)fontStretch} ({fontStretch})";
        }

        public static string GetDisplayString(this FontWeight fontWeight)
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

        public static void ToLaTeX(this IEnumerable<Font> fonts, StringBuilder stringBuilder, int indent, string sampleText)
        {
            stringBuilder.AppendLineWithIndentFormat(indent, @"\begin{{tabular}}{{|{0}|}}", string.Join("|", Headers.Select(s => "l")));
            stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");
            stringBuilder.AppendLineWithIndentFormat(indent + 1, @"{0} \\", string.Join(" & ", Headers));
            stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");

            var stretches = from t in fonts
                            orderby t.Weight, t.Stretch
                            group t by t.Stretch;

            foreach (var stretch in stretches)
            {
                stretch.ToLaTeX(stringBuilder, indent + 1, sampleText);
                stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");
            }
            stringBuilder.AppendLineWithIndent(indent, @"\end{tabular}");
        }

        private static void ToLaTeX(this IGrouping<FontStretch, Font> stretch, StringBuilder stringBuilder, int indent, string sampleText)
        {
            stringBuilder.AppendWithIndentFormat(indent, @"\multirow{{{0}}}{{*}}{{{1}}} & ", stretch.Count(), stretch.Key.GetDisplayString());

            var weights = stretch.GroupBy(t => t.Weight).ToArray();

            weights.First().ToLaTeX(stringBuilder, indent, sampleText);

            foreach (var weight in weights.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{2-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& ");
                weight.ToLaTeX(stringBuilder, indent, sampleText);
            }
        }

        private static void ToLaTeX(this IGrouping<FontWeight, Font> weight, StringBuilder stringBuilder, int indent, string sampleText)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", weight.Count(), weight.Key.GetDisplayString());

            var styles = weight.GroupBy(t => t.Style).ToArray();

            styles.First().ToLaTeX(stringBuilder, indent, sampleText);

            foreach (var style in styles.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{3-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& & ");
                style.ToLaTeX(stringBuilder, indent, sampleText);
            }
        }

        private static void ToLaTeX(this IGrouping<FontStyle, Font> style, StringBuilder stringBuilder, int indent, string sampleText)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", style.Count(), style.Key.ToString());

            var fonts = style.GroupBy(t => t).ToArray();

            fonts.First().ToLaTeX(stringBuilder, sampleText);

            foreach (var font in fonts.Skip(1))
            {
                stringBuilder.AppendLineWithIndentFormat(indent, @"\cline{{4-{0}}}", Headers.Length);
                stringBuilder.AppendWithIndent(indent, "& & & ");
                font.ToLaTeX(stringBuilder, sampleText);
            }
        }

        private static void ToLaTeX(this IGrouping<Font, Font> font, StringBuilder stringBuilder, string sampleText)
        {
            stringBuilder.AppendLineWithIndentFormat(0,
                                                     @"{0} & \sample{{{1}}}{{{2}}} \\",
                                                     font.Key.FaceNames.GetPreferedString(),
                                                     font.Key.GetInformationalStrings(InformationalStringId.PostScriptName).GetPreferedString(),
                                                     sampleText.EscapseToLaTeX());
        }
    }
}
