using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrameRate
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancel_token_source = new CancellationTokenSource();
        private Task task;
        private Scene scene = new Scene();

        public MainForm()
        {
            InitializeComponent();

            Run(new Progress<int>(v => this.Invalidate(true)));
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cancel_token_source.Cancel();
            task.Wait();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            scene.Render(e.Graphics, this.ClientSize.Width, this.ClientSize.Height, DateTime.Now.Millisecond, 1000);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(1280, 720);
            Graphics g = Graphics.FromImage(bitmap);
            int fps = 75, time = 6000, frames = fps * time / 1000;

            for (int i = 0; i < frames; i++)
            {
                scene.Render(g, bitmap.Width, bitmap.Height, 1000 * i / fps, time);
                bitmap.Save(string.Format(@"D:\Images\Sources\Image_{0}.png", i));
            }
        }

        private async void Run(IProgress<int> progress)
        {
            task = Task.Run(() =>
            {
                while (!cancel_token_source.IsCancellationRequested)
                {
                    Thread.Sleep(1);
                    progress.Report(0);
                }
            });
            await task;
        }
    }
}
