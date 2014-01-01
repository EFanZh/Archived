using System.Drawing;
using System.Drawing.Drawing2D;

namespace FrameRate
{
    internal class Scene
    {
        private int[] frame_rates = new[] { 24, 25, 30, 75 };

        public void Render(Graphics graphics, int width, int height, int time, int total_time)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Black);

            float t = 3 * frame_rates.Length + 4;
            float size = 2 * height / t;
            for (int i = 0; i < frame_rates.Length; i++)
            {
                int frame = time * frame_rates[i] / 1000;
                float left = frame * width * 1000 / (float)(frame_rates[i] * total_time);
                float top = (3 * i + 1) * height / t;
                graphics.FillEllipse(Brushes.White, left, top, size, size);
            }
            graphics.FillEllipse(Brushes.White, time * width / (float)total_time, (t - 3) * height / t, size, size);
        }
    }
}
