using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaRecursion
{
    internal class Program
    {
        private delegate Func<T, R> Self<T, R>(Self<T, R> self);

        // Y=\f.(\x.f (x x))(\x.f (x x))
        private static Func<T, R> Y<T, R>(Func<Func<T, R>, T, R> f)
        {
            Self<T, R> self = x => t => f(x(x), t);
            return t => f(self(self), t);
        }

        private static Func<T0, T1, R> Y<T0, T1, R>(Func<Func<T0, T1, R>, T0, T1, R> f)
        {
            return (s0, s1) =>
                Y<Tuple<T0, T1>, R>(
                    (self, n) => f(
                        (t0, t1) => self(Tuple.Create(t0, t1)),
                        n.Item1,
                        n.Item2
                        )
                    )
                (Tuple.Create(s0, s1));
        }

        private static Func<T0, T1, T2, R> Y<T0, T1, T2, R>(Func<Func<T0, T1, T2, R>, T0, T1, T2, R> f)
        {
            return (s0, s1, s2) =>
                Y<Tuple<T0, T1, T2>, R>(
                    (self, n) => f(
                        (t0, t1, t2) => self(Tuple.Create(t0, t1, t2)),
                        n.Item1,
                        n.Item2,
                        n.Item3
                        )
                    )
                (Tuple.Create(s0, s1, s2));
        }

        private static Func<T0, T1, T2, T3, R> Y<T0, T1, T2, T3, R>(Func<Func<T0, T1, T2, T3, R>, T0, T1, T2, T3, R> f)
        {
            return (s0, s1, s2, s3) =>
                Y<Tuple<T0, T1, T2, T3>, R>(
                    (self, n) => f(
                        (t0, t1, t2, t3) => self(Tuple.Create(t0, t1, t2, t3)),
                        n.Item1,
                        n.Item2,
                        n.Item3,
                        n.Item4
                        )
                    )
                (Tuple.Create(s0, s1, s2, s3));
        }

        private static void Main(string[] args)
        {
            var fab = Y<int, int>((self, n) => n <= 2 ? 1 : self(n - 1) + self(n - 2));
            for (int i = 1; i <= 100; i++)
            {
                Console.Write(fab(i) + " ");
            }
        }
    }
}
