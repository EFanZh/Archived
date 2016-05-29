using System;

namespace RectangleResize
{
    internal class MathOperators<T, TSize>
    {
        public MathOperators(Func<T, T, T> plus, Func<T, TSize, T> multiply, Func<T, TSize, T> divide)
        {
            Plus = plus;
            Multiply = multiply;
            Divide = divide;
        }

        public Func<T, T, T> Plus
        {
            get;
        }

        public Func<T, TSize, T> Multiply
        {
            get;
        }

        public Func<T, TSize, T> Divide
        {
            get;
        }
    }
}
