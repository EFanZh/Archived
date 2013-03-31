using System;
using System.Collections.Generic;
using System.Drawing;

namespace Gravitation
{
    internal abstract class Scene
    {
        private static Random random = new Random();

        protected Random Random
        {
            get
            {
                return random;
            }
        }

        public Size Size
        {
            get;
            set;
        }

        public double GravitationalConstant
        {
            get;
            set;
        }

        public IEnumerable<Tuple<PointF, double>> MassPoints
        {
            get;
            set;
        }

        public double MaxGravitation
        {
            get;
            set;
        }

        public abstract void Render(Graphics graphics);

        protected static int Dithering(double x)
        {
            int x1 = (int)x;
            double xd = x - x1;
            return random.NextDouble() < xd ? x1 + 1 : x1;
        }
    }
}
