using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace FontViewer
{
    internal class FontFamilyWrapper
    {
        private readonly FontFamily fontFamily;

        public FontFamilyWrapper(FontFamily fontFamily)
        {
            this.fontFamily = fontFamily;

            FamilyNames = fontFamily.FamilyNames.ToArray();
            Typefaces = fontFamily.GetTypefaces().ToArray();
        }

        public FontFamily FontFamily
        {
            get
            {
                return fontFamily;
            }
        }

        public IList<KeyValuePair<XmlLanguage, string>> FamilyNames
        {
            get;
            private set;
        }

        public IList<Typeface> Typefaces
        {
            get;
            private set;
        }
    }
}
