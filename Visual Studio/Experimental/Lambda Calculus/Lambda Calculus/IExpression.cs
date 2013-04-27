using System.Collections.Generic;

namespace LambdaCalculus
{
    internal interface IExpression
    {
        public IExpression Replace(VariableExpression old_expression, IExpression expression);

        public IEnumerable<VariableExpression> GetVariables();
    }
}
