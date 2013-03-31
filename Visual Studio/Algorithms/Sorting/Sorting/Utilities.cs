using System;

namespace Sorting
{
    internal static class Utilities
    {
        static Utilities()
        {
            Random = new Random();
        }

        public static Random Random
        {
            get;
            private set;
        }

        public static void Swap(this int[] data, int a, int b)
        {
            int t = data[a];
            data[a] = data[b];
            data[b] = t;
        }
    }
}
