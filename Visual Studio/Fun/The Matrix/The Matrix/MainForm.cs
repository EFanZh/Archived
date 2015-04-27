using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheMatrix
{
    public partial class MainForm : Form
    {
        private Scene scene;

        private Font font = new Font("Consolas", 9.0f);

        private SizeF char_size;

        public MainForm()
        {
            InitializeComponent();

            char_size = GetCharSize();
            this.ClientSize = new Size((int)Math.Ceiling(char_size.Width * 80), (int)Math.Ceiling(char_size.Height * 25));
            this.CenterToScreen();
            scene = new Scene(GetLogicalSize(), this.ClientSize, font, Color.Transparent, 5, 0.25, 24, 32, 1, 2, Color.FromArgb(210, 255, 220), Color.FromArgb(118, 238, 127), Color.FromArgb(0, 118, 238, 127), 0.1, 16);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            scene.Render(e.Graphics);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (scene != null)
            {
                scene.Size = this.ClientSize;
                scene.LogicalSize = GetLogicalSize();
                scene.NextFrame();
                this.Invalidate();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    scene.DisplayInfo = !scene.DisplayInfo;
                    break;

                case Keys.R:
                    scene.Rendering = !scene.Rendering;
                    break;
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            scene.NextFrame();
            this.Invalidate();
        }

        private SizeF GetCharSize()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                SizeF size1 = graphics.MeasureString("0", font);
                SizeF size2 = graphics.MeasureString("00\r\n00", font);
                return size2 - size1;
            }
        }

        private Size GetLogicalSize()
        {
            return new Size((int)(ClientSize.Width / char_size.Width), (int)(this.ClientSize.Height / char_size.Height));
        }
    }
}
