using System;

namespace Sorting
{
    internal class SortEventArgs : EventArgs
    {
        public SortEventArgs(int a, int b)
        {
            A = a;
            B = b;
        }

        public int A
        {
            get;
            private set;
        }

        public int B
        {
            get;
            private set;
        }
    }
}
