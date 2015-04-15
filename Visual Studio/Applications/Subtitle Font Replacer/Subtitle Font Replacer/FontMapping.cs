namespace SubtitleFontReplacer
{
    public class FontMapping
    {
        public FontMapping(string original, string target)
        {
            Original = original;
            Target = target;
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
    }
}
