using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LCDNumbers
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.TranslateTransform((float)((this.ClientSize.Width - trackBar1.Value) / 2.0), (float)((this.ClientSize.Height - trackBar2.Value) / 2.0));

            LCDScene.Render(e.Graphics, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
