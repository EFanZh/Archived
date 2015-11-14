using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearchColors
{
    internal class Searcher
    {
        private Color target;
        private int min = int.MaxValue;
        private SpinLock spinLock = new SpinLock();

        public Searcher(Color target)
        {
            this.target = target;
        }

        public bool Test(string file, Color color)
        {
            try
            {
                bool locked = false;
                spinLock.Enter(ref locked);

                int distance = GetDistance(target, color);

                if (distance <= min)
                {
                    min = distance;
                    File = file;
                    FoundColor = color;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                spinLock.Exit();
            }
        }

        public string File
        {
            get;
            private set;
        }

        public Color FoundColor
        {
            get;
            private set;
        }

        private static int GetDistance(Color color1, Color color2)
        {
            int dr = color2.R - color1.R;
            int dg = color2.G - color1.G;
            int db = color2.B - color1.B;

            return dr * dr + dg * dg + db * db;
        }
    }
}
