using System.Collections.Generic;

namespace Calculator
{
    internal class ExpressionStatement : IStatement
    {
        public ExpressionStatement(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression
        {
            get;
            private set;
        }

        #region IStatement Members

        public double Evaluate(Dictionary<string, double> identifier_table)
        {
            return Expression.Evaluate(identifier_table);
        }

        #endregion IStatement Members

        public override string ToString()
        {
            return Expression.ToString();
        }
    }
}
