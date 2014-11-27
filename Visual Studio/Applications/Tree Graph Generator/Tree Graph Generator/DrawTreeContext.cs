using System.Drawing;

namespace TreeGraphGenerator
{
    internal class DrawTreeContext
    {
        public Graphics Graphics
        {
            get;
            set;
        }

        public Brush BackgroundBrush
        {
            get;
            set;
        }

        public Pen BorderPen
        {
            get;
            set;
        }

        public Pen ConnectorPen
        {
            get;
            set;
        }

        public Font LabelFont
        {
            get;
            set;
        }

        public Brush LabelBrush
        {
            get;
            set;
        }

        public double LabelHeight
        {
            get;
            set;
        }

        public double NodeHorizontalPadding
        {
            get;
            set;
        }

        public double NodeVerticalPadding
        {
            get;
            set;
        }

        public double NodeHorizontalSep
        {
            get;
            set;
        }

        public double NodeVerticalSep
        {
            get;
            set;
        }
    }
}
