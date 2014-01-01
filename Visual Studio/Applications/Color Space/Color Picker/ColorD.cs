namespace ColorPicker
{
    internal struct ColorD
    {
        public ColorD(double c1, double c2, double c3)
            : this()
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }

        public double C1
        {
            get;
            set;
        }

        public double C2
        {
            get;
            set;
        }

        public double C3
        {
            get;
            set;
        }
    }
}
