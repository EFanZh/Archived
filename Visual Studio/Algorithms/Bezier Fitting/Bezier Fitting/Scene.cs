using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierFitting
{
    internal class Scene
    {
        private const float radius = 3.0f;
        private const float double_radius = radius + radius;
        private const float radius_s = radius * radius;

        public Scene()
        {
            DataPoints = new List<PointD>();
            TargetBezier = new Bezier();
            CurrentBezier = new Bezier();

            MinDeviation = double.MaxValue;
        }

        public List<PointD> DataPoints
        {
            get;
            private set;
        }

        public Bezier TargetBezier
        {
            get;
            private set;
        }

        public double Deviation
        {
            get;
            private set;
        }

        public Bezier CurrentBezier
        {
            get;
            private set;
        }

        public double MinDeviation
        {
            get;
            private set;
        }

        public void Render(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            foreach (var p in DataPoints)
            {
                DrawPoint(graphics, p.X, p.Y, Brushes.Red);
            }

            DrawBezier(graphics, TargetBezier, Pens.Green, null, false);
            DrawBezier(graphics, CurrentBezier, Pens.Blue, Brushes.Black, true);
        }

        public void SetPoint1(double x, double y)
        {
            Bezier b = CurrentBezier;
            b.Point1 = new PointD(x, y);
            CurrentBezier = b;
            CalcDeviation();
        }

        public void SetPoint2(double x, double y)
        {
            Bezier b = CurrentBezier;
            b.Point2 = new PointD(x, y);
            CurrentBezier = b;
            CalcDeviation();
        }

        public void SetPoint3(double x, double y)
        {
            Bezier b = CurrentBezier;
            b.Point3 = new PointD(x, y);
            CurrentBezier = b;
            CalcDeviation();
        }

        public void SetPoint4(double x, double y)
        {
            Bezier b = CurrentBezier;
            b.Point4 = new PointD(x, y);
            CurrentBezier = b;
            CalcDeviation();
        }

        private void CalcDeviation()
        {
            double v = Fitting.CalcDeviation(DataPoints, CurrentBezier);
            if (v < MinDeviation)
            {
                MinDeviation = v;
                TargetBezier = CurrentBezier;
            }
        }

        public int HitTest(double x, double y)
        {
            double dx, dy;
            double v, min = double.MaxValue;
            int r = 0;

            dx = x - CurrentBezier.Point1.X;
            dy = y - CurrentBezier.Point1.Y;
            v = dx * dx + dy * dy;
            if (v <= radius_s && v <= min)
            {
                min = v;
                r = 1;
            }

            dx = x - CurrentBezier.Point2.X;
            dy = y - CurrentBezier.Point2.Y;
            v = dx * dx + dy * dy;
            if (v <= radius_s && v <= min)
            {
                min = v;
                r = 2;
            }

            dx = x - CurrentBezier.Point3.X;
            dy = y - CurrentBezier.Point3.Y;
            v = dx * dx + dy * dy;
            if (v <= radius_s && v <= min)
            {
                min = v;
                r = 3;
            }

            dx = x - CurrentBezier.Point4.X;
            dy = y - CurrentBezier.Point4.Y;
            v = dx * dx + dy * dy;
            if (v <= radius_s && v <= min)
            {
                min = v;
                r = 4;
            }

            return r;
        }

        private void DrawBezier(Graphics graphics, Bezier bezier, Pen pen, Brush brush, bool control)
        {
            graphics.DrawBezier(pen, bezier.Point1.ToPointF(), bezier.Point2.ToPointF(), bezier.Point3.ToPointF(), bezier.Point4.ToPointF());
            if (control)
            {
                graphics.DrawLine(pen, bezier.Point1.ToPointF(), bezier.Point2.ToPointF());
                graphics.DrawLine(pen, bezier.Point4.ToPointF(), bezier.Point3.ToPointF());

                DrawPoint(graphics, bezier.Point1.X, bezier.Point1.Y, brush);
                DrawPoint(graphics, bezier.Point2.X, bezier.Point2.Y, brush);
                DrawPoint(graphics, bezier.Point3.X, bezier.Point3.Y, brush);
                DrawPoint(graphics, bezier.Point4.X, bezier.Point4.Y, brush);
            }
        }

        private void DrawPoint(Graphics graphics, double x, double y, Brush brush)
        {
            graphics.FillEllipse(brush, (float)(x - radius), (float)(y - radius), double_radius, double_radius);
        }
    }
}
