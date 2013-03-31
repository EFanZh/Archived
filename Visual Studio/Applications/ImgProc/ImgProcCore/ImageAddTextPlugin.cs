using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProcCore
{
    public class ImageAddTextPlugin : ProcessingPlugin
    {
        public ImageAddTextPlugin()
        {
            ConfigForm = new ImageAddTextPluginForm(this);
            SetupPath = new string[] { "添加文字" };
            Name = "AddText";
            AboutInfo = "By EFanZh";
        }

        internal ImageAddTextPluginContext ImageAddTextPluginContext
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
            Bitmap newBitmap = new Bitmap(bitmap);
            Graphics g = Graphics.FromImage(newBitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            SizeF textSize = g.MeasureString(ImageAddTextPluginContext.Text, ImageAddTextPluginContext.Font);
            PointF p = new Point();
            switch (ImageAddTextPluginContext.Position)
            {
                case ContentAlignment.TopLeft:
                    p.X = 0.0f;
                    p.Y = 0.0f;
                    break;
                case ContentAlignment.TopCenter:
                    p.X = (newBitmap.Width - textSize.Width) / 2;
                    p.Y = 0.0f;
                    break;
                case ContentAlignment.TopRight:
                    p.X = newBitmap.Width - textSize.Width;
                    p.Y = 0.0f;
                    break;
                case ContentAlignment.MiddleLeft:
                    p.X = 0.0f;
                    p.Y = (newBitmap.Height - textSize.Height) / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    p.X = (newBitmap.Width - textSize.Width) / 2;
                    p.Y = (newBitmap.Height - textSize.Height) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    p.X = newBitmap.Width - textSize.Width;
                    p.Y = (newBitmap.Height - textSize.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    p.X = 0.0f;
                    p.Y = newBitmap.Height - textSize.Height;
                    break;
                case ContentAlignment.BottomCenter:
                    p.X = (newBitmap.Width - textSize.Width) / 2;
                    p.Y = newBitmap.Height - textSize.Height;
                    break;
                case ContentAlignment.BottomRight:
                    p.X = newBitmap.Width - textSize.Width;
                    p.Y = newBitmap.Height - textSize.Height;
                    break;
            }
            p.X += ImageAddTextPluginContext.XOffset;
            p.Y += ImageAddTextPluginContext.YOffset;
            g.DrawString(ImageAddTextPluginContext.Text, ImageAddTextPluginContext.Font, new SolidBrush(ImageAddTextPluginContext.Color), p);
            g.Dispose();
            return newBitmap;
        }
    }
}
