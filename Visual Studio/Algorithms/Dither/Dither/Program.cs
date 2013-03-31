using System;

namespace Dither
{
    internal class Program
    {
        private static Random random = new Random();

        private static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(GetDitheredValue(0.75));
            }
        }

        private static int GetDitheredValue(double x)
        {
            int i = (int)x;
            double xd = x - i;
            return random.NextDouble() < xd ? i + 1 : i;
        }
    }
}
