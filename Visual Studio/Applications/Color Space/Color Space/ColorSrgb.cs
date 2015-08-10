namespace ColorSpace
{
    internal class ColorSrgb
    {
        public double R
        {
            get;
            set;
        }

        public double G
        {
            get;
            set;
        }

        public double B
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"({R}, {B}, {B})";
        }
    }
}
