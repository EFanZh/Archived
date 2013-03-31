using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ClockDotNet
{
    internal class DefaultScene : Scene
    {
        private double margin = 0.1;

        private int calibrate_count = 4;
        private double[] calibrate_lengths = { 0.1, 0.05, 0.025, 0.0125 };
        private float[] calibrate_postions;
        private Color[] calibrate_pen_colors = { Color.Red, Color.White, Color.Orange, Color.Gray };
        private double[] calibrate_pen_widths = { 0.01, 0.005, 0.0025, 0.00125 };
        private Pen[] calibrate_pen_real;

        private double digit_location = 0.85;
        private float digit_location_real;
        private string digit_font_name = "Times New Roman";
        private double digit_font_size = 0.1;
        private Font digit_font;
        private Brush digit_brush = Brushes.White;

        private double time_location = 0.24;
        private float time_location_real;
        private string time_font_name = "Segoe UI";
        private double time_font_size = 0.12;
        private Font time_font;
        private Brush time_brush = Brushes.MediumOrchid;

        private int hand_count = 3;
        private double[] hand_widths = { 0.03, 0.03, 0.03 };
        private double[] hand_long_lengths = { 0.64, 0.8, 0.96 };
        private double[] hand_short_lengths = { 0.1, 0.1, 0.1 };
        private PointF[][] hand_points;
        private Brush[] hand_brushes = { Brushes.Green, Brushes.Blue, Brushes.Red };

        private double center_radius = 0.005;
        private float center_radius_real;
        private Brush center_brush = Brushes.Yellow;

        private StringFormat string_format = new StringFormat() { Alignment = StringAlignment.Center };

        private Matrix transform_1, transform_2;

        private double radius;

        public DefaultScene()
        {
            this.SizeChanged += (sender, e) =>
            {
                float x_center = Size.Width / 2.0f;
                float y_center = Size.Height / 2.0f;
                radius = Math.Min(x_center, y_center) * (1.0 - margin);

                calibrate_postions = new float[calibrate_count];
                calibrate_pen_real = new Pen[calibrate_count];
                for (int i = 0; i < calibrate_count; i++)
                {
                    calibrate_postions[i] = (float)((1.0 - calibrate_lengths[i]) * radius);
                    calibrate_pen_real[i] = new Pen(calibrate_pen_colors[i], (float)(calibrate_pen_widths[i] * radius));
                }

                digit_location_real = (float)-(digit_location * radius);
                digit_font = new Font(digit_font_name, (float)(digit_font_size * radius));

                time_location_real = (float)(time_location * radius);
                time_font = new Font(time_font_name, (float)(time_font_size * radius));

                hand_points = new PointF[hand_count][];
                for (int i = 0; i < hand_count; i++)
                {
                    hand_points[i] = new PointF[]
                    {
                        new PointF(0.0f, (float)-(hand_short_lengths[i] * radius)),
                        new PointF((float)-(hand_widths[i] * radius / 2.0), 0.0f),
                        new PointF(0.0f, (float)(hand_long_lengths[i] * radius)),
                        new PointF((float)(hand_widths[i] * radius / 2.0), 0.0f)
                    };
                }

                center_radius_real = (float)(radius * center_radius);

                transform_1 = new Matrix();
                transform_1.Translate(x_center, y_center);
                transform_2 = transform_1.Clone();
                transform_1.Rotate(30);
                transform_2.Rotate(180);
            };
        }

        private void SetGraphicsQuality(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        protected override void DoDrawBackground(Graphics graphics)
        {
            graphics.Clear(Color.Black);
            graphics.Transform = transform_1;

            Func<int, Action> get_draw_calibrate_function = (index) =>
            {
                float radius_f = (float)radius;
                return () =>
                {
                    graphics.DrawLine(calibrate_pen_real[index], 0.0f, radius_f, 0.0f, calibrate_postions[index]);
                    graphics.RotateTransform(0.6f);
                };
            };
            Action[] draw_calibrate = new Action[calibrate_count];
            for (int i = 0; i < calibrate_count; i++)
            {
                draw_calibrate[i] = get_draw_calibrate_function(i);
            }
            Action draw_calibrates_3 = () =>
            {
                for (int i = 0; i < 4; i++)
                {
                    draw_calibrate[3]();
                }
            };
            Action draw_calibrates_2 = () =>
            {
                draw_calibrates_3();
                draw_calibrate[2]();
                draw_calibrates_3();
            };
            SetGraphicsQuality(graphics);

            for (int i = 1; i <= 12; i++)
            {
                graphics.DrawString(i.ToString(), digit_font, digit_brush, 0.0f, digit_location_real, string_format);
                draw_calibrate[0]();
                draw_calibrates_2();
                for (int j = 0; j < 4; j++)
                {
                    draw_calibrate[1]();
                    draw_calibrates_2();
                }
            }
        }

        protected override void DoDrawForeground(Graphics graphics)
        {
            DateTime now = DateTime.Now;

            double second = 1000 * now.Second + now.Millisecond;
            double minute = 60000 * now.Minute + second;
            double hour = 3600000 * now.Hour + minute;

            graphics.Clear(Color.FromArgb(0, 0, 0, 0));

            SetGraphicsQuality(graphics);

            graphics.Transform = transform_2;
            graphics.RotateTransform(180.0f);
            graphics.DrawString(now.ToShortTimeString(), time_font, time_brush, 0.0f, time_location_real, string_format);
            graphics.Transform = transform_2;
            graphics.RotateTransform((float)(hour * (30.0 / 3600000)));
            graphics.FillPolygon(hand_brushes[0], hand_points[0]);
            graphics.Transform = transform_2;
            graphics.RotateTransform((float)(minute * (6.0 / 60000)));
            graphics.FillPolygon(hand_brushes[1], hand_points[1]);
            graphics.Transform = transform_2;
            graphics.RotateTransform((float)(second * (6.0 / 1000)));
            graphics.FillPolygon(hand_brushes[2], hand_points[2]);

            graphics.FillEllipse(center_brush, -center_radius_real, -center_radius_real, center_radius_real + center_radius_real, center_radius_real + center_radius_real);
        }

        public override void Dispose()
        {
            if (digit_font != null)
            {
                digit_font.Dispose();
            }
            if (time_font != null)
            {
                time_font.Dispose();
            }
            if (transform_1 != null)
            {
                transform_1.Dispose();
            }
            if (transform_2 != null)
            {
                transform_2.Dispose();
            }
        }
    }
}
