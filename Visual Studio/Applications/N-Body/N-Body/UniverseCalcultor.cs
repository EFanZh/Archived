using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBody
{
    internal class UniverseCalcultor
    {
        public Universe Universe
        {
            get;
            set;
        }

        public double FrameRate
        {
            get;
            set;
        }

        private void NextFrame()
        {
            foreach (var planet in Universe.Planets)
            {
            }
        }
    }
}
