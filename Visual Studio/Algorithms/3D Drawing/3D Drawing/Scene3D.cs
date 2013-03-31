using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ThreeDDrawing
{
    internal class Scene3D : Scene3DBase, IDisposable
    {
        private Font main_font = SystemFonts.MessageBoxFont;

        private Pen x_axis_pen = new Pen(Color.Red, 3.0f);
        private Pen y_axis_pen = new Pen(Color.Green, 3.0f);
        private Pen z_axis_pen = new Pen(Color.Blue, 3.0f);
        private Pen yaw_axis_pen = new Pen(Color.Orange, 3.0f);
        private Pen pitch_axis_pen = new Pen(Color.Cyan, 3.0f);
        private Pen roll_axis_pen = new Pen(Color.Magenta, 3.0f);
        private Pen box_pen = new Pen(Color.Gray, 2.0f);
        private Pen guide_line_pen = new Pen(Color.FromArgb(96, Color.ForestGreen), 1.0f);

        private Brush x_axis_brush = new SolidBrush(Color.Red);
        private Brush y_axis_brush = new SolidBrush(Color.Green);
        private Brush z_axis_brush = new SolidBrush(Color.Blue);
        private Brush yaw_axis_brush = new SolidBrush(Color.Orange);
        private Brush pitch_axis_brush = new SolidBrush(Color.Cyan);
        private Brush roll_axis_brush = new SolidBrush(Color.Magenta);

        private const double x_axis_from = -10.0, x_axis_to = 10.0;
        private const double y_axis_from = -10.0, y_axis_to = 10.0;
        private const double z_axis_from = -10.0, z_axis_to = 10.0;
        private const double box_length = 4.0;
        private const double min_line_length = 5.0;

        private bool disposed = false;

        public Scene3D(Size size)
            : base(size, Math.PI / 3.0, new CameraTransform(new Point3D(0.0, -40.0, 0.0), new Quaternion()))
        {
        }

        ~Scene3D()
        {
            Dispose(false);
        }

        protected override void DoDraw(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            Action<Point3D, Point3D, Pen, string, Brush> draw_axis = (p1, p2, pen, label, brush) =>
            {
                var axis = GetLines(min_line_length, p1, p2);
                graphics.DrawLines(pen, axis);
                graphics.DrawString(label, main_font, brush, axis.Last());
            };

            draw_axis(new Point3D(x_axis_from, 0.0, 0.0), new Point3D(x_axis_to, 0.0, 0.0), x_axis_pen, "X", x_axis_brush);
            draw_axis(new Point3D(0.0, y_axis_from, 0.0), new Point3D(0.0, y_axis_to, 0.0), y_axis_pen, "Y", y_axis_brush);
            draw_axis(new Point3D(0.0, 0.0, z_axis_from), new Point3D(0.0, 0.0, z_axis_to), z_axis_pen, "Z", z_axis_brush);

            Point3D yaw_axis = this.Camera.Transform.GetYawAxis();
            Point3D pitch_axis = this.Camera.Transform.GetPitchAxis();
            Point3D roll_axis = this.Camera.Transform.GetRollAxis();
            draw_axis(new Point3D(), yaw_axis, yaw_axis_pen, "Yaw", yaw_axis_brush);
            draw_axis(new Point3D(), pitch_axis, pitch_axis_pen, "Pitch", pitch_axis_brush);
            draw_axis(new Point3D(), roll_axis, roll_axis_pen, "Roll", roll_axis_brush);

            Point3D ypr = new Point3D(yaw_axis.X + pitch_axis.X + roll_axis.X, yaw_axis.Y + pitch_axis.Y + roll_axis.Y, yaw_axis.Z + pitch_axis.Z + roll_axis.Z);

            var ypr_box = new Point3D[]
            {
                new Point3D(-yaw_axis.X - pitch_axis.X + roll_axis.X, -yaw_axis.Y - pitch_axis.Y + roll_axis.Y, -yaw_axis.Z - pitch_axis.Z + roll_axis.Z),
                new Point3D(-yaw_axis.X + pitch_axis.X + roll_axis.X, -yaw_axis.Y + pitch_axis.Y + roll_axis.Y, -yaw_axis.Z + pitch_axis.Z + roll_axis.Z),
                new Point3D(yaw_axis.X + pitch_axis.X + roll_axis.X, yaw_axis.Y + pitch_axis.Y + roll_axis.Y, yaw_axis.Z + pitch_axis.Z + roll_axis.Z),
                new Point3D(yaw_axis.X - pitch_axis.X + roll_axis.X, yaw_axis.Y - pitch_axis.Y + roll_axis.Y, yaw_axis.Z - pitch_axis.Z + roll_axis.Z),
                new Point3D(-yaw_axis.X - pitch_axis.X - roll_axis.X, -yaw_axis.Y - pitch_axis.Y - roll_axis.Y, -yaw_axis.Z - pitch_axis.Z - roll_axis.Z),
                new Point3D(-yaw_axis.X + pitch_axis.X - roll_axis.X, -yaw_axis.Y + pitch_axis.Y - roll_axis.Y, -yaw_axis.Z + pitch_axis.Z - roll_axis.Z),
                new Point3D(yaw_axis.X + pitch_axis.X - roll_axis.X, yaw_axis.Y + pitch_axis.Y - roll_axis.Y, yaw_axis.Z + pitch_axis.Z - roll_axis.Z),
                new Point3D(yaw_axis.X - pitch_axis.X - roll_axis.X, yaw_axis.Y - pitch_axis.Y - roll_axis.Y, yaw_axis.Z - pitch_axis.Z - roll_axis.Z)
            };

            this.DrawPolygon(graphics, box_pen, min_line_length, ypr_box[0], ypr_box[1], ypr_box[2], ypr_box[3]);
            this.DrawPolygon(graphics, box_pen, min_line_length, ypr_box[4], ypr_box[5], ypr_box[6], ypr_box[7]);
            this.DrawLines(graphics, box_pen, min_line_length, ypr_box[0], ypr_box[4]);
            this.DrawLines(graphics, box_pen, min_line_length, ypr_box[1], ypr_box[5]);
            this.DrawLines(graphics, box_pen, min_line_length, ypr_box[2], ypr_box[6]);
            this.DrawLines(graphics, box_pen, min_line_length, ypr_box[3], ypr_box[7]);

            var box_vertexes = new Point3D[]
            {
                new Point3D(-box_length, -box_length, -box_length),
                new Point3D(-box_length, box_length, -box_length),
                new Point3D(box_length, box_length, -box_length),
                new Point3D(box_length, -box_length, -box_length),
                new Point3D(-box_length, -box_length, 0.0),
                new Point3D(-box_length, box_length, 0.0),
                new Point3D(box_length, box_length, 0.0),
                new Point3D(box_length, -box_length, 0.0),
                new Point3D(-box_length, -box_length, box_length),
                new Point3D(-box_length, box_length, box_length),
                new Point3D(box_length, box_length, box_length),
                new Point3D(box_length, -box_length, box_length),
                new Point3D(-box_length, 0.0, -box_length),
                new Point3D(-box_length, 0.0, box_length),
                new Point3D(box_length, 0.0, box_length),
                new Point3D(box_length, 0.0, -box_length),
                new Point3D(0.0, -box_length, -box_length),
                new Point3D(0.0, -box_length, box_length),
                new Point3D(0.0, box_length, box_length),
                new Point3D(0.0, box_length, -box_length)
            };

            this.DrawPolygon(graphics, box_pen, min_line_length, box_vertexes[0], box_vertexes[1], box_vertexes[2], box_vertexes[3]);
            this.DrawPolygon(graphics, box_pen, min_line_length, box_vertexes[4], box_vertexes[5], box_vertexes[6], box_vertexes[7]);
            this.DrawPolygon(graphics, box_pen, min_line_length, box_vertexes[8], box_vertexes[9], box_vertexes[10], box_vertexes[11]);
            this.DrawPolygon(graphics, box_pen, min_line_length, box_vertexes[12], box_vertexes[13], box_vertexes[14], box_vertexes[15]);
            this.DrawPolygon(graphics, box_pen, min_line_length, box_vertexes[16], box_vertexes[17], box_vertexes[18], box_vertexes[19]);
            this.DrawLines(graphics, box_pen, min_line_length, box_vertexes[0], box_vertexes[8]);
            this.DrawLines(graphics, box_pen, min_line_length, box_vertexes[1], box_vertexes[9]);
            this.DrawLines(graphics, box_pen, min_line_length, box_vertexes[2], box_vertexes[10]);
            this.DrawLines(graphics, box_pen, min_line_length, box_vertexes[3], box_vertexes[11]);

            graphics.DrawLine(guide_line_pen, 0.0f, Size.Height * 0.5f, Size.Width, Size.Height * 0.5f);
            graphics.DrawLine(guide_line_pen, Size.Width * 0.5f, 0.0f, Size.Width * 0.5f, Size.Height);

            graphics.DrawString(string.Format("Scene Size: {0} × {1}\r\n" +
                "Camera Angle Of View: {2}°\r\n" +
                "Camera Full View Size: {3}\r\n" +
                "Yaw Axis: ({4}, {5}, {6})\r\n" +
                "Pitch Axis: ({7}, {8}, {9})\r\n" +
                "Roll Axis: ({10}, {11}, {12})\r\n" +
                "Camera Position: ({13}, {14}, {15})\r\n" +
                "Camera Rotation: [{16}, {17}, {18}, {19}]",
                this.Size.Width, this.Size.Height,
                this.Camera.AngleOfView / Math.PI * 180,
                this.Camera.FullViewSize,
                yaw_axis.X, yaw_axis.Y, yaw_axis.Z,
                pitch_axis.X, pitch_axis.Y, pitch_axis.Z,
                roll_axis.X, roll_axis.Y, roll_axis.Z,
                this.Camera.Transform.Position.X,
                this.Camera.Transform.Position.Y,
                this.Camera.Transform.Position.Z,
                this.Camera.Transform.Rotation.W,
                this.Camera.Transform.Rotation.I,
                this.Camera.Transform.Rotation.J,
                this.Camera.Transform.Rotation.K), main_font, SystemBrushes.WindowText, 10, 10);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    x_axis_pen.Dispose();
                    y_axis_pen.Dispose();
                    z_axis_pen.Dispose();
                    yaw_axis_pen.Dispose();
                    pitch_axis_pen.Dispose();
                    roll_axis_pen.Dispose();
                    box_pen.Dispose();
                    guide_line_pen.Dispose();
                    x_axis_brush.Dispose();
                    y_axis_brush.Dispose();
                    z_axis_brush.Dispose();
                    yaw_axis_brush.Dispose();
                    pitch_axis_brush.Dispose();
                    roll_axis_brush.Dispose();
                    x_axis_pen = null;
                    y_axis_pen = null;
                    z_axis_pen = null;
                    yaw_axis_pen = null;
                    pitch_axis_pen = null;
                    roll_axis_pen = null;
                    box_pen = null;
                    guide_line_pen = null;
                    x_axis_brush = null;
                    y_axis_brush = null;
                    z_axis_brush = null;
                    yaw_axis_brush = null;
                    pitch_axis_brush = null;
                    roll_axis_brush = null;

                    GC.SuppressFinalize(this);
                }

                disposed = true;
            }
        }
    }
}
