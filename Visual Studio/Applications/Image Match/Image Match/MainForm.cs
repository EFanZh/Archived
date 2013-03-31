using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageMatch
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private double ColorDistance(Color color1, Color color2)
        {
            return Math.Sqrt(Math.Pow((color1.R - color2.R), 2) + Math.Pow((color1.G - color2.G), 2) + Math.Pow((color1.B - color2.B), 2));
        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            if (opFlDlgMain.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pctrBxMain.Image = new Bitmap(opFlDlgMain.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblColorShow_Click(object sender, EventArgs e)
        {
            if (clrDlgMain.ShowDialog() == DialogResult.OK)
            {
                lblColorShow.BackColor = clrDlgMain.Color;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            byte[] bytImg;
            double radius;
            int bytCount;
            Bitmap bmp;
            BitmapData bmpData;
            Graphics g;
            IntPtr bmpPtr;

            if (pctrBxMain.Image != null)
            {
                try
                {
                    bmp = new Bitmap(pctrBxMain.Image.Width, pctrBxMain.Image.Height, PixelFormat.Format32bppRgb);
                    g = Graphics.FromImage(bmp);
                    g.DrawImage(pctrBxMain.Image, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    radius = double.Parse(txtBxRadius.Text);

                    bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                    bmpPtr = bmpData.Scan0;
                    bytCount = bmpData.Stride * bmp.Height;
                    bytImg = new byte[bytCount];
                    Marshal.Copy(bmpPtr, bytImg, 0, bytCount);
                    for (int i = 0; i < bytImg.Length - 4; i += 4)
                    {
                        if (ColorDistance(Color.FromArgb(bytImg[i + 2], bytImg[i + 1], bytImg[i]), lblColorShow.BackColor) <= radius)
                        {
                            bytImg[i] = bytImg[i + 1] = bytImg[i + 2] = 255;
                        }
                        else
                        {
                            bytImg[i] = bytImg[i + 1] = bytImg[i + 2] = 0;
                        }
                        //bytImg[i] = bytImg[i + 1] = bytImg[i + 2] = (byte)(255 * (1 - ColorDistance(Color.FromArgb(bytImg[i + 2], bytImg[i + 1], bytImg[i]), lblColorShow.BackColor) / ColorDistance(Color.Black, Color.White))); // Something Interesting.
                    }
                    Marshal.Copy(bytImg, 0, bmpPtr, bytCount);
                    bmp.UnlockBits(bmpData);
                    (new ResultForm(bmp)).Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请打开一张图片。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
