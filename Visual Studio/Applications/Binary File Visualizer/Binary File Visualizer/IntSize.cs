using System.Windows;

namespace BinaryFileVisualizer
{
    internal struct IntSize
    {
        public IntSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public Size ToSize()
        {
            return new Size(Width, Height);
        }

        public static IntSize FromSize(Size size)
        {
            return new IntSize((int)size.Width, (int)size.Height);
        }
    }
}
