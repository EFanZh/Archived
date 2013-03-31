using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ImgProc.Shared;

namespace ImgProcCore
{
    public class ImageMosaicPlugin : ProcessingPlugin
    {
        public ImageMosaicPlugin()
        {
            ConfigForm = new ImageMosaicPluginForm(this);
            SetupPath = new string[] { "马赛克" };
            Name = "ImageMosaic";
            AboutInfo = "By EFanZh";
        }

        public int Size
        {
            get;
            set;
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
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics graphics = Graphics.FromImage(newBitmap);
            for (int y = 0; y < bitmap.Height; y += Size)
            {
                for (int x = 0; x < bitmap.Width; x += Size)
                {
                    int a = 0, r = 0, g = 0, b = 0;
                    int yb = y + Size;
                    int xb = x + Size;
                    for (int yt = y; yt < yb && yt < bitmap.Height; yt++)
                    {
                        for (int xt = x; xt < xb && xt < bitmap.Width; xt++)
                        {
                            Color color = bitmap.GetPixel(xt, yt);
                            a += color.A;
                            r += color.R;
                            g += color.G;
                            b += color.B;
                        }
                    }
                    int wt, ht;
                    if (x + Size > bitmap.Width)
                    {
                        wt = bitmap.Width - x;
                    }
                    else
                    {
                        wt = Size;
                    }
                    if (y + Size > bitmap.Height)
                    {
                        ht = bitmap.Height - y;
                    }
                    else
                    {
                        ht = Size;
                    }
                    double nt = wt * ht;
                    Color newColor = Color.FromArgb((int)Math.Round(a / nt), (int)Math.Round(r / nt), (int)Math.Round(g / nt), (int)Math.Round(b / nt));
                    graphics.FillRectangle(new SolidBrush(newColor), x, y, wt, ht);
                }
                if (y + Size < bitmap.Height)
                {
                    this.DoProcessAsyncProgressChanged(asyncOp, new ProgressChangedEventArgs((int)Math.Round(100.0 * (y + Size) / bitmap.Height), asyncOp.UserSuppliedState));
                }
            }
            graphics.Dispose();
            return newBitmap;
        }
    }
}
