using System;

namespace ThreeDDrawing
{
    internal class Camera
    {
        private double default_angle_of_view;

        private double parameter;

        public Camera(double full_view_size, double angle_of_view, CameraTransform camera_transform)
        {
            default_angle_of_view = angle_of_view;

            FullViewSize = full_view_size;
            AngleOfView = angle_of_view;
            CommitFVSAndAOV();

            Transform = camera_transform;
        }

        public double FullViewSize
        {
            get;
            set;
        }

        public double AngleOfView
        {
            get;
            set;
        }

        public CameraTransform Transform
        {
            get;
            set;
        }

        public void Reset()
        {
            AngleOfView = default_angle_of_view;
            CommitFVSAndAOV();
            Transform.Reset();
        }

        public void CommitFVSAndAOV()
        {
            parameter = FullViewSize / AngleOfView;
        }

        public Point2D Get2DPoint(Point3D point)
        {
            Point3D new_point = Transform.GetCameraCoordinates(point);

            double x2pz2 = new_point.X * new_point.X + new_point.Z * new_point.Z;
            double r_scale = Math3D.InvSqrt(x2pz2) * Math.Acos(new_point.Y * Math3D.InvSqrt(x2pz2 + new_point.Y * new_point.Y)) * parameter;

            return new Point2D(new_point.X * r_scale, new_point.Z * r_scale);
        }
    }
}
