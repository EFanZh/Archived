using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImgProcCore
{
    internal partial class ImageAddTextPluginForm : Form
    {
        ImageAddTextPlugin imageAddTextPlugin;

        public ImageAddTextPluginForm(ImageAddTextPlugin imageAddTextPlugin)
        {
            InitializeComponent();

            this.imageAddTextPlugin = imageAddTextPlugin;

            labelFont.Text = fontDialogMain.Font.FontFamily.Name;
            labelFont.Font = fontDialogMain.Font;
            labelColor.BackColor = colorDialogMain.Color;
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

        private void labelFont_Click(object sender, EventArgs e)
        {
            try
            {
                if (fontDialogMain.ShowDialog() == DialogResult.OK)
                {
                    labelFont.Text = fontDialogMain.Font.FontFamily.Name;
                    labelFont.Font = fontDialogMain.Font;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("抱歉，暂不支持此字体。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelColor_Click(object sender, EventArgs e)
        {
            if (colorDialogMain.ShowDialog() == DialogResult.OK)
            {
                labelColor.BackColor = colorDialogMain.Color;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ImageAddTextPluginContext iatpc = new ImageAddTextPluginContext();

            iatpc.XOffset = int.Parse(textBoxXOffset.Text);
            iatpc.YOffset = int.Parse(textBoxYOffset.Text);
            if (radioButtonTL.Checked)
            {
                iatpc.Position = ContentAlignment.TopLeft;
            }
            else if (radioButtonTC.Checked)
            {
                iatpc.Position = ContentAlignment.TopCenter;
            }
            else if (radioButtonTR.Checked)
            {
                iatpc.Position = ContentAlignment.TopRight;
            }
            else if (radioButtonML.Checked)
            {
                iatpc.Position = ContentAlignment.MiddleLeft;
            }
            else if (radioButtonMC.Checked)
            {
                iatpc.Position = ContentAlignment.MiddleCenter;
            }
            else if (radioButtonMR.Checked)
            {
                iatpc.Position = ContentAlignment.MiddleRight;
            }
            else if (radioButtonBL.Checked)
            {
                iatpc.Position = ContentAlignment.BottomLeft;
            }
            else if (radioButtonBC.Checked)
            {
                iatpc.Position = ContentAlignment.BottomCenter;
            }
            else if (radioButtonBR.Checked)
            {
                iatpc.Position = ContentAlignment.BottomRight;
            }
            iatpc.Text = textBoxText.Text;
            iatpc.Font = fontDialogMain.Font;
            iatpc.Color = colorDialogMain.Color;
            imageAddTextPlugin.ImageAddTextPluginContext = iatpc;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
