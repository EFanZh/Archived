using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace FontViewer
{
    internal class FontFamilyWrapper
    {
        private readonly FontFamily fontFamily;

        public FontFamilyWrapper(FontFamily fontFamily)
        {
            this.fontFamily = fontFamily;
        }

        public FontFamily FontFamily
        {
            get
            {
                return fontFamily;
            }
        }

        public ICollection<Typeface> Typefaces
        {
            get
            {
                return fontFamily.GetTypefaces();
            }
        }
    }
}
