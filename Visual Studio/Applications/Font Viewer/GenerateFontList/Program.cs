using System.IO;
using System.Linq;
using System.Text;
using DirectWriteWrapper;

namespace GenerateFontList
{
    internal class Program
    {
        private static void Main()
        {
            var factory = new Factory(FactoryType.Shared);

            var fontFamilies = (from f in factory.GetSystemFontCollection(true)
                                where !f.GetFirstMatchingFont(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal).IsSymbolFont
                                orderby f.FamilyNames.GetPreferedString()
                                select f).ToArray();

            var stringBuilder = new StringBuilder();

            fontFamilies.First().ToLaTeX(stringBuilder, 0);

            foreach (var fontFamily in fontFamilies.Skip(1))
            {
                stringBuilder.AppendLine();
                fontFamily.ToLaTeX(stringBuilder, 0);
            }

            File.WriteAllText("E:\\1.txt", stringBuilder.ToString(), Encoding.UTF8);
        }
    }
}
