using System.Drawing;

namespace BezierFitting
{
    internal struct PointD
    {
        private double pointd_x;
        private double pointd_y;

        public PointD(double x = 0, double y = 0)
        {
            pointd_x = x;
            pointd_y = y;
        }

        public double X
        {
            get
            {
                return pointd_x;
            }
            set
            {
                pointd_x = value;
            }
        }

        public double Y
        {
            get
            {
                return pointd_y;
            }
            set
            {
                pointd_y = value;
            }
        }

        public PointF ToPointF()
        {
            return new PointF((float)X, (float)Y);
        }
    }
}
