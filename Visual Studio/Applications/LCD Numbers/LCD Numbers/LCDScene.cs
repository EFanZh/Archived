using System;
using System.Drawing;

namespace LCDNumbers
{
    internal static class LCDScene
    {
        private static readonly double sqrt_2 = Math.Sqrt(2.0);
        private static readonly Brush brush = new SolidBrush(Color.FromArgb(97, Color.BlueViolet));

        public static void Render(Graphics graphics, double width, double height, double part_width, double part_sep)
        {
            double part_width_half = part_width / 2.0;
            double part_sep_size = part_sep / sqrt_2;
            double part_width_half_plus_part_sep_size = part_width_half + part_sep_size;
            double part_width_plus_part_sep_size = part_width + part_sep_size;
            double height_half = height / 2.0;

            float f_height = (float)height;
            float f_part_width = (float)part_width;
            float f_width = (float)width;

            float f_height_half = (float)height_half;
            float f_part_width_half = (float)part_width_half;
            float f_part_width_half_plus_part_sep_size = (float)part_width_half_plus_part_sep_size;
            float f_part_width_plus_part_sep_size = (float)part_width_plus_part_sep_size;

            float height_half_minus_part_sep_size = (float)(height_half - part_sep_size);
            float height_half_minus_part_width_half = (float)(height_half - part_width_half);
            float height_half_minus_part_width_half_plus_part_sep_size = (float)(height_half - part_width_half_plus_part_sep_size);
            float height_half_plus_part_sep_size = (float)(height_half + part_sep_size);
            float height_half_plus_part_width_half = (float)(height_half + part_width_half);
            float height_half_plus_part_width_half_plus_part_sep_size = (float)(height_half + part_width_half_plus_part_sep_size);
            float height_minus_part_width = (float)(height - part_width);
            float height_minus_part_width_half = (float)(height - part_width_half);
            float height_minus_part_width_half_plus_part_sep_size = (float)(height - part_width_half_plus_part_sep_size);
            float height_minus_part_width_plus_part_sep_size = (float)(height - part_width_plus_part_sep_size);
            float width_minus_part_width = (float)(width - part_width);
            float width_minus_part_width_half = (float)(width - part_width_half);
            float width_minus_part_width_half_plus_part_sep_size = (float)(width - part_width_half_plus_part_sep_size);
            float width_minus_part_width_plus_part_sep_size = (float)(width - part_width_plus_part_sep_size);

            graphics.FillPolygon(brush, new[]
            {
                new PointF(f_part_width_half_plus_part_sep_size, f_part_width_half),
                new PointF(f_part_width_plus_part_sep_size, 0.0f),
                new PointF(width_minus_part_width_plus_part_sep_size, 0.0f),
                new PointF(width_minus_part_width_half_plus_part_sep_size, f_part_width_half),
                new PointF(width_minus_part_width_plus_part_sep_size, f_part_width),
                new PointF(f_part_width_plus_part_sep_size, f_part_width)
            });

            graphics.FillPolygon(brush, new[]
            {
                new PointF(f_part_width_half, f_part_width_half_plus_part_sep_size),
                new PointF(f_part_width, f_part_width_plus_part_sep_size),
                new PointF(f_part_width, height_half_minus_part_width_half_plus_part_sep_size),
                new PointF(f_part_width_half, height_half_minus_part_sep_size),
                new PointF(0.0f, height_half_minus_part_width_half_plus_part_sep_size),
                new PointF(0.0f, f_part_width_plus_part_sep_size)
            });

            graphics.FillPolygon(brush, new[]
            {
                new PointF(width_minus_part_width_half, f_part_width_half_plus_part_sep_size),
                new PointF(f_width, f_part_width_plus_part_sep_size),
                new PointF(f_width, height_half_minus_part_width_half_plus_part_sep_size),
                new PointF(width_minus_part_width_half, height_half_minus_part_sep_size),
                new PointF(width_minus_part_width, height_half_minus_part_width_half_plus_part_sep_size),
                new PointF(width_minus_part_width, f_part_width_plus_part_sep_size)
            });

            graphics.FillPolygon(brush, new[]
            {
                new PointF(f_part_width_half_plus_part_sep_size, f_height_half),
                new PointF(f_part_width_plus_part_sep_size, height_half_minus_part_width_half),
                new PointF(width_minus_part_width_plus_part_sep_size, height_half_minus_part_width_half),
                new PointF(width_minus_part_width_half_plus_part_sep_size, f_height_half),
                new PointF(width_minus_part_width_plus_part_sep_size, height_half_plus_part_width_half),
                new PointF(f_part_width_plus_part_sep_size, height_half_plus_part_width_half)
            });

            graphics.FillPolygon(brush, new[]
            {
                new PointF(f_part_width_half, height_half_plus_part_sep_size),
                new PointF(f_part_width, height_half_plus_part_width_half_plus_part_sep_size),
                new PointF(f_part_width, height_minus_part_width_plus_part_sep_size),
                new PointF(f_part_width_half, height_minus_part_width_half_plus_part_sep_size),
                new PointF(0.0f, height_minus_part_width_plus_part_sep_size),
                new PointF(0.0f, height_half_plus_part_width_half_plus_part_sep_size)
            });

            graphics.FillPolygon(brush, new[]
            {
                new PointF(width_minus_part_width_half, height_half_plus_part_sep_size),
                new PointF(f_width, height_half_plus_part_width_half_plus_part_sep_size),
                new PointF(f_width, height_minus_part_width_plus_part_sep_size),
                new PointF(width_minus_part_width_half, height_minus_part_width_half_plus_part_sep_size),
                new PointF(width_minus_part_width, height_minus_part_width_plus_part_sep_size),
                new PointF(width_minus_part_width, height_half_plus_part_width_half_plus_part_sep_size)
            });

            graphics.FillPolygon(brush, new[]
            {
                new PointF(f_part_width_half_plus_part_sep_size, height_minus_part_width_half),
                new PointF(f_part_width_plus_part_sep_size, height_minus_part_width),
                new PointF(width_minus_part_width_plus_part_sep_size, height_minus_part_width),
                new PointF(width_minus_part_width_half_plus_part_sep_size, height_minus_part_width_half),
                new PointF(width_minus_part_width_plus_part_sep_size, f_height),
                new PointF(f_part_width_plus_part_sep_size, f_height)
            });
        }
    }
}
