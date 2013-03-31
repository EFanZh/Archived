using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DividePoints
{
    public partial class MainForm : Form
    {
        private List<Point> points = new List<Point>();
        private List<Tuple<int, int>> lines = new List<Tuple<int, int>>();
        private HashSet<HashSet<int>> sets;
        private Random random = new Random();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(e.Location);
            sets = Divider.Analysis(points.ToArray());
            this.Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (sets != null)
            {
                e.Graphics.Clear(Color.White);
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                foreach (var set in sets)
                {
                    SolidBrush b = new SolidBrush(Color.FromArgb(random.Next(0, 64), random.Next(0, 128), random.Next(0, 256)));
                    foreach (var p in set)
                    {
                        e.Graphics.FillEllipse(b, points[p].X - 4, points[p].Y - 4, 8, 8);
                    }
                    if (set.Count > 2)
                    {
                        var s = new List<Point>();
                        foreach (var p in set)
                        {
                            s.Add(points[p]);
                        }
                        var r = Divider.ConvexHull(s.ToArray());
                        e.Graphics.DrawPolygon(Pens.Black, r);
                    }
                }
            }
        }
    }
}
