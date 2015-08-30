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

        public void ConvertXyyToSRgb()
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

        public void ConvertSRgbToFinalSRgb()
        {
            Component1 = SRgbToFinalSRgb(Component1);
            Component2 = SRgbToFinalSRgb(Component2);
            Component3 = SRgbToFinalSRgb(Component3);
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
                Component1 = SRgbToFinalSRgb(bigY + rOffset * upperBound);
                Component2 = SRgbToFinalSRgb(bigY + gOffset * upperBound);
                Component3 = SRgbToFinalSRgb(bigY + bOffset * upperBound);
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

        private static double SRgbToFinalSRgb(double x)
        {
            return x <= 0.0031308 ? x * 12.92 : Math.Pow(x, 1.0 / 2.4) * 1.055 - 0.055;
        }

        private static byte ToColorComponent(double c)
        {
            return c < 1.0 ? (byte)(c * 256.0) : byte.MaxValue;
        }
    }
}
