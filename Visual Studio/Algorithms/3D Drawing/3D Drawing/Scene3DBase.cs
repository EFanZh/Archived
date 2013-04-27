using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ThreeDDrawing
{
    internal abstract class Scene3DBase
    {
        private double x_center, y_center;
        private const int max_recursion_depth = 20;

        public Scene3DBase(Size size, double angle_of_view, CameraTransform camera_transform)
        {
            Size = size;
            Camera = new Camera(GetCameraFullViewSize(), angle_of_view, camera_transform);
            CommitSize();
        }

        public Camera Camera
        {
            get;
            set;
        }

        public Size Size
        {
            get;
            private set;
        }

        public void Render(Graphics graphics)
        {
            DoDraw(graphics);
        }

        public void Resize(Size new_size)
        {
            Size = new_size;
            CommitSize();
            Camera.FullViewSize = GetCameraFullViewSize();
            Camera.CommitFVSAndAOV();
        }

        protected abstract void DoDraw(Graphics graphics);

        protected PointF[] GetLines(double min_length, params Point3D[] points)
        {
            var pre_calc_map = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                pre_calc_map[i] = GetViewCoordinates(points[i]);
            }

            var point_list = new List<PointF>();
            point_list.Add(pre_calc_map.First());

            int end = points.Length - 1;
            for (int i = 0; i < end; i++)
            {
                foreach (var p in SliceLine(min_length, points[i], points[i + 1], pre_calc_map[i], pre_calc_map[i + 1]))
                {
                    point_list.Add(p);
                }
            }

            point_list.Add(pre_calc_map.Last());

            return point_list.ToArray();
        }

        protected PointF[] GetPolygon(double min_length, params Point3D[] points)
        {
            var pre_calc_map = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                pre_calc_map[i] = GetViewCoordinates(points[i]);
            }

            var point_list = new List<PointF>();
            point_list.Add(pre_calc_map.First());

            int end = points.Length - 1;
            for (int i = 0; i < end; i++)
            {
                foreach (var p in SliceLine(min_length, points[i], points[i + 1], pre_calc_map[i], pre_calc_map[i + 1]))
                {
                    point_list.Add(p);
                }
            }

            point_list.Add(pre_calc_map.Last());

            foreach (var p2 in SliceLine(min_length, points.Last(), points.First(), pre_calc_map.Last(), pre_calc_map.First()))
            {
                point_list.Add(p2);
            }

            return point_list.ToArray();
        }

        protected void DrawLines(Graphics graphics, Pen pen, double min_length, params Point3D[] points)
        {
            graphics.DrawLines(pen, GetLines(min_length, points));
        }

        protected void DrawPolygon(Graphics graphics, Pen pen, double min_length, params Point3D[] points)
        {
            graphics.DrawPolygon(pen, GetPolygon(min_length, points));
        }

        private List<PointF> SliceLine(double min_length, Point3D line_start, Point3D line_end, PointF line_start_map, PointF line_end_map)
        {
            var point_list = new List<PointF>();

            Action<Point3D, Point3D, PointF, PointF, int> slice = null;
            slice = (point_1, point_2, point_map_1, point_map_2, depth) =>
            {
                if (depth > max_recursion_depth)
                {
                    return;
                }
                Point3D p3d_center = new Point3D((point_1.X + point_2.X) * 0.5, (point_1.Y + point_2.Y) * 0.5, (point_1.Z + point_2.Z) * 0.5);
                PointF p_center = GetViewCoordinates(p3d_center);

                if (Utilities.GetDistance(point_map_1, p_center) > min_length)
                {
                    slice(point_1, p3d_center, point_map_1, p_center, depth + 1);
                }

                point_list.Add(p_center);

                if (Utilities.GetDistance(p_center, point_map_2) > min_length)
                {
                    slice(p3d_center, point_2, p_center, point_map_2, depth + 1);
                }
            };

            point_list.Add(line_start_map);
            slice(line_start, line_end, line_start_map, line_end_map, 0);
            point_list.Add(line_end_map);

            return point_list;
        }

        private void CommitSize()
        {
            x_center = Size.Width * 0.5;
            y_center = Size.Height * 0.5;
        }

        private double GetCameraFullViewSize()
        {
            return Math.Sqrt(Size.Width * Size.Width + Size.Height * Size.Height);
        }

        private PointF GetViewCoordinates(Point3D point)
        {
            Point2D p = Camera.Get2DPoint(point);
            return new PointF((float)(x_center + p.X), (float)(y_center - p.Y));
        }
    }
}
