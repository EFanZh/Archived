using System.Collections.Generic;

namespace Calculator
{
    internal class IdentifierExpression : IExpression
    {
        public IdentifierExpression(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier
        {
            get;
            private set;
        }

        #region IExpression Members

        public double Evaluate(Dictionary<string, double> identifier_table)
        {
            return identifier_table[Identifier];
        }

        #endregion IExpression Members

        public override string ToString()
        {
            return Identifier;
        }
    }
}
