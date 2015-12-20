using System;
using System.Windows;

namespace BouncingBallAccelerate
{
    internal class Scene
    {
        private Size size;
        private readonly double ballSize;
        private Point initialBallLocation;
        private Vector acceleration;

        public Scene(double ballSize, Point initialBallLocation, Vector acceleration)
        {
            this.ballSize = ballSize;
            this.initialBallLocation = initialBallLocation;
            this.acceleration = acceleration;
        }

        public void SetSize(Size size)
        {
            this.size = size;
        }
        
        public Point GetBallLocation(TimeSpan time)
        {
            var x = GetPosition(initialBallLocation.X, acceleration.X, time);
            var y = GetPosition(initialBallLocation.Y, acceleration.Y, time);

            return new Point(Cut(x, size.Width - ballSize), Cut(y, size.Height - ballSize));
        }

        private static double Square(double x)
        {
            return x * x;
        }

        private static double GetPosition(double s, double a, TimeSpan t)
        {
            return s + 0.5 * a * Square(t.TotalSeconds);
        }

        private static double Cut(double x, double range)
        {
            var k = range * 2.0;
            var p = x % k;

            return p < range ? p : k - p;
        }
    }
}
