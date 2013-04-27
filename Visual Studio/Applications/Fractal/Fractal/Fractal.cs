using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fractal
{
    internal abstract class Fractal
    {
        private Color fractal_color_from, fractal_color_to;
        private int fractal_max_level;

        public Size Size
        {
            get;
            set;
        }

        public Color ColorFrom
        {
            get
            {
                return fractal_color_from;
            }
            set
            {
                fractal_color_from = value;
                UpdateLevelColorCache();
            }
        }

        public Color ColorTo
        {
            get
            {
                return fractal_color_to;
            }
            set
            {
                fractal_color_to = value;
                UpdateLevelColorCache();
            }
        }

        public int MaxLevel
        {
            get
            {
                return fractal_max_level;
            }
            set
            {
                fractal_max_level = value;
                UpdateLevelColorCache();
            }
        }

        protected Color[] LevelColor
        {
            get;
            set;
        }

        protected abstract void DoDraw(Graphics graphics);

        private void UpdateLevelColorCache()
        {
            LevelColor = new Color[MaxLevel];

            if (LevelColor.Length > 0)
            {
                LevelColor[0] = ColorFrom;
            }
            for (int i = 1; i < MaxLevel; i++)
            {
                double p = i / (MaxLevel - 1.0);
                LevelColor[i] = GetInterpolationColor(p);
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

        protected Color GetInterpolationColor(double p)
        {
            return Color.FromArgb((int)(ColorFrom.A + p * (ColorTo.A - ColorFrom.A)), (int)(ColorFrom.R + p * (ColorTo.R - ColorFrom.R)), (int)(ColorFrom.G + p * (ColorTo.G - ColorFrom.G)), (int)(ColorFrom.B + p * (ColorTo.B - ColorFrom.B)));
        }
    }
}
