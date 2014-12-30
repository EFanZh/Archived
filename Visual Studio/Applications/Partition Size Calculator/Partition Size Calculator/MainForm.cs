using System;
using System.Globalization;
using System.Windows.Forms;

namespace PartitionSizeCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBoxSource_TextChanged(object sender, EventArgs e)
        {
            decimal source;

            if (decimal.TryParse(textBoxSource.Text, out source))
            {
                try
                {
                    decimal kb = 1024 * 1024 * source + 4;
                    decimal mb = Math.Ceiling(kb / 1024);

                    textBoxSystem.Text = mb.ToString("N0");
                    textBoxPrecise.Text = kb.ToString("N0");
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
