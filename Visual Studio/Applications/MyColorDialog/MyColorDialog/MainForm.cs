using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyColorDialog
{
    public partial class MainForm : Form
    {
        private MyColorDialog c = new MyColorDialog();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            c.ShowDialog();
        }
    }
}
