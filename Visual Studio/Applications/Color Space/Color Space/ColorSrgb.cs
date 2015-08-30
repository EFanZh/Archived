using System;

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

        public ColorSrgbLinear ToColorSrgbLinear()
        {
            return new ColorSrgbLinear()
            {
                R = ToSrgbLinear(R),
                G = ToSrgbLinear(G),
                B = ToSrgbLinear(B)
            };
        }

        private static double ToSrgbLinear(double c)
        {
            const double a = 0.055;

            if (c <= 0.04045)
            {
                return c / 12.92;
            }
            else
            {
                return Math.Pow((c + a) / (1 + a), 2.4);
            }
        }
    }
}
