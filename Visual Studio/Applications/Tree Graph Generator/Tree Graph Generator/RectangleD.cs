using System.Drawing;

namespace TreeGraphGenerator
{
    internal struct RectangleD
    {
        private PointD location;
        private SizeD size;

        public RectangleD(PointD location, SizeD size)
        {
            this.location = location;
            this.size = size;
        }

        public RectangleD(double x, double y, double width, double height)
            : this(new PointD(x, y), new SizeD(width, height))
        {
        }

        public PointD Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        public SizeD Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public double X
        {
            get
            {
                return location.X;
            }
            set
            {
                location.X = value;
            }
        }

        public double Y
        {
            get
            {
                return location.Y;
            }
            set
            {
                location.Y = value;
            }
        }

        public double Width
        {
            get
            {
                return size.Width;
            }
            set
            {
                size.Width = value;
            }
        }

        public double Height
        {
            get
            {
                return size.Height;
            }
            set
            {
                size.Height = value;
            }
        }

        public static implicit operator RectangleF(RectangleD rectangle)
        {
            return new RectangleF(rectangle.Location, rectangle.Size);
        }
    }
}
