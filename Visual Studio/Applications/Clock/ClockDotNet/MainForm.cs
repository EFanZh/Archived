using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClockDotNet
{
    public partial class MainForm : Form
    {
        private Bitmap bitmap_background;
        private Bitmap bitmap_foreground;
        private Scene scene = new DefaultScene();

        public MainForm()
        {
            InitializeComponent();
            CreateBitmaps();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(bitmap_background, 0, 0);
            g.DrawImage(bitmap_foreground, 0, 0);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            CreateBitmaps();
            this.Invalidate();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            scene.DrawForeground(bitmap_foreground);
            this.Invalidate();
        }

        private void CreateBitmaps()
        {
            Size client_size = this.ClientSize;
            if (client_size.Width > 0 && client_size.Height > 0)
            {
                int width = client_size.Width, height = client_size.Height;
                scene.Size = client_size;
                Graphics this_graphics = this.CreateGraphics();
                bitmap_background = new Bitmap(client_size.Width, client_size.Height, this_graphics);
                bitmap_foreground = new Bitmap(client_size.Width, client_size.Height, this_graphics);
                scene.DrawBackground(bitmap_background);
                scene.DrawForeground(bitmap_foreground);
            }
        }
    }
}
