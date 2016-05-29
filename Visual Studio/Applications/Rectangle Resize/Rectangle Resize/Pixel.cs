using TValue = System.Double;

namespace RectangleResize
{
    internal class Pixel
    {
        public Pixel(TValue c0, TValue c1, TValue c2, TValue c3)
        {
            C0 = c0;
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }

        public TValue C0
        {
            get;
        }

        public TValue C1
        {
            get;
        }

        public TValue C2
        {
            get;
        }

        public TValue C3
        {
            get;
        }

        public static Pixel operator +(Pixel lhs, Pixel rhs)
        {
            return new Pixel(lhs.C0 + rhs.C0, lhs.C1 + rhs.C1, lhs.C2 + rhs.C2, lhs.C3 + rhs.C3);
        }

        public static Pixel operator -(Pixel lhs, Pixel rhs)
        {
            return new Pixel(lhs.C0 - rhs.C0, lhs.C1 - rhs.C1, lhs.C2 - rhs.C2, lhs.C3 - rhs.C3);
        }

        public static Pixel operator *(Pixel lhs, int rhs)
        {
            return new Pixel(lhs.C0 * rhs, lhs.C1 * rhs, lhs.C2 * rhs, lhs.C3 * rhs);
        }

        public static Pixel operator *(Pixel lhs, double rhs)
        {
            return new Pixel(lhs.C0 * rhs, lhs.C1 * rhs, lhs.C2 * rhs, lhs.C3 * rhs);
        }

        public static Pixel operator /(Pixel lhs, int rhs)
        {
            return new Pixel(lhs.C0 / rhs, lhs.C1 / rhs, lhs.C2 / rhs, lhs.C3 / rhs);
        }

        public static Pixel operator /(Pixel lhs, double rhs)
        {
            return new Pixel(lhs.C0 / rhs, lhs.C1 / rhs, lhs.C2 / rhs, lhs.C3 / rhs);
        }

        public static Pixel Plus(Pixel lhs, Pixel rhs)
        {
            return lhs + rhs;
        }

        public static Pixel Multiply(Pixel lhs, int rhs)
        {
            return lhs * rhs;
        }

        public static Pixel Divide(Pixel lhs, int rhs)
        {
            return lhs / rhs;
        }
    }
}
