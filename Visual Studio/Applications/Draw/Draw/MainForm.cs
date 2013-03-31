using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw
{
    public partial class MainForm : Form
    {
        private Line line1 = new Line();
        private Scene scene = new Scene();

        public MainForm()
        {
            InitializeComponent();

            line1.Point1 = new PointF(10, 10);
            line1.Point2 = new PointF(100, 100);
            line1.Stroke = new Pen(Color.Green, 4.0f);
            scene.Shapes.Add(line1);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            scene.Render(e.Graphics);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                line1.Point2 = e.Location;
            }
            this.Invalidate();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            line1.Point1 = e.Location;
            this.Invalidate();
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.Invalidate();
        }
    }
}
