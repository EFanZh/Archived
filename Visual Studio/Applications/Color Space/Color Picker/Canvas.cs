using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPicker
{
    internal class Canvas : Control
    {
        private double y;
        private bool need_new_bitmap = true;
        private bool need_redraw;
        private int size;
        private Bitmap bitmap;
        private static double s3 = 0.5 * Math.Sqrt(3);
        private static double value_size;

        public Canvas()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(GetBitmap(e.Graphics), 0, 0);
            base.OnPaint(e);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            int t = Math.Min(this.ClientSize.Width, this.ClientSize.Height);
            if (size != t)
            {
                size = t;
                value_size = size * s3;
                need_new_bitmap = true;
                this.Invalidate();
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                need_redraw = true;
                this.Invalidate();
            }
        }

        private Bitmap GetBitmap(Graphics graphics)
        {
            if (need_new_bitmap)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                bitmap = new Bitmap(size, size, graphics);
                need_redraw = true;
            }
            if (need_redraw)
            {
                BitmapData bitmap_data = bitmap.LockBits(new Rectangle(0, 0, size, size), ImageLockMode.WriteOnly, bitmap.PixelFormat);
                IntPtr base_addr = bitmap_data.Scan0;

                for (int j = 0; j < size; j++)
                {
                    int offset_j = j * bitmap_data.Stride;

                    for (int i = 0; i < size; i++)
                    {
                        int offset = offset_j + i * 4;

                        double y = size - j - 0.5;
                        double x = s3 * (i + 0.5) - 0.5 * y;

                        if (x >= 0 && y >= 0 && x + y < value_size)
                        {
                            ColorB color = ColorUtilities.XYYToColorI(new ColorD(x / value_size, y / value_size, Y));

                            Marshal.WriteByte(base_addr, offset, color.B);
                            Marshal.WriteByte(base_addr, offset + 1, color.G);
                            Marshal.WriteByte(base_addr, offset + 2, color.R);
                            Marshal.WriteByte(base_addr, offset + 3, 255);
                        }
                    }
                }

                bitmap.UnlockBits(bitmap_data);
            }
            return bitmap;
        }
    }
}
