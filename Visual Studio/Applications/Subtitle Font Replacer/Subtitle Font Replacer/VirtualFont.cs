namespace SubtitleFontReplacer
{
    public class VirtualFont
    {
        public VirtualFont(string name, string horizontalFont, string verticalFont)
        {
            Name = name;
            HorizontalFont = horizontalFont;
            VerticalFont = verticalFont;
        }

        public string Name
        {
            get;
            set;
        }

        public string HorizontalFont
        {
            get;
            set;
        }

        public string VerticalFont
        {
            get;
            set;
        }
    }
}
