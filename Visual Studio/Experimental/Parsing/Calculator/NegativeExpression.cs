using System.Collections.Generic;

namespace Calculator
{
    internal class NegativeExpression : IExpression
    {
        public NegativeExpression(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression
        {
            get;
            private set;
        }

        #region IExpression Members

        public double Evaluate(Dictionary<string, double> identifier_table)
        {
            return -Expression.Evaluate(identifier_table);
        }

        #endregion IExpression Members

        public override string ToString()
        {
            return string.Format("(-{0})", Expression.ToString());
        }
    }
}
