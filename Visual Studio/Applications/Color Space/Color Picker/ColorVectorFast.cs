using System.Windows.Media;

namespace ColorPicker
{
    internal class ColorVectorFast
    {
        public double Component1
        {
            get;
            set;
        }

        public double Component2
        {
            get;
            set;
        }

        public double Component3
        {
            get;
            set;
        }

        public void Transform(FastColorTransformMatrix matrix)
        {
            double c1 = Component1;
            double c2 = Component2;
            double c3 = Component3;

            Component1 = matrix.M11 * c1 + matrix.M12 * c2 + matrix.M13 * c3;
            Component2 = matrix.M21 * c1 + matrix.M22 * c2 + matrix.M23 * c3;
            Component3 = matrix.M31 * c1 + matrix.M32 * c2 + matrix.M33 * c3;
        }

        public Color ToColor()
        {
            return Color.FromScRgb(1.0f, (float)Component1, (float)Component2, (float)Component3);
        }
    }
}
