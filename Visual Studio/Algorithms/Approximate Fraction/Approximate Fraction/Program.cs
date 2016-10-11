using System;

namespace ApproximateFraction
{
    internal class Program
    {
        private const decimal pi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535940812848111745028410270193852110555964462294895493038196442881097566593344612847564823378678316527120190914564856692346034861045432664821339360726024914127372458700660631558817488152092096282925409171536436789259036m;

        private static void Main(string[] args)
        {
            int a, b;
            GetApproximateFraction((decimal)Math.Sqrt(3), int.MaxValue, out a, out b);
        }

        private static void GetApproximateFraction(decimal number, int max, out int numerator, out int denominator)
        {
            int a = 0, b = 0;
            decimal min = decimal.MaxValue;

            for (int i = 0; i < max; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.Title = string.Format("{0}", i);
                }

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
