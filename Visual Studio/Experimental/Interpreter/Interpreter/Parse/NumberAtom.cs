namespace Interpreter.Parse
{
    internal class NumberAtom : IAtom
    {
        public NumberAtom(double number)
        {
            Number = number;
        }

        public double Number
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
