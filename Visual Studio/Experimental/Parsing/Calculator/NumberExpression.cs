using System.Collections.Generic;

namespace Calculator
{
    internal class NumberExpression : IExpression
    {
        public NumberExpression(double number)
        {
            Number = number;
        }

        public double Number
        {
            get;
            private set;
        }

        #region IExpression Members

        public double Evaluate(Dictionary<string, double> identifier_table)
        {
            return Number;
        }

        #endregion IExpression Members

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
