using System.Collections.Generic;

namespace Calculator
{
    internal class PlusExpression : IExpression
    {
        public PlusExpression(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public IExpression Left
        {
            get;
            private set;
        }

        public IExpression Right
        {
            get;
            private set;
        }

        #region IExpression Members

        public double Evaluate(Dictionary<string, double> identifier_table)
        {
            return Left.Evaluate(identifier_table) + Right.Evaluate(identifier_table);
        }

        #endregion IExpression Members

        public override string ToString()
        {
            return string.Format("({0} + {1})", Left, Right);
        }
    }
}
