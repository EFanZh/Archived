using System;
using System.Text;
using System.Windows.Forms;

namespace FullwidthToHalfwidth
{
    public partial class MainForm : Form
    {
        string char_w = "　！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～";
        string char_f = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        string char_except = "！（），：；？～";

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonFullwidthToHalfwidth_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in textBoxContent.Text)
            {
                int i = char_w.IndexOf(item);
                if (i >= 0 && char_except.IndexOf(item) == -1)
                {
                    sb.Append(char_f[i]);
                }
                else
                {
                    sb.Append(item);
                }
            }
            textBoxContent.Text = sb.ToString();
            textBoxContent.SelectAll();
            textBoxContent.Focus();
        }
    }
}
