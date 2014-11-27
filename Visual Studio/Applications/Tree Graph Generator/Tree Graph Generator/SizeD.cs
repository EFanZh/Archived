using System.Drawing;

namespace TreeGraphGenerator
{
    internal struct SizeD
    {
        private double width;
        private double height;

        public SizeD(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public static implicit operator SizeF(SizeD size)
        {
            return new SizeF((float)size.Width, (float)size.Height);
        }
    }
}
