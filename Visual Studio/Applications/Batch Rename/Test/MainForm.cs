using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            var margins = new NativeMethods.MARGINS() { cxLeftWidth = -1 };
            NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            const int nx = 16, ny = 16;
            const float sps_x = 10.0f, sps_y = 10.0f;

            RectangleF rectf = new RectangleF();
            rectf.Width = (this.ClientSize.Width - (nx - 1) * sps_x) / nx;
            rectf.Height = (this.ClientSize.Height - (ny - 1) * sps_y) / ny;

            g.Clear(Color.FromArgb(128, Color.Red));
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    rectf.X = i * (rectf.Width + sps_x);
                    rectf.Y = j * (rectf.Height + sps_y);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255 * j / (ny - 1), Color.FromArgb(0, 255 * (15 - i) / (nx - 1), 255 * i / (nx - 1)))), rectf);
                }
            }
            g.FillRectangle(new SolidBrush(Color.Black), 100, 100, 200, 200);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
