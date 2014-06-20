using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWindows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pageSetupDialog1.ShowDialog().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(printDialog1.ShowDialog().ToString());
        }
    }
}
