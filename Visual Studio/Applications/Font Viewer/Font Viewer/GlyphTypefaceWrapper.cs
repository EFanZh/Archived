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
