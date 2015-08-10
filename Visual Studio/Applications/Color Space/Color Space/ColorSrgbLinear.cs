using System;

namespace ColorSpace
{
    internal class ColorSrgbLinear
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
            return $"({R}, {G}, {B})";
        }

        public ColorSrgbLinear ToNormalized()
        {
            double k = Math.Max(Math.Max(R, G), B);

            if (k == 0.0 || Math.Min(Math.Min(R, G), B) < 0)
            {
                return new ColorSrgbLinear()
                {
                    R = 0.0,
                    G = 0.0,
                    B = 0.0
                };
            }
            else
            {
                return new ColorSrgbLinear()
                {
                    R = R / k,
                    G = G / k,
                    B = B / k
                };
            }
        }

        public ColorSrgbLinear ToCropped()
        {
            if (R < 0.0 || R > 1.0 ||
                G < 0.0 || G > 1.0 ||
                B < 0.0 || B > 1.0)
            {
                return new ColorSrgbLinear()
                {
                    R = 0.0,
                    G = 0.0,
                    B = 0.0
                };
            }
            else
            {
                return new ColorSrgbLinear()
                {
                    R = Math.Max(Math.Min(R, 1.0), 0.0),
                    G = Math.Max(Math.Min(G, 1.0), 0.0),
                    B = Math.Max(Math.Min(B, 1.0), 0.0)
                };
            }
        }

        public ColorSrgb ToColorSrgb()
        {
            return new ColorSrgb()
            {
                R = ToSrgb(R),
                G = ToSrgb(G),
                B = ToSrgb(B)
            };
        }

        private static double ToSrgb(double c)
        {
            if (c <= 0.0031308)
            {
                return 12.92 * c;
            }
            else
            {
                const double a = 0.055;

                return (1.0 + a) * Math.Pow(c, 1.0 / 2.4) - a;
            }
        }
    }
}
