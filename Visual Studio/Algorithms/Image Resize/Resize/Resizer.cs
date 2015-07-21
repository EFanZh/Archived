using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Resize
{
    internal static class Resizer
    {
        private static Random random = new Random();

        public static Bitmap Resize(Bitmap source_bitmap, int new_width, int new_height, bool dither)
        {
            int grid_width = LCM(source_bitmap.Width, new_width);
            int grid_target_px_width = grid_width / new_width;
            int grid_source_px_width = grid_width / source_bitmap.Width;
            int grid_height = LCM(source_bitmap.Height, new_height);
            int grid_target_px_height = grid_height / new_height;
            int grid_source_px_height = grid_height / source_bitmap.Height;
            int grid_target_px_size = grid_target_px_width * grid_target_px_height;

            BitmapData source_bitmap_data = source_bitmap.LockBits(new Rectangle(0, 0, source_bitmap.Width, source_bitmap.Height), ImageLockMode.ReadOnly, source_bitmap.PixelFormat);

            // Create new bitmap.
            Bitmap new_bitmap = new Bitmap(new_width, new_height, source_bitmap.PixelFormat);
            new_bitmap.SetResolution(source_bitmap.HorizontalResolution, source_bitmap.VerticalResolution);
            BitmapData new_bitmap_data = new_bitmap.LockBits(new Rectangle(0, 0, new_width, new_height), ImageLockMode.WriteOnly, new_bitmap.PixelFormat);

            // Easier to get the get_offset function.
            Func<BitmapData, Func<int, int>> get_get_y_offset = bitmap_data =>
            {
                if (bitmap_data.Stride < 0)
                {
                    return y =>
                    {
                        return -bitmap_data.Stride * (bitmap_data.Height - y);
                    };
                }
                else
                {
                    return y =>
                    {
                        return bitmap_data.Stride * y;
                    };
                }
            };

            var get_source_y_offset = get_get_y_offset(source_bitmap_data);
            var get_new_y_offset = get_get_y_offset(new_bitmap_data);

            // Set to_int function.
            Func<double, int> to_int;
            if (dither)
            {
                to_int = x =>
                {
                    int xi = (int)x;
                    double xd = x - xi;
                    return random.NextDouble() < xd ? xi + 1 : xi;
                };
            }
            else
            {
                to_int = x =>
                {
                    return (int)Math.Round(x);
                };
            }

            // Set calc_helper.
            Action<int, int, Action<Action<int, int>>> calc_helper = null;
            switch (source_bitmap.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    calc_helper = (tx, ty, grid_calc) =>
                    {
                        int r = 0, g = 0, b = 0;
                        grid_calc((sx, sy) =>
                        {
                            int offset = get_source_y_offset(sy) + sx * 3;
                            b += Marshal.ReadByte(source_bitmap_data.Scan0, offset);
                            g += Marshal.ReadByte(source_bitmap_data.Scan0, offset + 1);
                            r += Marshal.ReadByte(source_bitmap_data.Scan0, offset + 2);
                        });
                        int offset_2 = get_new_y_offset(ty) + tx * 3;
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2, (byte)to_int((double)b / grid_target_px_size));
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2 + 1, (byte)to_int((double)g / grid_target_px_size));
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2 + 2, (byte)to_int((double)r / grid_target_px_size));
                    };
                    break;

                case PixelFormat.Format32bppArgb:
                    calc_helper = (tx, ty, grid_calc) =>
                    {
                        int a = 0, r = 0, g = 0, b = 0;
                        grid_calc((sx, sy) =>
                        {
                            int offset = get_source_y_offset(sy) + sx * 4;
                            b += Marshal.ReadByte(source_bitmap_data.Scan0, offset);
                            g += Marshal.ReadByte(source_bitmap_data.Scan0, offset + 1);
                            r += Marshal.ReadByte(source_bitmap_data.Scan0, offset + 2);
                            a += Marshal.ReadByte(source_bitmap_data.Scan0, offset + 3);
                        });
                        int offset_2 = get_new_y_offset(ty) + tx * 4;
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2, (byte)to_int((double)b / grid_target_px_size));
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2 + 1, (byte)to_int((double)g / grid_target_px_size));
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2 + 2, (byte)to_int((double)r / grid_target_px_size));
                        Marshal.WriteByte(new_bitmap_data.Scan0, offset_2 + 3, (byte)to_int((double)a / grid_target_px_size));
                    };
                    break;

                default:
                    throw new ArgumentException();
            }

            for (int tx = 0, gx_start = 0, gx_end = grid_target_px_width; tx < new_width; tx++, gx_start += grid_target_px_width, gx_end += grid_target_px_width)
            {
                for (int ty = 0, gy_start = 0, gy_end = grid_target_px_height; ty < new_height; ty++, gy_start += grid_target_px_height, gy_end += grid_target_px_height)
                {
                    calc_helper(tx, ty, add_info =>
                    {
                        for (int gx = gx_start; gx < gx_end; gx++)
                        {
                            for (int gy = gy_start; gy < gy_end; gy++)
                            {
                                add_info(gx / grid_source_px_width, gy / grid_source_px_height);
                            }
                        }
                    });
                }
            }

            new_bitmap.UnlockBits(new_bitmap_data);
            source_bitmap.UnlockBits(source_bitmap_data);

            return new_bitmap;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        private static int LCM(int a, int b)
        {
            return a * b / GCD(a, b);
        }
    }
}
