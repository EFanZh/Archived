using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw
{
    internal abstract class Plane : Geometry
    {
        public Brush Fill
        {
            get;
            set;
        }
    }
}
