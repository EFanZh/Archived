using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawStringTest
{
    public partial class MainForm : Form
    {
        private readonly DrawStringContext drawStringContext = new DrawStringContext();

        public MainForm()
        {
            InitializeComponent();

            this.propertyGrid1.SelectedObject = drawStringContext;
            drawStringContext.Brush = Color.Black;
            drawStringContext.Font = new Font("Times New Roman", 9.0f);
        }

        private void splitContainerMain_Panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(Color.White);
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                SizeF size = e.Graphics.MeasureString(drawStringContext.Text,
                                                      drawStringContext.Font,
                                                      drawStringContext.LayoutRectangle.Size,
                                                      drawStringContext.StringFormat);

                e.Graphics.DrawLine(Pens.Red, drawStringContext.X - 5.0f, drawStringContext.Y, drawStringContext.X + 5.0f, drawStringContext.Y);
                e.Graphics.DrawLine(Pens.Red, drawStringContext.X, drawStringContext.Y - 5.0f, drawStringContext.X, drawStringContext.Y + 5.0f);
                e.Graphics.DrawRectangle(Pens.DodgerBlue, drawStringContext.X, drawStringContext.Y, size.Width, size.Height);
                e.Graphics.DrawRectangle(Pens.MediumSlateBlue, drawStringContext.X, drawStringContext.Y, drawStringContext.Width, drawStringContext.Height);
                e.Graphics.DrawString(drawStringContext.Text,
                                      drawStringContext.Font,
                                      new SolidBrush(drawStringContext.Brush),
                                      drawStringContext.LayoutRectangle,
                                      drawStringContext.StringFormat);
            }
            catch (Exception)
            {
            }
        }

        private void propertyGridMain_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            splitContainer1.Panel1.Invalidate();
        }
    }
}
