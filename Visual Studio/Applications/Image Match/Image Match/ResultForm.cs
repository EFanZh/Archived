using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageMatch
{
    public partial class ResultForm : Form
    {
        public ResultForm(Image img)
        {
            InitializeComponent();

            pctrBxMain.Image = img;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (svFlDlgMain.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pctrBxMain.Image.Save(svFlDlgMain.FileName, ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
