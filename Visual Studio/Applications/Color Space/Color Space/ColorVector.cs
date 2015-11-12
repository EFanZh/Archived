using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSpace
{
    internal struct ColorVector
    {
        public ColorVector(double component1 = 0.0, double component2 = 0.0, double component3 = 0.0)
        {
            Component1 = component1;
            Component2 = component2;
            Component3 = component3;
        }

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

        public double Hue
        {
            get
            {
                double hue;
                double min = Math.Min(Math.Min(Component1, Component3), Component2);
                double max = Math.Max(Math.Max(Component1, Component2), Component3);

                if (max == Component1)
                {
                    hue = (Component2 - Component3) / (max - min);
                }
                else if (max == Component2)
                {
                    hue = 2.0 + (Component3 - Component1) / (max - min);
                }
                else
                {
                    hue = 4.0 + (Component1 - Component2) / (max - min);
                }

                if (hue < 0.0)
                {
                    hue += 6.0;
                }

                return hue;
            }
        }

        public void ConvertXyyToLinearSRgb()
        {
            // TODO: Check this.
            if (Component3 == 0.0)
            {
                Component1 = 0.0;
                Component2 = 0.0;
            }

            const double f11 = 330000.0 / 88229.0;
            const double f12 = -275000.0 / 264687.0;
            const double f13 = -44000.0 / 88229.0;
            const double f21 = -12500.0 / 12367.0;
            const double f22 = 612500.0 / 333909.0;
            const double f23 = 4625.0 / 111303.0;
            const double f31 = -30000.0 / 29963.0;
            const double f32 = -340000.0 / 269667.0;
            const double f33 = 5000.0 / 4731.0;
            double x = Component1;
            double y = Component2;
            double bigY = Component3;
            double scale = bigY / y;

            Component1 = (f11 * x + f12 * y + f13) * scale;
            Component2 = (f21 * x + f22 * y + f23) * scale;
            Component3 = (f31 * x + f32 * y + f33) * scale;
        }

        public void CompressLuminance()
        {
            double max = Math.Max(Math.Max(Component1, Component2), Component3);

            if (max > 1.0)
            {
                Component1 /= max;
                Component2 /= max;
                Component3 /= max;
            }
        }

        public bool IsCanonical()
        {
            return Component1 >= 0.0 && Component1 <= 1.0 &&
                   Component2 >= 0.0 && Component2 <= 1.0 &&
                   Component3 >= 0.0 && Component3 <= 1.0;
        }

        public void Chop()
        {
            if (Component1 < 0.0 || Component1 > 1.0 ||
                Component2 < 0.0 || Component2 > 1.0 ||
                Component3 < 0.0 || Component3 > 1.0)
            {
                Component1 = 0.0;
                Component2 = 0.0;
                Component3 = 0.0;
            }
        }

        public void ConvertLinearSRgbToSRgb()
        {
            Component1 = LinearSRgbToSRgb(Component1);
            Component2 = LinearSRgbToSRgb(Component2);
            Component3 = LinearSRgbToSRgb(Component3);
        }

        public void ConvertSRgbToLinearSRgb()
        {
            Component1 = SRgbToLinearSRgb(Component1);
            Component2 = SRgbToLinearSRgb(Component2);
            Component3 = SRgbToLinearSRgb(Component3);
        }

        public void ConvertXyyToSRgbSmart()
        {
            const double f11 = 330000.0 / 88229.0;
            const double f12 = -275000.0 / 264687.0;
            const double f13 = -44000.0 / 88229.0;
            const double f21 = -12500.0 / 12367.0;
            const double f22 = 612500.0 / 333909.0;
            const double f23 = 4625.0 / 111303.0;
            const double f31 = -30000.0 / 29963.0;
            const double f32 = -340000.0 / 269667.0;
            const double f33 = 5000.0 / 4731.0;
            double x = Component1;
            double y = Component2;
            double bigY = Component3;
            double scale = bigY / y;
            double r = (f11 * x + f12 * y + f13) * scale;
            double g = (f21 * x + f22 * y + f23) * scale;
            double b = (f31 * x + f32 * y + f33) * scale;

            SetSRgbSmart(r, g, b, bigY);
        }

        public void ConvertXyzToXyy()
        {
            double total = Component1 + Component2 + Component3;
            double bigY = Component2;

            if (total != 0.0)
            {
                Component1 /= total;
                Component2 /= total;
                Component3 = bigY;
            }
        }

        public void ConvertXyzToSRgbSmart()
        {
            const double f11 = 286000.0 / 88229.0;
            const double f12 = -407000.0 / 264687.0;
            const double f13 = -44000.0 / 88229.0;
            const double f21 = -107875.0 / 111303.0;
            const double f22 = 626375.0 / 333909.0;
            const double f23 = 4625.0 / 111303.0;
            const double f31 = 5000.0 / 89889.0;
            const double f32 = -55000.0 / 269667.0;
            const double f33 = 5000.0 / 4731.0;
            double x = Component1;
            double y = Component2;
            double z = Component3;
            double r = f11 * x + f12 * y + f13 * z;
            double g = f21 * x + f22 * y + f23 * z;
            double b = f31 * x + f32 * y + f33 * z;

            SetSRgbSmart(r, g, b, y);
        }

        public void ConvertLinearSRgbToXyz()
        {
            const double m11 = 8504.0 / 20625.0;
            const double m12 = 447.0 / 1250.0;
            const double m13 = 361.0 / 2000.0;
            const double m21 = 1063.0 / 5000.0;
            const double m22 = 447.0 / 625.0;
            const double m23 = 361.0 / 5000.0;
            const double m31 = 1063.0 / 55000.0;
            const double m32 = 149.0 / 1250.0;
            const double m33 = 28519.0 / 30000.0;
            double r = Component1;
            double g = Component2;
            double b = Component3;
            Component1 = (m11 * r + m12 * g + m13 * b);
            Component2 = (m21 * r + m22 * g + m23 * b);
            Component3 = (m31 * r + m32 * g + m33 * b);
        }

        public Color ToColor()
        {
            double min = Math.Min(Math.Min(Component1, Component2), Component3);
            double max = Math.Max(Math.Max(Component1, Component2), Component3);

            if (min >= 0 && max <= 1)
            {
                return Color.FromArgb(ToColorComponent(Component1), ToColorComponent(Component2), ToColorComponent(Component3));
            }
            else
            {
                return Color.Transparent;
            }
        }

        private void SetSRgbSmart(double r, double g, double b, double bigY)
        {
            double lowerBound = 0.0;
            double upperBound = 1.0;
            double rOffset = r - bigY;
            double gOffset = g - bigY;
            double bOffset = b - bigY;

            UpdateSRgbBounds(bigY, rOffset, ref lowerBound, ref upperBound);
            UpdateSRgbBounds(bigY, gOffset, ref lowerBound, ref upperBound);
            UpdateSRgbBounds(bigY, bOffset, ref lowerBound, ref upperBound);

            if (lowerBound > upperBound)
            {
                Component1 = 0.0;
                Component2 = 0.0;
                Component3 = 0.0;
            }
            else
            {
                Component1 = LinearSRgbToSRgb(bigY + rOffset * upperBound);
                Component2 = LinearSRgbToSRgb(bigY + gOffset * upperBound);
                Component3 = LinearSRgbToSRgb(bigY + bOffset * upperBound);
            }
        }

        private static void UpdateSRgbBounds(double y, double offset, ref double lowerBound, ref double upperBound)
        {
            if (offset < 0)
            {
                lowerBound = Math.Max(lowerBound, (1.0 - y) / offset);
                upperBound = Math.Min(upperBound, -y / offset);
            }
            else if (offset > 0)
            {
                lowerBound = Math.Max(lowerBound, -y / offset);
                upperBound = Math.Min(upperBound, (1.0 - y) / offset);
            }
        }

        private static double LinearSRgbToSRgb(double c)
        {
            return c <= 0.0031308 ? c * 12.92 : Math.Pow(c, 1.0 / 2.4) * 1.055 - 0.055;
        }

        private static double SRgbToLinearSRgb(double c)
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

        private static byte ToColorComponent(double c)
        {
            return c < 1.0 ? (byte)(c * 256.0) : byte.MaxValue;
        }
    }
}
