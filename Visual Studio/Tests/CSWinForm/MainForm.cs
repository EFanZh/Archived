using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
        }

        private async void Test()
        {
            var s = await Task<int>.Run(() =>
            {
                Thread.Sleep(2000);
                return 12;
            });
            this.Text = s.ToString();
        }
    }
}
