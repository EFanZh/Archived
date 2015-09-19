namespace ColorPicker
{
    internal struct ColorTransformMatrix
    {
        public ColorTransformMatrix(decimal m11, decimal m12, decimal m13, decimal m21, decimal m22, decimal m23, decimal m31, decimal m32, decimal m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        public decimal M11
        {
            get;
            set;
        }

        public decimal M12
        {
            get;
            set;
        }

        public decimal M13
        {
            get;
            set;
        }

        public decimal M21
        {
            get;
            set;
        }

        public decimal M22
        {
            get;
            set;
        }

        public decimal M23
        {
            get;
            set;
        }

        public decimal M31
        {
            get;
            set;
        }

        public decimal M32
        {
            get;
            set;
        }

        public decimal M33
        {
            get;
            set;
        }

        public void Transform(ColorTransformMatrix matrix)
        {
            decimal m11 = matrix.M11;
            decimal m12 = matrix.M12;
            decimal m13 = matrix.M13;
            decimal m21 = matrix.M21;
            decimal m22 = matrix.M22;
            decimal m23 = matrix.M23;
            decimal m31 = matrix.M31;
            decimal m32 = matrix.M32;
            decimal m33 = matrix.M33;

            decimal c1 = M11;
            decimal c2 = M21;
            decimal c3 = M31;

            M11 = m11 * c1 + m12 * c2 + m13 * c3;
            M21 = m21 * c1 + m22 * c2 + m23 * c3;
            M31 = m31 * c1 + m32 * c2 + m33 * c3;

            c1 = M12;
            c2 = M22;
            c3 = M32;

            M12 = m11 * c1 + m12 * c2 + m13 * c3;
            M22 = m21 * c1 + m22 * c2 + m23 * c3;
            M32 = m31 * c1 + m32 * c2 + m33 * c3;

            c1 = M13;
            c2 = M23;
            c3 = M33;

            M13 = m11 * c1 + m12 * c2 + m13 * c3;
            M23 = m21 * c1 + m22 * c2 + m23 * c3;
            M33 = m31 * c1 + m32 * c2 + m33 * c3;
        }

        public FastColorTransformMatrix ToFastColorTransformMatrix()
        {
            return new FastColorTransformMatrix((double)M11, (double)M12, (double)M13,
                (double)M21, (double)M22, (double)M23,
                (double)M31, (double)M32, (double)M33);
        }
    }
}
