using System.Drawing;

namespace ImgProcCore
{
    internal class ImageCutPluginContext
    {
        public Size Size
        {
            get;
            set;
        }

        public ContentAlignment Position
        {
            get;
            set;
        }

        public Color FillColor
        {
            get;
            set;
        }
    }
}
