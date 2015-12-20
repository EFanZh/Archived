using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BouncingBall
{
    internal class Scene
    {
        private double g;
        private Point ballIinitialLocation;
        private Vector ballIinitialVelocity;
        
        private Func<TimeSpan, double> xMotionFunction;
        private Func<TimeSpan, double> yMotionFunction;

        private readonly FixedSizeQueue<double> xPositions;
        private readonly FixedSizeQueue<double> yPositions;

        private long lastAfterimageId = 0;

        public Scene(double g, double ballSize, Point ballIinitialLocation, Vector ballIinitialVelocity, int maxAfterimageCount, TimeSpan afterimageInterval)
        {
            this.g = g;
            BallSize = ballSize;
            this.ballIinitialLocation = ballIinitialLocation;
            this.ballIinitialVelocity = ballIinitialVelocity;
            MaxAfterimageCount = maxAfterimageCount;
            this.AfterimageInterval = afterimageInterval;

            xPositions = new FixedSizeQueue<double>(maxAfterimageCount);
            yPositions = new FixedSizeQueue<double>(maxAfterimageCount);
        }
        
        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        public double BallSize
        {
            get;
            set;
        }

        public int MaxAfterimageCount
        {
            get;
            set;
        }

        public TimeSpan AfterimageInterval
        {
            get;
        }

        public void ClearXCache()
        {
            xMotionFunction = MotionFunction.CreateXMotionFunction(Width,
                                                                   BallSize,
                                                                   ballIinitialLocation.X,
                                                                   ballIinitialVelocity.X);

            lastAfterimageId = 0;
        }

        public void ClearYCache()
        {
            yMotionFunction = MotionFunction.CreateYMotionFunction(Height,
                                                                   g,
                                                                   BallSize,
                                                                   ballIinitialLocation.Y,
                                                                   ballIinitialVelocity.Y);

            lastAfterimageId = 0;
        }

        public IEnumerable<Point> GetAfterImages(TimeSpan currentTime)
        {
            var newId = currentTime.Ticks / AfterimageInterval.Ticks;
            var fromId = Math.Max(lastAfterimageId + 1, newId - MaxAfterimageCount + 1);

            for (var i = fromId; i <= newId; i++)
            {
                var time = new TimeSpan(AfterimageInterval.Ticks * i);

                xPositions.Enqueue(xMotionFunction(time));
                yPositions.Enqueue(yMotionFunction(time));
            }

            lastAfterimageId = newId;

            return xPositions.Zip(yPositions, (x, y) => new Point(x, y));
        }

        public Point GetCurrentPosition(TimeSpan currentTime)
        {
            return new Point(xMotionFunction(currentTime), yMotionFunction(currentTime));
        }
    }
}
