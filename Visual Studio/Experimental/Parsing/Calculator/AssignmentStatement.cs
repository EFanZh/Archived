using System.Collections.Generic;

namespace Calculator
{
    internal class AssignmentStatement : IStatement
    {
        public AssignmentStatement(string identifier, IExpression expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public string Identifier
        {
            get;
            private set;
        }

        public IExpression Expression
        {
            get;
            private set;
        }

        #region IStatement Members

        public double Evaluate(Dictionary<string, double> identifier_table)
        {
            double value = Expression.Evaluate(identifier_table);

            identifier_table[Identifier] = value;

            return value;
        }

        #endregion IStatement Members

        public override string ToString()
        {
            return string.Format("{0} = {1}", Identifier, Expression);
        }
    }
}
