using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GravityDistribution
{
    public partial class MainForm : Form
    {
        private const float radius = 320;
        private const int num = 60;
        private Pen pen_earth = new Pen(Color.LightBlue, 1.0f);
        private Pen pen_center = new Pen(Color.FromArgb(128, Color.DimGray), 1.0f);
        private Pen pen_gravititon = new Pen(Color.Red, 1.0f);
        private Pen pen_cent_force = new Pen(Color.Green, 1.0f);
        private Pen pen_gravity = new Pen(Color.Blue, 1.0f);
        private const double gravition = 100.0;
        private const double omega = 0.4;
        private Point pt_mouse;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            PointF pt_center = new PointF(this.ClientSize.Width / 2.0f, this.ClientSize.Height / 2.0f);

            // Draw circle
            g.DrawEllipse(pen_earth, pt_center.X - radius, pt_center.Y - radius, radius * 2, radius * 2);

            // Draw this rest

            double cent_force_a = omega * omega * radius;
            for (int i = 0; i < num; i++)
            {
                double angle = 2 * Math.PI * i / num;
                DrawLineAngle(g, pt_center, angle, cent_force_a);
            }

            if (!pt_mouse.IsEmpty)
            {
                DrawLineAngle(g, pt_center, Math.Atan2(pt_mouse.Y - pt_center.Y, pt_mouse.X - pt_center.X), cent_force_a);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            this.Invalidate();
            pt_mouse = e.Location;
        }

        private void DrawLineAngle(Graphics g, PointF pt_center, double angle, double cent_force_a)
        {
            double cos_angle = Math.Cos(angle);
            double sin_angle = Math.Sin(angle);
            PointF pt_end = new PointF((float)(pt_center.X + cos_angle * radius), (float)(pt_center.Y + sin_angle * radius));

            // Draw line
            g.DrawLine(pen_center, pt_center.X, pt_center.Y, pt_end.X, pt_end.Y);
            double grav_x = cos_angle * gravition;
            double grav_y = sin_angle * gravition;
            double cent_force = cent_force_a * cos_angle;
            g.DrawLine(pen_gravititon, pt_end.X, pt_end.Y, (float)(pt_end.X + grav_x), (float)(pt_end.Y + grav_y));
            g.DrawLine(pen_cent_force, pt_end.X, pt_end.Y, (float)(pt_end.X + cent_force), pt_end.Y);
            g.DrawLine(pen_gravity, pt_end.X, pt_end.Y, (float)(pt_end.X + grav_x - cent_force), (float)(pt_end.Y + grav_y));
        }
    }
}
