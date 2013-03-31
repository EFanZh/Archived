using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ASCIIArt
{
    public partial class MainForm : Form
    {
        private string ascii = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        private Bitmap bitmap = null;
        private ResultForm result_form = new ResultForm();

        public MainForm()
        {
            InitializeComponent();

            Graphics graphics = Graphics.FromHwnd(this.Handle);
            IntPtr hdc = graphics.GetHdc();
            var logfont = new NativeMethods.LOGFONT() { lfCharSet = NativeMethods.DEFAULT_CHARSET };
            NativeMethods.EnumFontFamiliesEx(hdc, ref logfont, new NativeMethods.FONTENUMPROC(EnumFontFamExProc), IntPtr.Zero, 0);
            graphics.ReleaseHdc();

            if (comboBoxFontFamily.Items.Count > 0)
            {
                if (comboBoxFontFamily.Items.Contains("Consolas"))
                {
                    comboBoxFontFamily.Text = "Consolas";
                }
                else
                {
                    comboBoxFontFamily.SelectedIndex = 0;
                }
            }
        }

        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            SelectImage();
        }

        private void pictureBoxImage_DoubleClick(object sender, EventArgs e)
        {
            SelectImage();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (pictureBoxImage.Image == null)
            {
                MessageBox.Show("Please select an image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int columns;
            if (string.IsNullOrEmpty(textBoxColumns.Text))
            {
                columns = pictureBoxImage.Image.Width;
                textBoxColumns.Text = columns.ToString();
            }
            else
            {
                if (!int.TryParse(textBoxColumns.Text, out columns))
                {
                    MessageBox.Show("Illegal integer format: \"Columns\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            int lines;
            if (string.IsNullOrEmpty(textBoxLines.Text))
            {
                lines = pictureBoxImage.Image.Height;
                textBoxLines.Text = lines.ToString();
            }
            else
            {
                if (!int.TryParse(textBoxLines.Text, out lines))
                {
                    MessageBox.Show("Illegal integer format: \"Lines\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            float font_size;
            if (string.IsNullOrEmpty(textBoxFontSize.Text))
            {
                font_size = 9.0f;
                textBoxFontSize.Text = font_size.ToString();
            }
            else
            {
                if (!float.TryParse(textBoxFontSize.Text, out font_size))
                {
                    MessageBox.Show("Illegal float format: \"Font Size\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            result_form.ResultFont = new Font(comboBoxFontFamily.Text, font_size);
            using (Graphics graphics = this.CreateGraphics())
            {
                result_form.ResultText = ASCIIArt.Generate(graphics, new Bitmap(pictureBoxImage.Image, columns, lines), result_form.ResultFont, new HashSet<char>(ascii));
            }
            result_form.ShowDialog();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectImage()
        {
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bitmap = new Bitmap(openFileDialogMain.FileName))
                {
                    pictureBoxImage.Image = new Bitmap(bitmap);
                }
                labelImageInfo.Text = string.Format("{0} × {1}, {2}", pictureBoxImage.Image.Width, pictureBoxImage.Image.Height, openFileDialogMain.FileName);
            }
        }

        private int EnumFontFamExProc(ref NativeMethods.ENUMLOGFONT lpelf, ref NativeMethods.NEWTEXTMETRIC lpntm, uint FontType, IntPtr lParam)
        {
            if ((lpelf.elfLogFont.lfPitchAndFamily & 0x3) == NativeMethods.FIXED_PITCH)
            {
                if (lpelf.elfLogFont.lfFaceName.StartsWith("@") || comboBoxFontFamily.Items.Contains(lpelf.elfLogFont.lfFaceName) || FontType != NativeMethods.TRUETYPE_FONTTYPE)
                {
                    return 1;
                }
                comboBoxFontFamily.Items.Add(lpelf.elfLogFont.lfFaceName);
            }
            return 1;
        }
    }
}
