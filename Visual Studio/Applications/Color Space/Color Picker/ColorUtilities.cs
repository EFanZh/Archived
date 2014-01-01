using System;
using System.Drawing;

namespace ColorPicker
{
    internal static class ColorUtilities
    {
        private static double SRGBLinearToSRGB(double x)
        {
            return x <= 0.0031308 ? 12.92 * x : 1.055 * Math.Pow(x, 1.0 / 2.4) - 0.055;
        }

        public static ColorD XYZToSRGB(ColorD color_xyz)
        {
            double rl = 3.2406 * color_xyz.C1 - 1.5372 * color_xyz.C2 - 0.4986 * color_xyz.C3;
            double gl = -0.9689 * color_xyz.C1 + 1.8758 * color_xyz.C2 + 0.0415 * color_xyz.C3;
            double bl = 0.0557 * color_xyz.C1 - 0.204 * color_xyz.C2 + 1.057 * color_xyz.C3;

            return new ColorD(SRGBLinearToSRGB(rl), SRGBLinearToSRGB(gl), SRGBLinearToSRGB(bl));
        }

        public static ColorD XYYToXYZ(ColorD xyy)
        {
            double t = xyy.C3 / xyy.C2;
            return new ColorD(t * xyy.C1, xyy.C3, t * (1 - xyy.C1 - xyy.C2));
        }

        private static double Confine(double x)
        {
            return x < 0.0 ? 0.0 : x < 1.0 ? x : 1.0;
        }

        public static ColorD CompressRGB(ColorD rgb)
        {
            if (rgb.C1 < 0.0 || rgb.C1 > 1.0 || rgb.C2 < 0.0 || rgb.C2 > 1.0 || rgb.C3 < 0.0 || rgb.C3 > 1.0)
            {
                double max = Math.Max(Math.Max(rgb.C1, rgb.C2), rgb.C3);
                return new ColorD(Confine(rgb.C1 / max * 0.5), Confine(rgb.C2 / max * 0.5), Confine(rgb.C3 / max * 0.5));
            }
            else
            {
                return rgb;
            }
        }

        public static Color SRGBToColor(ColorD rgb)
        {
            return Color.FromArgb((int)rgb.C1, (int)rgb.C2, (int)rgb.C3);
        }

        public static ColorB SRGBToColorI(ColorD rgb)
        {
            return new ColorB((byte)(255 * rgb.C1), (byte)(255 * rgb.C2), (byte)(255 * rgb.C3));
        }

        public static ColorB XYYToColorI(ColorD xyy)
        {
            return SRGBToColorI(CompressRGB(XYZToSRGB(XYYToXYZ(xyy))));
        }
    }
}
