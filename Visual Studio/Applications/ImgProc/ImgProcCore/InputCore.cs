using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProcCore
{
    public class InputCore : InputPlugin
    {
        public InputCore()
        {
            SupportedExtensions = new Dictionary<string, string[]>()
            {
                { "BMP 图像", new string[] { "bmp" } },
                { "GIF 图像", new string[] { "gif" } },
                { "JPEG 图像", new string[] { "jpeg", "jpg" } },
                { "PNG 图像", new string[] { "png" } }
            };
            Name = "InputCore";
            ConfigForm = null;
            AboutInfo = "By EFanZh";
        }

        public override IDictionary<string, string[]> SupportedExtensions
        {
            get;
            protected set;
        }

        public override Bitmap GetBitmap(string path)
        {
            return new Bitmap(path);
        }

        public override string Name
        {
            get;
            protected set;
        }

        public override Form ConfigForm
        {
            get;
            protected set;
        }

        public override string AboutInfo
        {
            get;
            protected set;
        }
    }
}
