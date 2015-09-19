namespace ColorPicker
{
    internal struct ColorVector
    {
        public ColorVector(decimal component1, decimal component2, decimal component3)
        {
            Component1 = component1;
            Component2 = component2;
            Component3 = component3;
        }

        public decimal Component1
        {
            get;
            set;
        }

        public decimal Component2
        {
            get;
            set;
        }

        public decimal Component3
        {
            get;
            set;
        }

        public void Transform(ColorTransformMatrix matrix)
        {
            decimal c1 = Component1;
            decimal c2 = Component2;
            decimal c3 = Component3;

            Component1 = matrix.M11 * c1 + matrix.M12 * c2 + matrix.M13 * c3;
            Component2 = matrix.M21 * c1 + matrix.M22 * c2 + matrix.M23 * c3;
            Component3 = matrix.M31 * c1 + matrix.M32 * c2 + matrix.M33 * c3;
        }

        public void XyyToXyz()
        {
            decimal scale = Component3 / Component2;
            decimal x = Component1 * scale;
            decimal z = (1.0m - Component1 - Component2) * scale;

            Component1 = x;
            Component2 = Component3;
            Component3 = z;
        }

        public void XyzToXyy()
        {
            decimal total = Component1 + Component2 + Component3;
            decimal x = Component1 / total;
            decimal y = Component2 / total;

            Component1 = x;
            Component3 = Component2;
            Component2 = y;
        }
    }
}
