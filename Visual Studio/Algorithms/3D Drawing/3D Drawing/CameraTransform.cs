using System;

namespace ThreeDDrawing
{
    internal class CameraTransform
    {
        private Point3D initial_yaw_axis = new Point3D(0.0, 0.0, -1.0);
        private Point3D initial_pitch_axis = new Point3D(1.0, 0.0, 0.0);
        private Point3D initial_roll_axis = new Point3D(0.0, 1.0, 0.0);

        public CameraTransform(Point3D position, Quaternion rotation)
        {
            DefaultPosition = new Point3D(position);
            DefaultRotation = new Quaternion(rotation);
            Position = new Point3D(position);
            Rotation = new Quaternion(rotation);
        }

        public Point3D DefaultPosition
        {
            get;
            set;
        }

        public Quaternion DefaultRotation
        {
            get;
            set;
        }

        public Point3D Position
        {
            get;
            set;
        }

        public Quaternion Rotation
        {
            get;
            set;
        }

        public Point3D GetYawAxis()
        {
            return Rotate(Rotation, initial_yaw_axis);
        }

        public Point3D GetPitchAxis()
        {
            return Rotate(Rotation, initial_pitch_axis);
        }

        public Point3D GetRollAxis()
        {
            return Rotate(Rotation, initial_roll_axis);
        }

        public void Yaw(double angle)
        {
            Rotation = GetRotateQuaternion(GetYawAxis(), angle) * Rotation;
        }

        public void Pitch(double angle)
        {
            Rotation = GetRotateQuaternion(GetPitchAxis(), angle) * Rotation;
        }

        public void Roll(double angle)
        {
            Rotation = GetRotateQuaternion(GetRollAxis(), angle) * Rotation;
        }

        public void MoveForward(double length)
        {
            Move(GetRollAxis(), length);
        }

        public void MoveRight(double length)
        {
            Move(GetPitchAxis(), length);
        }

        public void MoveDown(double length)
        {
            Move(GetYawAxis(), length);
        }

        public void Reset()
        {
            Position = new Point3D(DefaultPosition);
            Rotation = new Quaternion(DefaultRotation);
        }

        public Point3D GetCameraCoordinates(Point3D point)
        {
            // Reverse rotate the world.
            return Rotate(Rotation.GetConjugate(), new Point3D(point.X - Position.X, point.Y - Position.Y, point.Z - Position.Z));
        }

        private void Move(Point3D axis, double length)
        {
            Position.X += length * axis.X;
            Position.Y += length * axis.Y;
            Position.Z += length * axis.Z;
        }

        private static Quaternion GetRotateQuaternion(Point3D axis, double angle)
        {
            double t = angle * 0.5;
            double s = Math.Sin(t);
            return new Quaternion(Math.Cos(t), s * axis.X, s * axis.Y, s * axis.Z);
        }

        private static Point3D Rotate(Quaternion rotation, Point3D point)
        {
            Quaternion result = rotation * (new Quaternion(0.0, point.X, point.Y, point.Z)) * rotation.GetConjugate();
            return new Point3D(result.I, result.J, result.K);
        }
    }
}
