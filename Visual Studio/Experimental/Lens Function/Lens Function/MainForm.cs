using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LensFunction
{
    public partial class MainForm : Form
    {
        private float[] ds;
        private float r = 200;
        // private double f = 40;
        // private double n = 1.5;

        public MainForm()
        {
            InitializeComponent();

            ds = Enumerable.Range(1, 200).Select(x => x / 16.0f).ToArray();
        }

        private void Calc(PointF p1, PointF p2, double y)
        {
            double x = (p2.X * p1.Y - p1.X * p2.Y + (p1.X - p2.X) * y) / (p1.Y - p2.Y);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            float w2 = this.ClientSize.Width / 2.0f, h2 = this.ClientSize.Height / 2.0f;
            Graphics g = e.Graphics;

            g.TranslateTransform(w2, h2);

            // Draw axis.
            g.DrawLine(Pens.Gray, -w2, 0, w2, 0);
            g.DrawLine(Pens.Gray, 0, -h2, 0, h2);

            var points_0 = ds.Select((x, i) => new PointF(x, i * r / (ds.Length - 1) - r)).ToArray();
            var points_right = points_0.Concat(from p in points_0.Reverse().Skip(1)
                                               select new PointF(p.X, -p.Y)).ToArray();
            g.DrawPolygon(Pens.Black, points_right.Concat(from p in points_right.Reverse()
                                                          select new PointF(-p.X, p.Y)).ToArray());
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
