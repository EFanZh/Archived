using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            MessageBox.Show(this.GetStyle(ControlStyles.UserPaint).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1920, 1080);

            Graphics g = Graphics.FromImage(bmp);
            Size size = new Size(1920, 1080);
            for (int i = 0; i < 2000; i++)
            {
                g.CopyFromScreen(0, 0, 0, 0, size, CopyPixelOperation.SourceCopy);
            }
        }
    }
}
