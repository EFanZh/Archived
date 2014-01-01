namespace ColorPicker
{
    internal struct ColorB
    {
        public ColorB(byte r, byte g, byte b)
            : this()
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R
        {
            get;
            set;
        }

        public byte G
        {
            get;
            set;
        }

        public byte B
        {
            get;
            set;
        }
    }
}
