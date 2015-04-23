using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace FontViewer
{
    internal class GlyphTypefaceWrapper
    {
        private readonly GlyphTypeface glyphTypeface;

        public GlyphTypefaceWrapper(GlyphTypeface glyphTypeface)
        {
            this.glyphTypeface = glyphTypeface;
        }

        public GlyphTypeface GlyphTypeface
        {
            get
            {
                return glyphTypeface;
            }
        }
    }
}
