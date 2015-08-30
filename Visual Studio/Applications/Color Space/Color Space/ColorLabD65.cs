using System;

namespace ColorSpace
{
    internal class ColorLabD65
    {
        public double L
        {
            get;
            set;
        }

        public double A
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
            return $"({L}, {A}, {B})";
        }

        public ColorXyz ToColorXyz()
        {
            return new ColorXyz()
            {
                X = D65.X * ToXyz(1.0 / 116.0 * (L + 16.0) + 1.0 / 500.0 * A),
                Y = D65.Y * ToXyz(1.0 / 116.0 * (L + 16.0)),
                Z = D65.Z * ToXyz(1.0 / 116.0 * (L + 16.0) - 1.0 / 200.0 * B)
            };
        }

        public double ToXyz(double t)
        {
            if (t > 6.0 / 29.0)
            {
                return Math.Pow(t, 3.0);
            }
            else
            {
                return 3.0 * (6.0 / 29.0) * (6.0 / 29.0) * (t - 4.0 / 29.0);
            }
        }
    }
}
