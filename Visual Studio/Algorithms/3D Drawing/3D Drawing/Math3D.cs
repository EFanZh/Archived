using System.Runtime.InteropServices;

namespace ThreeDDrawing
{
    internal static class Math3D
    {
        private const long inv_sqrt_magic_number_64_bit = 0x5fe6eb50c7b537a9;

        [StructLayout(LayoutKind.Explicit, Size = 8)]
        private struct LongDouble
        {
            public LongDouble(double x)
            {
                long_value = 0;
                double_value = x;
            }

            [FieldOffset(0)]
            public double double_value;

            [FieldOffset(0)]
            public long long_value;
        }

        // See https://en.wikipedia.org/wiki/Fast_inverse_square_root for more information.
        public static double InvSqrt(double x)
        {
            LongDouble i = new LongDouble(x);
            double x2 = x * 0.5, y;
            const double threehalfs = 1.5;

            i.long_value = inv_sqrt_magic_number_64_bit - (i.long_value >> 1);
            y = i.double_value;

            y *= threehalfs - (x2 * y * y);   // 1st iteration
            y *= threehalfs - (x2 * y * y);   // 2nd iteration

            //  y *= threehalfs - (x2 * y * y);   // 3nd iteration

            return y;
        }
    }
}
