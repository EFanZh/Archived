using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSpace
{
    internal static class D65
    {
        public static double X
        {
            get;
        } = 0.95047;

        public static double Y
        {
            get;
        } = 1.0;

        public static double Z
        {
            get;
        } = 1.08883;

        public static double SmallX
        {
            get
            {
                return X / (X + Y + Z);
            }
        }

        public static double SmallY
        {
            get
            {
                return Y / (X + Y + Z);
            }
        }
    }
}
