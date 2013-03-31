using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImgProcCore
{
    internal partial class ImageScalePluginForm : Form
    {
        ImageScalePlugin imageScalePlugin;

        public ImageScalePluginForm(ImageScalePlugin imageScalePlugin)
        {
            InitializeComponent();

            this.imageScalePlugin = imageScalePlugin;
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

        private void textBoxFloat_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            float t;
            if (!float.TryParse(textBox.Text, out t))
            {
                e.Cancel = true;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ImageScalePluginContext ispc = new ImageScalePluginContext();

            if (tabControlMain.SelectedTab == tabPageKeepSize)
            {
                ispc.ScaleMethod = ImageScalePluginMethod.KeepSize;
                ispc.Width = int.Parse(textBoxWidth.Text);
                ispc.Height = int.Parse(textBoxHeight.Text);
            }
            else if (tabControlMain.SelectedTab == tabPageKeepAspectRatio)
            {
                if (radioButtonKeepWidth.Checked)
                {
                    ispc.ScaleMethod = ImageScalePluginMethod.KeepAspectRatioWidth;
                    ispc.Width = int.Parse(textBoxKeepWidth.Text);
                }
                else if (radioButtonKeepHeight.Checked)
                {
                    ispc.ScaleMethod = ImageScalePluginMethod.KeepAspectRatioHeight;
                    ispc.Height = int.Parse(textBoxKeepHeight.Text);
                }
            }
            ispc.Resolution = float.Parse(textBoxResolution.Text);
            imageScalePlugin.ScaleContext = ispc;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void radioButtonKeep_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKeepWidth.Checked)
            {
                textBoxKeepWidth.Enabled = true;
                textBoxKeepHeight.Enabled = false;
            }
            else if (radioButtonKeepHeight.Checked)
            {
                textBoxKeepHeight.Enabled = true;
                textBoxKeepWidth.Enabled = false;
            }
        }
    }
}
