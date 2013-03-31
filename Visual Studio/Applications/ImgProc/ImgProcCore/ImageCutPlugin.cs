using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProcCore
{
    public class ImageCutPlugin : ProcessingPlugin
    {
        public ImageCutPlugin()
        {
            ConfigForm = new ImageCutPluginForm(this);
            SetupPath = new string[] { "裁剪" };
            Name = "ImageCut";
            AboutInfo = "By EFanZh";
        }

        internal ImageCutPluginContext ImageCutPluginContext
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
            Bitmap newBitmap = new Bitmap(ImageCutPluginContext.Size.Width, ImageCutPluginContext.Size.Height);
            newBitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            Graphics g = Graphics.FromImage(newBitmap);
            g.Clear(ImageCutPluginContext.FillColor);
            Point p = new Point();
            switch (ImageCutPluginContext.Position)
            {
                case ContentAlignment.TopLeft:
                    p.X = 0;
                    p.Y = 0;
                    break;
                case ContentAlignment.TopCenter:
                    p.X = (int)Math.Round((newBitmap.Width - bitmap.Width) / 2.0);
                    p.Y = 0;
                    break;
                case ContentAlignment.TopRight:
                    p.X = newBitmap.Width - bitmap.Width;
                    p.Y = 0;
                    break;
                case ContentAlignment.MiddleLeft:
                    p.X = 0;
                    p.Y = (int)Math.Round((newBitmap.Height - bitmap.Height) / 2.0);
                    break;
                case ContentAlignment.MiddleCenter:
                    p.X = (int)Math.Round((newBitmap.Width - bitmap.Width) / 2.0);
                    p.Y = (int)Math.Round((newBitmap.Height - bitmap.Height) / 2.0);
                    break;
                case ContentAlignment.MiddleRight:
                    p.X = newBitmap.Width - bitmap.Width;
                    p.Y = (int)Math.Round((newBitmap.Height - bitmap.Height) / 2.0);
                    break;
                case ContentAlignment.BottomLeft:
                    p.X = 0;
                    p.Y = newBitmap.Height - bitmap.Height;
                    break;
                case ContentAlignment.BottomCenter:
                    p.X = (int)Math.Round((newBitmap.Width - bitmap.Width) / 2.0);
                    p.Y = newBitmap.Height - bitmap.Height;
                    break;
                case ContentAlignment.BottomRight:
                    p.X = newBitmap.Width - bitmap.Width;
                    p.Y = newBitmap.Height - bitmap.Height;
                    break;
            }
            g.DrawImage(bitmap, p);
            g.Dispose();
            return newBitmap;
        }
    }
}
