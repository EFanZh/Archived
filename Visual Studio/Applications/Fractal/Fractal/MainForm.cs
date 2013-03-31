using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Fractal
{
    public partial class MainForm : Form
    {
        private FractalTree tree = new FractalTree();

        public MainForm()
        {
            InitializeComponent();

            EventHandler update = (sender, e) =>
            {
                tree.MaxLevel = trackBar1.Value;
                tree.OffsetAngle = (double)(trackBar2.Value - trackBar2.Minimum) / trackBar2.Maximum * 2.0 * Math.PI;
                tree.Scale1 = (double)(trackBar3.Value - trackBar3.Minimum + 1) / (trackBar3.Maximum - trackBar3.Minimum + 2);
                tree.Scale2 = (double)(trackBar4.Value - trackBar4.Minimum + 1) / (trackBar4.Maximum - trackBar4.Minimum + 2);
                tree.SplitAngle = (double)(trackBar5.Value - trackBar5.Minimum) / trackBar5.Maximum * 2.0 * Math.PI;

                this.Invalidate();
            };

            update(null, null);

            trackBar1.Scroll += update;
            trackBar2.Scroll += update;
            trackBar3.Scroll += update;
            trackBar4.Scroll += update;
            trackBar5.Scroll += update;

            tree.ColorFrom = Color.FromArgb(192, 64, 32);
            tree.ColorTo = Color.FromArgb(32, Color.ForestGreen);
            tree.Size = this.ClientSize;

            //Export();
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            tree.Size = this.ClientSize;
            this.Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            tree.Render(e.Graphics);
        }

        private void Export()
        {
            int max_level = tree.MaxLevel;
            Size size0 = tree.Size;

            Bitmap bitmap = new Bitmap(10000, 10000);
            tree.MaxLevel = 1000;
            tree.Size = bitmap.Size;
            tree.Render(Graphics.FromImage(bitmap));
            bitmap.Save("D:\\tree.png");
            bitmap.Dispose();

            Process.Start("D:\\tree.png");
            tree.MaxLevel = max_level;
            tree.Size = size0;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Export();
            sw.Stop();
            MessageBox.Show(string.Format("Done!\r\nTime: {0}", sw.Elapsed));
        }
    }
}
