using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MatrixTransform
{
    public partial class MainForm : Form
    {
        private Canvas canvas_1 = new Canvas(), canvas_2 = new Canvas();
        private HashSet<PointF> dots_1 = new HashSet<PointF>(), dots_2 = new HashSet<PointF>();
        private Rectangle rect_1, rect_2;
        private Matrix2x2 m2x2 = new Matrix2x2() { A11 = 1.0f, A22 = 1.0f };

        public MainForm()
        {
            InitializeComponent();

            canvas_1.Dots = dots_1;
            canvas_2.Dots = dots_2;
            rect_1 = new Rectangle(0, 0, panelCanvas.ClientSize.Width / 2, panelCanvas.ClientSize.Height);
            rect_2 = new Rectangle(new Point(rect_1.Right, 0), rect_1.Size);
            canvas_1.Size = rect_1.Size;
            canvas_2.Size = rect_2.Size;

            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(panelCanvas, true);
        }

        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            canvas_1.Render(e.Graphics, rect_1);
            canvas_2.Render(e.Graphics, rect_2);
        }

        private void panelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < panelCanvas.ClientSize.Width / 2)
            {
                PointF p = new PointF(e.X - rect_1.Width / 2, rect_2.Height / 2 - e.Y);
                dots_1.Add(p);
                dots_2.Add(m2x2.Transform(p));
                panelCanvas.Invalidate();
            }
        }

        private void panelCanvas_Resize(object sender, EventArgs e)
        {
            rect_1 = new Rectangle(0, 0, panelCanvas.ClientSize.Width / 2, panelCanvas.ClientSize.Height);
            rect_2 = new Rectangle(new Point(rect_1.Right, 0), rect_1.Size);

            canvas_1.Size = rect_1.Size;
            canvas_2.Size = rect_2.Size;
            panelCanvas.Invalidate();
        }

        private void textBoxMatrix_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m2x2.A11 = float.Parse(textBoxMatrixA11.Text);
                m2x2.A12 = float.Parse(textBoxMatrixA12.Text);
                m2x2.A21 = float.Parse(textBoxMatrixA21.Text);
                m2x2.A22 = float.Parse(textBoxMatrixA22.Text);

                dots_2.Clear();
                foreach (var dot in dots_1)
                {
                    dots_2.Add(m2x2.Transform(dot));
                }

                panelCanvas.Invalidate();
            }
            catch (Exception)
            {
            }
        }

        private void panelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < panelCanvas.ClientSize.Width / 2)
                {
                    PointF p = new PointF(e.X - rect_1.Width / 2, rect_2.Height / 2 - e.Y);
                    dots_1.Add(p);
                    dots_2.Add(m2x2.Transform(p));
                    panelCanvas.Invalidate();
                }
            }
        }
    }
}
