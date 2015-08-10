using System;

namespace ColorSpace
{
    internal class ColorLab
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
            const double xn = 0.95047;
            const double yn = 1.0;
            const double zn = 1.08883;

            return new ColorXyz()
            {
                X = xn * ToXyz(1.0 / 116.0 * (L + 16.0) + 1.0 / 500.0 * A),
                Y = yn * ToXyz(1.0 / 116.0 * (L + 16.0)),
                Z = zn * ToXyz(1.0 / 116.0 * (L + 16.0) - 1.0 / 200.0 * B)
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
