using System.Drawing;

namespace MatrixTransform
{
    internal struct Matrix2x2
    {
        public float A11
        {
            get;
            set;
        }

        public float A12
        {
            get;
            set;
        }

        public float A21
        {
            get;
            set;
        }

        public float A22
        {
            get;
            set;
        }

        public PointF Transform(PointF point)
        {
            return new PointF(A11 * point.X + A12 * point.Y, A21 * point.X + A22 * point.Y);
        }
    }
}
