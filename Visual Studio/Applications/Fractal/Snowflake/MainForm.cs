using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snowflake
{
    public partial class MainForm : Form
    {
        private SnowflakeScene scene = new SnowflakeScene();

        public MainForm()
        {
            InitializeComponent();
            scene.ColorFrom = Color.White;
            scene.ColorTo = Color.FromArgb(16, Color.SkyBlue);
            scene.Count = 6;
            scene.MaxLevel = 6;
            scene.Scale = 1.0 / 3.0;
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
    }
}
