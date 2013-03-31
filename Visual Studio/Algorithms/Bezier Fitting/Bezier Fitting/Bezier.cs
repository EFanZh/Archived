namespace BezierFitting
{
    internal struct Bezier
    {
        private PointD bezier_point1, bezier_point2, bezier_point3, bezier_point4;

        public PointD Point1
        {
            get
            {
                return bezier_point1;
            }
            set
            {
                bezier_point1 = value;
            }
        }

        public PointD Point2
        {
            get
            {
                return bezier_point2;
            }
            set
            {
                bezier_point2 = value;
            }
        }

        public PointD Point3
        {
            get
            {
                return bezier_point3;
            }
            set
            {
                bezier_point3 = value;
            }
        }

        public PointD Point4
        {
            get
            {
                return bezier_point4;
            }
            set
            {
                bezier_point4 = value;
            }
        }
    }
}
