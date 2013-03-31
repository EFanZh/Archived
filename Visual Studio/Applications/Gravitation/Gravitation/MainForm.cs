using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gravitation
{
    public partial class MainForm : Form
    {
        private ScalarScene scene = new ScalarScene();
        private HashSet<Tuple<PointF, double>> mass_points = new HashSet<Tuple<PointF, double>>();
        private Random random = new Random();

        public MainForm()
        {
            InitializeComponent();

            scene.Size = this.ClientSize;
            scene.GravitationalConstant = 100.0;
            scene.MassPoints = mass_points;
            scene.ColorationColor = Color.LightGreen;
            scene.MaxGravitation = 1;

            for (int i = 0; i < 32; i++)
            {
                mass_points.Add(new Tuple<PointF, double>(new PointF((float)(this.ClientSize.Width / 4 + this.ClientSize.Width / 2 * random.NextDouble()), (float)(this.ClientSize.Height / 4 + this.ClientSize.Height / 2 * random.NextDouble())), 2 + 16 * random.NextDouble()));
            }
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            mass_points.Clear();

            for (int i = 0; i < 32; i++)
            {
                mass_points.Add(new Tuple<PointF, double>(new PointF((float)(this.ClientSize.Width / 4 + this.ClientSize.Width / 2 * random.NextDouble()), (float)(this.ClientSize.Height / 4 + this.ClientSize.Height / 2 * random.NextDouble())), 2 + 16 * random.NextDouble()));
            }

            scene.Size = this.ClientSize;
            this.Invalidate();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            scene.Render(e.Graphics);
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
        }
    }
}
