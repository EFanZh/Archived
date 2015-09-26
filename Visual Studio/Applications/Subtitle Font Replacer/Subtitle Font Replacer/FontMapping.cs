namespace SubtitleFontReplacer
{
    public class FontMapping
    {
        public FontMapping(string original, string target, string verticalTarget)
        {
            Original = original;
            Target = target;
            VerticalTarget = verticalTarget;
        }

        public string Original
        {
            get;
            set;
        }

        public string Target
        {
            get;
            set;
        }

        public string VerticalTarget
        {
            get;
            set;
        }
    }
}
