using System.Drawing;

namespace TreeGraphGenerator
{
    internal struct PointD
    {
        private double x;
        private double y;

        public PointD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public static implicit operator PointF(PointD point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }
    }
}
