using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBody
{
    internal class Planet
    {
        private Vector Location
        {
            get;
            set;
        }

        private double Mass
        {
            get;
            set;
        }

        private Vector Velocity
        {
            get;
            set;
        }
    }
}
