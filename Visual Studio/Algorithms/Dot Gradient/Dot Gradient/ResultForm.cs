using System;
using System.Drawing;
using System.Windows.Forms;

namespace DotGradient
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get
            {
                return pictureBoxMain.Image;
            }
            set
            {
                pictureBoxMain.Image = value;
                Size subtract = Size.Subtract(value.Size, pictureBoxMain.ClientSize);
                this.ClientSize = Size.Add(ClientSize, subtract);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                Image.Save(saveFileDialogMain.FileName);
                MessageBox.Show("Done.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
