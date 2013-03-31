using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProcCore
{
    internal class OutputPNG : OutputPlugin
    {
        public OutputPNG()
        {
            TypeName = "PNG 图像";
            SelectedExtension = "png";
            Name = "OutputPNG";
            AboutInfo = "By EFanZh";
            ConfigForm = null;
        }

        public override string TypeName
        {
            get;
            protected set;
        }

        public override string SelectedExtension
        {
            get;
            protected set;
        }

        public override void Output(Bitmap bitmap, string path)
        {
            bitmap.Save(path, ImageFormat.Png);
        }

        public override string Name
        {
            get;
            protected set;
        }

        public override string AboutInfo
        {
            get;
            protected set;
        }

        public override Form ConfigForm
        {
            get;
            protected set;
        }
    }
}
