using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWindows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() => Thread.Sleep(5000));
            label1.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }
}
