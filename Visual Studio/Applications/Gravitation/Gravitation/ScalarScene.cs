using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gravitation
{
    internal class ScalarScene : Scene
    {
        public Color ColorationColor
        {
            get;
            set;
        }

        public override void Render(Graphics graphics)
        {
            Bitmap bitmap = new Bitmap(Size.Width, Size.Height, PixelFormat.Format24bppRgb);
            BitmapData bitmap_data = bitmap.LockBits(new Rectangle(Point.Empty, Size), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int[] x_offset_buffer = new int[Size.Width];
            int[] y_offset_buffer = new int[Size.Height];

            for (int i = 0, j = 0; i < Size.Width; i++, j += 3)
            {
                x_offset_buffer[i] = j;
            }

            for (int i = 0, j = 0; i < Size.Height; i++, j += bitmap_data.Stride)
            {
                y_offset_buffer[i] = j;
            }

            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    double gx = 0, gy = 0;
                    foreach (var mass_point in MassPoints)
                    {
                        float dx = x - mass_point.Item1.X;
                        float dy = y - mass_point.Item1.Y;
                        float d2 = dx * dx + dy * dy;
                        if (d2 == 0)
                        {
                            continue;
                        }
                        double d = Math.Sqrt(d2);
                        double t = GravitationalConstant * mass_point.Item2 / (d2 * d);
                        gx += t * dx;
                        gy += t * dy;
                    }

                    double g = Math.Sqrt(gx * gx + gy * gy);

                    if (g > MaxGravitation)
                    {
                        g = MaxGravitation;
                    }

                    double p = g / MaxGravitation;
                    double ca = p * ColorationColor.A, cr = p * ColorationColor.R, cg = p * ColorationColor.G, cb = p * ColorationColor.B;
                    p = ca / 255;
                    int offset = x_offset_buffer[x] + y_offset_buffer[y];
                    int r_offset = offset + 2;
                    int g_offset = offset + 1;
                    int b_offset = offset;
                    int r0 = Marshal.ReadByte(bitmap_data.Scan0, r_offset);
                    int g0 = Marshal.ReadByte(bitmap_data.Scan0, g_offset);
                    int b0 = Marshal.ReadByte(bitmap_data.Scan0, b_offset);
                    Marshal.WriteByte(bitmap_data.Scan0, r_offset, (byte)Dithering(cr * p + r0 * (1 - p)));
                    Marshal.WriteByte(bitmap_data.Scan0, g_offset, (byte)Dithering(cg * p + g0 * (1 - p)));
                    Marshal.WriteByte(bitmap_data.Scan0, b_offset, (byte)Dithering(cb * p + b0 * (1 - p)));
                }
            }

            bitmap.UnlockBits(bitmap_data);

            graphics.DrawImage(bitmap, 0, 0);
            bitmap.Dispose();
        }
    }
}
