namespace ColorSpace
{
    internal class ColorXyy
    {
        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public double BigY
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {BigY})";
        }

        public ColorXyz ToColorXyz()
        {
            return new ColorXyz()
            {
                X = BigY * X / Y,
                Y = BigY,
                Z = BigY * (1.0 - X - Y) / Y
            };
        }
    }
}
