using System;
using System.Windows.Forms;

namespace PartitionSizeCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBoxGb_TextChanged(object sender, EventArgs e)
        {
            decimal value;

            if (decimal.TryParse(textBoxGb.Text, out value))
            {
                if (value < 0)
                {
                    value = 0;
                    textBoxGb.Text = value.ToString();
                }
                try
                {
                    textBoxMbNtfs1.Text = Math.Ceiling(Math.Ceiling(value * 2097152 / 16065) * 16065 / 2048).ToString();
                    textBoxMbFat32.Text = (1028 * value - 4).ToString();
                }
                catch (Exception)
                {
                }
            }
        }

        private void textBoxGb_KeyDown(object sender, KeyEventArgs e)
        {
            decimal value;

            if (decimal.TryParse(textBoxGb.Text, out value))
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        textBoxGb.Text = (Math.Floor(value) + 1).ToString();
                        break;

                    case Keys.Down:
                        textBoxGb.Text = (Math.Ceiling(value) - 1).ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
