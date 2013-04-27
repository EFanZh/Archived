using System;

namespace LambdaRecursion
{
    internal class Program
    {
        private delegate T SelfDelegate<T>(SelfDelegate<T> self);

        private static T CallSelf<T>(SelfDelegate<T> self)
        {
            return self(self);
        }

        // https://en.wikipedia.org/wiki/Fixed-point_combinator#Y_combinator
        // Y combinator:
        // Y = λf.(λx.f (x x)) (λx.f (x x))
        // For call-by-value languages, we should use Z combinator.
        private static Func<T, TResult> Y<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
        {
            return CallSelf<Func<T, TResult>>(x => f(x(x)));
        }

        // Z combinator:
        // Y = λf.(λx.f (λv.((x x) v))) (λx.f (λv.((x x) v)))
        private static Func<T, TResult> Z<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
        {
            return CallSelf<Func<T, TResult>>(x => f(v => x(x)(v)));
        }

        private static void Main(string[] args)
        {
            var fact = Z<int, long>(self => (n => n == 0 ? 1 : n * self(n - 1)));
            Console.WriteLine(fact(10));
        }
    }
}
