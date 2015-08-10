using System;

namespace ColorSpace
{
    internal class ColorXyz
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

        public double Z
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public ColorSrgbLinear ToSrgbLinear()
        {
            return new ColorSrgbLinear()
            {
                R = 3.2406 * X - 1.5372 * Y - 0.4986 * Z,
                G = -0.9689 * X + 1.8758 * Y + 0.0415 * Z,
                B = 0.0557 * X - 0.204 * Y + 1.057 * Z
            };
        }

        public ColorLab ToColorLab()
        {
            const double xn = 0.95047;
            const double yn = 1.0;
            const double zn = 1.08883;

            return new ColorLab()
            {
                L = 116.0 * ToLab(Y / yn) - 16.0,
                A = 500.0 * (ToLab(X / xn) - ToLab(Y / yn)),
                B = 200.0 * (ToLab(Y / yn) - ToLab(Z / zn))
            };
        }

        private static double ToLab(double t)
        {
            if (t > Math.Pow(6.0 / 29.0, 3.0))
            {
                return Math.Pow(t, 1.0 / 3.0);
            }
            else
            {
                return (1.0 / 3.0) * (29.0 / 6.0) * (29.0 / 6.0) * t + 4.0 / 29.0;
            }
        }
    }
}
