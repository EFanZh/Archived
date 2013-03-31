using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImgProcCore
{
    internal partial class ImageMosaicPluginForm : Form
    {
        ImageMosaicPlugin imageMosaicPlugin;

        public ImageMosaicPluginForm(ImageMosaicPlugin imageMosaicPlugin)
        {
            InitializeComponent();

            this.imageMosaicPlugin = imageMosaicPlugin;
        }

        private void textBoxSize_Validating(object sender, CancelEventArgs e)
        {
            int t;
            if (!int.TryParse(textBoxSize.Text, out t))
            {
                e.Cancel = true;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            imageMosaicPlugin.Size = int.Parse(textBoxSize.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
