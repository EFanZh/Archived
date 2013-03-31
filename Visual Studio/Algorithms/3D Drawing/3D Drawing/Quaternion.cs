namespace ThreeDDrawing
{
    internal class Quaternion
    {
        public Quaternion()
            : this(1.0, 0.0, 0.0, 0.0)
        {
        }

        public Quaternion(Quaternion q)
            : this(q.W, q.I, q.J, q.K)
        {
        }

        public Quaternion(double w, double i, double j, double k)
        {
            W = w;
            I = i;
            J = j;
            K = k;
        }

        public double W
        {
            get;
            set;
        }

        public double I
        {
            get;
            set;
        }

        public double J
        {
            get;
            set;
        }

        public double K
        {
            get;
            set;
        }

        public static Quaternion operator *(Quaternion x, Quaternion y)
        {
            return new Quaternion(x.W * y.W - x.I * y.I - x.J * y.J - x.K * y.K,
                                  x.W * y.I + x.I * y.W + x.J * y.K - x.K * y.J,
                                  x.W * y.J - x.I * y.K + x.J * y.W + x.K * y.I,
                                  x.W * y.K + x.I * y.J - x.J * y.I + x.K * y.W);
        }

        public Quaternion GetConjugate()
        {
            return new Quaternion(W, -I, -J, -K);
        }
    }
}
