using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CSWinForm
{
    public partial class MainForm : Form
    {
        private const string str = "ff fi fl ffi ffl st We Wa MV WM WA";
        private Font font = new Font("Arial", 48.0f, FontStyle.Italic);
        private StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
        private Size size_max = new Size(int.MaxValue, int.MaxValue);
        private Pen line = new Pen(Color.FromArgb(64, Color.Green));
        private TextFormatFlags flags = TextFormatFlags.NoPadding;

        public MainForm()
        {
            InitializeComponent();
        }

        private Rectangle DrawText(Graphics graphics, string text, Point pt)
        {
            TextRenderer.DrawText(graphics, text, font, pt, Color.Black, flags);
            return new Rectangle(pt, TextRenderer.MeasureText(graphics, text, font, size_max, flags));
        }

        private Size MeasureText(Graphics graphics, string text)
        {
            return TextRenderer.MeasureText(graphics, text, font, size_max, flags);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var widths = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                widths[i] = MeasureText(g, str.Substring(0, i + 1)).Width;
            }

            Point p_start = new Point(10, 10);
            Rectangle rect = DrawText(g, str, p_start);

            Point p = p_start;
            p.Y = rect.Bottom;
            for (int i = 0; i < str.Length; i++)
            {
                p.X = p_start.X + widths[i] - MeasureText(g, str[i].ToString()).Width;
                DrawText(g, str[i].ToString(), p);
            }
        }
    }
}
