using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class FunctionExpression : IExpression
    {
        public FunctionExpression(VariableExpression token, IExpression body)
        {
            Variable = token;
            Body = body;
        }

        public VariableExpression Variable
        {
            get;
            private set;
        }

        public IExpression Body
        {
            get;
            private set;
        }

        public IExpression Convert(IExpression expression)
        {
            return Body.Replace(Variable, expression);
        }

        #region IExpression Members

        public IExpression Replace(VariableExpression old_expression, IExpression expression)
        {
            // ?
            return null;
        }

        public IEnumerable<VariableExpression> GetVariables()
        {
            return Body.GetVariables().Concat(new[] { Variable }).Distinct();
        }

        #endregion IExpression Members
    }
}
