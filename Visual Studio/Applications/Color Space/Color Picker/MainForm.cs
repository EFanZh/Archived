using System;
using System.Windows.Forms;

namespace ColorPicker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void trackBarY_Scroll(object sender, EventArgs e)
        {
            canvasMain.Y = (double)trackBarY.Value / trackBarY.Maximum;
        }
    }
}
