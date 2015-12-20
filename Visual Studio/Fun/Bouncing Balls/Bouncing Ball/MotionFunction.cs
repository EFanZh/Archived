using System;

namespace BouncingBall
{
    internal static class MotionFunction
    {
        private static double PositiveMod(double x, double y)
        {
            return (x % y + y) % y;
        }

        public static Func<TimeSpan, double> CreateXMotionFunction(double sceneWidth, double ballSize, double ballInitialLocationX, double ballInitialVelocityX)
        {
            var halfP = sceneWidth - ballSize;
            var p = halfP * 2.0;

            return t =>
                   {
                       var x = PositiveMod(ballInitialLocationX + ballInitialVelocityX * t.TotalSeconds, p);

                       if (x > halfP)
                       {
                           x = p - x;
                       }

                       return x;
                   };
        }

        public static Func<TimeSpan, double> CreateYMotionFunction(double sceneHeight, double g, double ballSize, double ballInitialLocationY, double ballInitialVelocityY)
        {
            var e0 = g * ballInitialLocationY + ballInitialVelocityY * ballInitialVelocityY * 0.5;
            var eTop = g * (sceneHeight - ballSize);
            var minV = e0 < eTop ? 0.0 : Math.Sqrt((e0 - eTop) * 2.0);
            var maxV = Math.Sqrt(e0 * 2.0);
            var offset = (ballInitialVelocityY + maxV) / g; // (v0 - (-maxV)) / g
            var p = (maxV - minV) * 2.0 / g;

            return t =>
                   {
                       var x = (t.TotalSeconds + offset) % p - p * 0.5;
                       var v = x * g + (x < 0 ? -minV : minV);

                       return (e0 - v * v * 0.5) / g;
                   };
        }
    }
}
