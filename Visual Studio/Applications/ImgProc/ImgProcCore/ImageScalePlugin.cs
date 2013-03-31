using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProcCore
{
    public class ImageScalePlugin : ProcessingPlugin
    {
        public ImageScalePlugin()
        {
            ConfigForm = new ImageScalePluginForm(this);
            SetupPath = new string[] { "缩放" };
            Name = "ImageScale";
            AboutInfo = "By EFanZh";
        }

        internal ImageScalePluginContext ScaleContext
        {
            get;
            set;
        }

        public override Form ConfigForm
        {
            get;
            protected set;
        }

        public override IEnumerable<string> SetupPath
        {
            get;
            protected set;
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

        protected override Bitmap Process(Bitmap bitmap, AsyncOperation asyncOp)
        {
            Bitmap newBitmap = null;
            switch (ScaleContext.ScaleMethod)
            {
                case ImageScalePluginMethod.KeepSize:
                    newBitmap = new Bitmap(ScaleContext.Width, ScaleContext.Height);
                    break;
                case ImageScalePluginMethod.KeepAspectRatioWidth:
                    newBitmap = new Bitmap(ScaleContext.Width, (int)Math.Round(ScaleContext.Width * ((double)bitmap.Height / bitmap.Width)));
                    break;
                case ImageScalePluginMethod.KeepAspectRatioHeight:
                    newBitmap = new Bitmap((int)Math.Round(ScaleContext.Height * ((double)bitmap.Width / bitmap.Height), ScaleContext.Width), ScaleContext.Height);
                    break;
            }
            newBitmap.SetResolution(ScaleContext.Resolution, ScaleContext.Resolution);
            Graphics g = Graphics.FromImage(newBitmap);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bitmap, 0, 0, newBitmap.Width, newBitmap.Height);
            g.Dispose();
            return newBitmap;
        }
    }
}
