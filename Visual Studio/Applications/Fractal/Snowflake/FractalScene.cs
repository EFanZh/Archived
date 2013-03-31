using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Snowflake
{
    internal abstract class FractalScene
    {
        private Size scene_size;
        private double scene_scale;
        private Color scene_color_from;
        private Color scene_color_to;
        private int scene_max_level;

        private double[] level_line_length;
        private Pen[] level_pen;

        private double base_line_length;

        public Size Size
        {
            get
            {
                return scene_size;
            }
            set
            {
                scene_size = value;
                UpdateLevelLineLengthCache();
            }
        }

        public double Scale
        {
            get
            {
                return scene_scale;
            }
            set
            {
                scene_scale = value;
                UpdateLevelLineLengthCache();
            }
        }

        public Color ColorFrom
        {
            get
            {
                return scene_color_from;
            }
            set
            {
                scene_color_from = value;
                UpdateLevelPenCache();
            }
        }

        public Color ColorTo
        {
            get
            {
                return scene_color_to;
            }
            set
            {
                scene_color_to = value;
                UpdateLevelPenCache();
            }
        }

        public int MaxLevel
        {
            get
            {
                return scene_max_level;
            }
            set
            {
                scene_max_level = value;
                UpdateLevelLineLengthCache();
            }
        }

        protected double[] LevelLineLength
        {
            get
            {
                return level_line_length;
            }
        }

        protected Pen[] LevelPen
        {
            get
            {
                return level_pen;
            }
        }

        protected abstract double DoGetBaseSize();

        protected abstract double DoGetPenWidthFactor();

        protected abstract void DoDraw(Graphics graphics);

        private void UpdateLevelLineLengthCache()
        {
            level_line_length = new double[MaxLevel];

            base_line_length = DoGetBaseSize() * (1.0 - scene_scale);
            UpdateLevelPenCache();
            for (int i = 0; i < scene_max_level; i++)
            {
                level_line_length[i] = base_line_length * Math.Pow(Scale, i);
            }
        }

        private void UpdateLevelPenCache()
        {
            level_pen = new Pen[MaxLevel];

            double base_pen_width = base_line_length * DoGetPenWidthFactor();
            if (level_pen.Length > 0)
            {
                level_pen[0] = new Pen(ColorFrom, (float)base_pen_width);
            }
            for (int i = 1; i < scene_max_level; i++)
            {
                double p = i / (scene_max_level - 1.0);
                level_pen[i] = new Pen(Color.FromArgb((int)(ColorFrom.A + p * (ColorTo.A - ColorFrom.A)), (int)(ColorFrom.R + p * (ColorTo.R - ColorFrom.R)), (int)(ColorFrom.G + p * (ColorTo.G - ColorFrom.G)), (int)(ColorFrom.B + p * (ColorTo.B - ColorFrom.B))), (float)(base_pen_width * Math.Pow(Scale, i)));
            }
        }

        public void Render(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            graphics.Clear(Color.Black);

            DoDraw(graphics);
        }

        protected void DrawFractalLine(Graphics graphics, double x1, double y1, double x2, double y2, int level)
        {
            graphics.DrawLine(level_pen[level], (float)x1, (float)y1, (float)x2, (float)y2);
        }
    }
}
