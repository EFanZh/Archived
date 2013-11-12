using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sorting
{
    internal class SortVisualization
    {
        private int operand_a = -1, operand_b = -1;
        private SortOperation sort_operation = SortOperation.None;
        private Brush fill_brush = new SolidBrush(Color.FromArgb(0xff, 0x45, 0x72, 0xff));
        private Brush compare_brush = new SolidBrush(Color.Green);
        private Pen set_value_pen = new Pen(Color.Red, 2.0f);
        private Pen set_value_indirect_pen = new Pen(Color.Purple, 2.0f);
        private Pen swap_pen = new Pen(Color.Orange, 2.0f);

        public int[] Data
        {
            get;
            set;
        }

        public double Interval
        {
            get;
            set;
        }

        public int Max
        {
            get;
            set;
        }

        public Padding Padding
        {
            get;
            set;
        }

        public Size Size
        {
            get;
            set;
        }

        public void Compare(int a, int b)
        {
            sort_operation = SortOperation.Compare;
            operand_a = a;
            operand_b = b;
        }

        public void SetValue(int a, int b)
        {
            sort_operation = SortOperation.SetValue;
            operand_a = a;
            operand_b = b;
            Data[operand_a] = operand_b;
        }

        public void SetValueIndirect(int a, int b)
        {
            sort_operation = SortOperation.SetValueIndirect;
            operand_a = a;
            operand_b = b;
            Data[operand_a] = Data[operand_b];
        }

        public void Swap(int a, int b)
        {
            sort_operation = SortOperation.Swap;
            operand_a = a;
            operand_b = b;

            if (operand_a >= 0 && operand_b >= 0)
            {
                Data.Swap(operand_a, operand_b);
            }
        }

        public void Reset()
        {
            sort_operation = SortOperation.None;
        }

        public void Render(Graphics graphics)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(graphics, new Rectangle(Point.Empty, Size));
            Graphics g = bg.Graphics;
            g.Transform = graphics.Transform;

            if (Data != null && Data.Length > 0)
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                int cy_bottom = Size.Height - Padding.Bottom;
                int cy = cy_bottom - Padding.Top;
                double dy = (double)cy / Max;
                double x = Padding.Left + Interval / 2.0;
                int cx = Size.Width - Padding.Left - Padding.Right;
                double dx = (double)cx / Data.Length;
                double w = dx - Interval;

                for (int i = 0; i < Data.Length; i++)
                {
                    Brush b;
                    if (sort_operation == SortOperation.Compare && (i == operand_a || i == operand_b))
                    {
                        b = compare_brush;
                    }
                    else
                    {
                        b = fill_brush;
                    }
                    double h = (double)Data[i] * dy;
                    g.FillRectangle(b, (float)x, (float)(cy_bottom - h), (float)w, (float)h);
                    x += dx;
                }

                Action<Pen, PointF> draw_arrow = (pen, point) =>
                {
                    g.DrawLines(pen, new PointF[]
                    {
                        new PointF(point.X - 6, point.Y - 6),
                        new PointF(point.X, point.Y),
                        new PointF(point.X + 6, point.Y - 6)
                    });
                };
                if (sort_operation == SortOperation.SetValue)
                {
                    float x_1 = (float)(Padding.Left + (operand_a + 0.5) * dx);
                    float y_1 = (float)(cy_bottom - (double)Data[operand_a] * dy);
                    g.DrawLine(set_value_pen, x_1, Padding.Top, x_1, y_1);
                    draw_arrow(set_value_pen, new PointF(x_1, y_1));
                }
                else if (sort_operation == SortOperation.SetValueIndirect || sort_operation == SortOperation.Swap)
                {
                    float x_1 = (float)(Padding.Left + (operand_a + 0.5) * dx);
                    float x_2 = (float)(Padding.Left + (operand_b + 0.5) * dx);
                    float y_1 = (float)(cy_bottom - Data[operand_a] * dy);
                    float y_2 = (float)(cy_bottom - Data[operand_b] * dy);
                    float y = Math.Min(y_1, y_2);

                    Pen p;
                    if (sort_operation == SortOperation.SetValueIndirect)
                    {
                        p = set_value_indirect_pen;
                    }
                    else
                    {
                        p = swap_pen;
                    }

                    g.DrawLines(p, new PointF[]
                    {
                        new PointF(x_1, y_1),
                        new PointF(x_1, y - 16),
                        new PointF(x_2, y - 16),
                        new PointF(x_2, y_2)
                    });

                    draw_arrow(p, new PointF(x_1, y_1));

                    if (sort_operation == SortOperation.Swap)
                    {
                        draw_arrow(p, new PointF(x_2, y_2));
                    }
                }
            }

            bg.Render();
        }
    }
}
