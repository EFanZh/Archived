using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierFitting
{
    public partial class MainForm : Form
    {
        private Scene scene = new Scene();
        private int capture = 0;
        private Point mouse_start;
        private PointD point_start;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBoxAddPoint.Checked)
            {
                scene.DataPoints.Add(new PointD(e.X, e.Y));
                this.Invalidate();
                return;
            }

            mouse_start = e.Location;
            this.Text = mouse_start.ToString();
            capture = scene.HitTest(e.X, e.Y);
            switch (capture)
            {
                case 1:
                    point_start = scene.CurrentBezier.Point1;
                    break;

                case 2:
                    point_start = scene.CurrentBezier.Point2;
                    break;

                case 3:
                    point_start = scene.CurrentBezier.Point3;
                    break;

                case 4:
                    point_start = scene.CurrentBezier.Point4;
                    break;

                default:
                    break;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkBoxAddPoint.Checked)
            {
                return;
            }

            int offset_x = e.X - mouse_start.X;
            int offset_y = e.Y - mouse_start.Y;
            if (capture != 0 || scene.HitTest(e.X, e.Y) != 0)
            {
                this.Cursor = Cursors.SizeAll;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }

            if (capture == 1)
            {
                scene.SetPoint1(point_start.X + offset_x, point_start.Y + offset_y);
                this.Invalidate();
            }
            else if (capture == 2)
            {
                scene.SetPoint2(point_start.X + offset_x, point_start.Y + offset_y);
                this.Invalidate();
            }
            else if (capture == 3)
            {
                scene.SetPoint3(point_start.X + offset_x, point_start.Y + offset_y);
                this.Invalidate();
            }
            else if (capture == 4)
            {
                scene.SetPoint4(point_start.X + offset_x, point_start.Y + offset_y);
                this.Invalidate();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBoxAddPoint.Checked)
            {
                return;
            }

            capture = 0;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            scene.Render(e.Graphics);
        }
    }
}
