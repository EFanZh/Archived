using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowflake
{
    internal class SnowflakeScene : FractalScene
    {
        private int snowflake_scene_count;
        private double line_angle;

        public int Count
        {
            get
            {
                return snowflake_scene_count;
            }
            set
            {
                snowflake_scene_count = value;
                line_angle = 2.0 * Math.PI / snowflake_scene_count;
            }
        }

        protected override double DoGetBaseSize()
        {
            return Math.Min(this.Size.Width, this.Size.Height) / 2.0;
        }

        protected override double DoGetPenWidthFactor()
        {
            return 0.02;
        }

        protected override void DoDraw(System.Drawing.Graphics graphics)
        {
            double x = Size.Width / 2.0f;
            double y = Size.Height / 2.0f;

            double angle = 0.0;
            for (int i = 0; i < Count; i++)
            {
                DrawSnowflake(graphics, x, y, angle, 0);
                angle += line_angle;
            }
        }

        private void DrawSnowflake(Graphics graphics, double x, double y, double angle, int level)
        {
            double length = this.LevelLineLength[level];
            double end_x = x + length * Math.Cos(angle);
            double end_y = y + length * Math.Sin(angle);
            this.DrawFractalLine(graphics, x, y, end_x, end_y, level);
            level++;
            if (level < MaxLevel)
            {
                double next_angle = angle - Math.PI + line_angle;
                for (int i = 0; i < Count - 1; i++)
                {
                    DrawSnowflake(graphics, end_x, end_y, next_angle, level);
                    next_angle += line_angle;
                }
            }
        }
    }
}
