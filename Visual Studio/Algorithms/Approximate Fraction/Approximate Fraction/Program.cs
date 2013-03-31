using System;

namespace ApproximateFraction
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int a, b;
            GetApproximateFraction(1.4142135623730950488016887242097m, 1000000, out a, out b);
        }

        private static void GetApproximateFraction(decimal number, int max, out int numerator, out int denominator)
        {
            int a = 0, b = 0;
            decimal min = decimal.MaxValue;

            for (int i = 0; i < max; i++)
            {
                Console.Title = string.Format("{0}", i);
                for (int j = 0; j <= i; j++)
                {
                    a = j + 1;
                    b = i - j + 1;

                    if (GCD(a, b) == 1)
                    {
                        decimal t = (NumberLength(a) + NumberLength(b)) * Math.Abs((decimal)a / b - number);
                        if (t < min)
                        {
                            min = t;
                            Console.WriteLine("{0} / {1}", a, b);
                        }
                    }
                }
            }
            numerator = a;
            denominator = b;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        private static int NumberLength(int x)
        {
            int n = 0;
            while (x > 0)
            {
                n++;
                x /= 10;
            }
            return n == 0 ? 1 : n;
        }
    }
}
