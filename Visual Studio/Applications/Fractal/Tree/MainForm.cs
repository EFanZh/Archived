using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tree
{
    public partial class MainForm : Form
    {
        private TreeScene scene = new TreeScene();

        public MainForm()
        {
            InitializeComponent();
            scene.ColorFrom = Color.FromArgb(255, Color.Red);
            scene.ColorTo = Color.FromArgb(16, Color.ForestGreen);
            scene.MaxLevel = 32;
            scene.OffsetAngle = 0.05;
            scene.Scale = 0.84;
            scene.Size = this.ClientSize;
            scene.SplitAngle = Math.PI / 7;

            // Export();
            scene.Size = this.ClientSize;
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            scene.Size = this.ClientSize;
            this.Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            scene.Render(e.Graphics);
        }

        private void Export()
        {
            Bitmap bitmap = new Bitmap(10000, 9600);
            scene.Size = bitmap.Size;
            scene.Render(Graphics.FromImage(bitmap));
            bitmap.Save("D:\\1.png");
            bitmap.Dispose();
        }
    }
}
