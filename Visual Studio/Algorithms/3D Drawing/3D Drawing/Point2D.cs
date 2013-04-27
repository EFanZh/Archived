namespace ThreeDDrawing
{
    internal class Point2D
    {
        public Point2D()
            : this(0.0, 0.0)
        {
        }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }
    }
}
