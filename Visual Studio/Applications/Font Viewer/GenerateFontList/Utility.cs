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

            if (PrederedLanguages.Any(k => languageSpecificStringDictionary.TryGetValue(k, out result)))
            {
                return result;
            }
            else
            {
                return languageSpecificStringDictionary.Values.First();
            }
        }

        private static string EscapseToLaTeX(this string text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                switch (c)
                {
                    case '#':
                    case '$':
                    case '%':
                    case '&':
                    case '\\':
                    case '^':
                    case '_':
                    case '{':
                    case '}':
                    case '~':
                        sb.Append('\\');
                        break;
                }

                sb.Append(c);
            }

            return sb.ToString();
        }

        private static FontList GetFonts(this FontFamily fontFamily)
        {
            return fontFamily.GetMatchingFonts(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal);
        }

        private static string GetDisplayString(this FontStretch fontStretch)
        {
            return string.Format("{0} ({1})", (int)fontStretch, fontStretch);
        }

        private static string GetDisplayString(this FontWeight fontWeight)
        {
            if (((int)fontWeight) % 100 == 0)
            {
                return string.Format("{0} ({1})", (int)fontWeight, fontWeight);
            }
            else
            {
                return ((int)fontWeight).ToString();
            }
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
            stringBuilder.AppendLineWithIndent(indent, @"\begin{tabular}{|l|l|l|l|l|}");
            stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");
            stringBuilder.AppendLineWithIndentFormat(indent + 1, @"{0} \\", string.Join(" & ", Headers));
            stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");

            var stretches = from t in fontFamily.GetFonts()
                            orderby t.Weight, t.Stretch
                            group t by t.Stretch;

            foreach (var stretch in stretches)
            {
                stretch.ToLaTeX(stringBuilder, indent + 1);
                stringBuilder.AppendLineWithIndent(indent + 1, @"\hline");
            }
            stringBuilder.AppendLineWithIndent(indent, @"\end{tabular}");
        }

        private static void ToLaTeX(this IGrouping<FontStretch, Font> stretch, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(indent, @"\multirow{{{0}}}{{*}}{{{1}}} & ", stretch.Count(), stretch.Key.GetDisplayString());

            var weights = stretch.GroupBy(t => t.Weight).ToArray();

            weights.First().ToLaTeX(stringBuilder, indent);

            foreach (var weight in weights.Skip(1))
            {
                stringBuilder.AppendLineWithIndent(indent, @"\cline{2-5}");
                stringBuilder.AppendWithIndent(indent, "& ");
                weight.ToLaTeX(stringBuilder, indent);
            }
        }

        private static void ToLaTeX(this IGrouping<FontWeight, Font> weight, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", weight.Count(), weight.Key.GetDisplayString());

            var styles = weight.GroupBy(t => t.Style).ToArray();

            styles.First().ToLaTeX(stringBuilder, indent);

            foreach (var style in styles.Skip(1))
            {
                stringBuilder.AppendLineWithIndent(indent, @"\cline{3-5}");
                stringBuilder.AppendWithIndent(indent, "& & ");
                style.ToLaTeX(stringBuilder, indent);
            }
        }

        private static void ToLaTeX(this IGrouping<FontStyle, Font> style, StringBuilder stringBuilder, int indent)
        {
            stringBuilder.AppendWithIndentFormat(0, @"\multirow{{{0}}}{{*}}{{{1}}} & ", style.Count(), style.Key.ToString());

            style.First().ToLaTeX(stringBuilder);

            foreach (var typeface in style.Skip(1))
            {
                stringBuilder.AppendLineWithIndent(indent, @"\cline{4-5}");
                stringBuilder.AppendWithIndent(indent, "& & & ");
                typeface.ToLaTeX(stringBuilder);
            }
        }

        private static void ToLaTeX(this Font font, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLineWithIndentFormat(0,
                @"{0} & {{\fontspec{{{1}}} {2}}} \\",
                font.FaceNames.GetPreferedString(),
                font.GetInformationalStrings(InformationalStringId.PostScriptName).GetPreferedString(),
                new string("ABCDEFG abcdefg ff fi ti ffi function 0123".Where(c => font.HasCharacter(c)).ToArray()));
        }
    }
}
