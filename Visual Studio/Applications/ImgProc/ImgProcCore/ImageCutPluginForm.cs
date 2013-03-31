using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImgProcCore
{
    internal partial class ImageCutPluginForm : Form
    {
        ImageCutPlugin imageCutPlugin;

        public ImageCutPluginForm(ImageCutPlugin imageCutPlugin)
        {
            InitializeComponent();

            this.imageCutPlugin = imageCutPlugin;
        }

        private void textBoxInteger_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int t;
            if (!int.TryParse(textBox.Text, out t))
            {
                e.Cancel = true;
            }
        }

        private void labelFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialogMain.ShowDialog() == DialogResult.OK)
            {
                labelFillColor.BackColor = colorDialogMain.Color;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ImageCutPluginContext icpc = new ImageCutPluginContext();
            icpc.Size = new Size(int.Parse(textBoxWidth.Text), int.Parse(textBoxHeight.Text));
            if (radioButtonTL.Checked)
            {
                icpc.Position = ContentAlignment.TopLeft;
            }
            else if (radioButtonTC.Checked)
            {
                icpc.Position = ContentAlignment.TopCenter;
            }
            else if (radioButtonTR.Checked)
            {
                icpc.Position = ContentAlignment.TopRight;
            }
            else if (radioButtonML.Checked)
            {
                icpc.Position = ContentAlignment.MiddleLeft;
            }
            else if (radioButtonMC.Checked)
            {
                icpc.Position = ContentAlignment.MiddleCenter;
            }
            else if (radioButtonMR.Checked)
            {
                icpc.Position = ContentAlignment.MiddleRight;
            }
            else if (radioButtonBL.Checked)
            {
                icpc.Position = ContentAlignment.BottomLeft;
            }
            else if (radioButtonBC.Checked)
            {
                icpc.Position = ContentAlignment.BottomCenter;
            }
            else if (radioButtonBR.Checked)
            {
                icpc.Position = ContentAlignment.BottomRight;
            }
            icpc.FillColor = labelFillColor.BackColor;
            imageCutPlugin.ImageCutPluginContext = icpc;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
