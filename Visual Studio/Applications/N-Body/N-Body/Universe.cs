using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBody
{
    internal class Universe
    {
        public double G
        {
            get;
            set;
        }

        public IEnumerable<Planet> Planets
        {
            get;
            private set;
        }
    }
}
