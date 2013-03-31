using System.Drawing;

namespace ImgProcCore
{
    internal class ImageAddTextPluginContext
    {
        public int XOffset
        {
            get;
            set;
        }

        public int YOffset
        {
            get;
            set;
        }

        public ContentAlignment Position
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }
    }
}
