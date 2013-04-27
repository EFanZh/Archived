using System;
using System.Drawing;

namespace Ellipse
{
    internal class Program
    {
        private static int RoundToInt(double x)
        {
            return (int)Math.Round(x);
        }

        private static void Main(string[] args)
        {
            int width = 12, height = 12, grid_width = 256, grid_height = 256;
            Color background_color = Color.FromArgb(0, Color.Black), color = Color.Black;
            bool dither = false;
            string save_path = "D:\\2.png";

            long total_grid = grid_width * grid_height;
            long color_a_d = color.A - background_color.A;
            long color_r_d = color.R - background_color.R;
            long color_g_d = color.G - background_color.G;
            long color_b_d = color.B - background_color.B;
            Func<long, Color> get_color = sum =>
            {
                double p = (double)sum / total_grid;
                return Color.FromArgb(RoundToInt(background_color.A + p * color_a_d), RoundToInt(background_color.R + p * color_r_d), RoundToInt(background_color.G + p * color_g_d), RoundToInt(background_color.B + p * color_b_d));
            };

            // Process pixels.
            double w_2 = width * width, h_2 = height * height;
            int grid_step_x = grid_width * 2;
            int grid_step_y = grid_height * 2;
            long total_center_x = width * grid_width, total_center_y = height * grid_height;
            double a_2 = total_center_x * total_center_x, b_2 = total_center_y * total_center_y, ab_x2 = total_center_x * total_center_y * 2;
            total_center_x--;
            total_center_y--;
            Bitmap bitmap = new Bitmap(width, height);
            for (int y = 0, grid_y_start = 0, grid_y_end = grid_step_y; y < height; y++, grid_y_start += grid_step_y, grid_y_end += grid_step_y)
            {
                Console.WriteLine(y);

                int off_y = 2 * y - height < 0 ? 0 : 1;
                for (int x = 0, grid_x_start = 0, grid_x_end = grid_step_x; x < width; x++, grid_x_start += grid_step_x, grid_x_end += grid_step_x)
                {
                    int off_x = 2 * x - width < 0 ? 0 : 1;

                    // (x, y) is all in circle.
                    long dx = 2 * (x + off_x) - width;
                    long dy = 2 * (y + off_y) - height;
                    if (dx * dx / w_2 + dy * dy / h_2 < 1.0)
                    {
                        bitmap.SetPixel(x, y, color);
                        continue;
                    }

                    // If polong (x, y) is all out of circle.
                    dx = 2 * (x + 1 - off_x) - width;
                    dy = 2 * (y + 1 - off_y) - height;
                    if (dx * dx / w_2 + dy * dy / h_2 > 1.0)
                    {
                        bitmap.SetPixel(x, y, background_color);
                        continue;
                    }

                    long sum = 0;
                    for (long grid_x = grid_x_start; grid_x < grid_x_end; grid_x += 2)
                    {
                        for (long grid_y = grid_y_start; grid_y < grid_y_end; grid_y += 2)
                        {
                            dx = grid_x - total_center_x;
                            dy = grid_y - total_center_y;
                            if (dx * dx / a_2 + dy * dy / b_2 < 1.0)
                            {
                                sum++;
                            }
                        }
                    }
                    bitmap.SetPixel(x, y, get_color(sum));
                }
            }

            bitmap.Save(save_path);
        }
    }
}
