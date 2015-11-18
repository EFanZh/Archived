namespace ColorSpace.Common
{
    internal static class WhitePoints
    {
        public static decimal D65X
        {
            get
            {
                return 313637m / 1003060m;
            }
        }

        public static decimal D65Y
        {
            get
            {
                return 16500m / 50153m;
            }
        }

        public static decimal D65Z => 1m - D65X - D65Y;
    }
}
