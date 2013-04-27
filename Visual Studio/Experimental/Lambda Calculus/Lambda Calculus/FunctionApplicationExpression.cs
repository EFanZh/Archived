using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class FunctionApplicationExpression : IExpression
    {
        public FunctionApplicationExpression(IExpression function, IExpression parameter)
        {
            Function = function;
            Parameter = parameter;
        }

        public IExpression Function
        {
            get;
            private set;
        }

        public IExpression Parameter
        {
            get;
            private set;
        }

        public IExpression Apply()
        {
            if (Function is FunctionExpression)
            {
                return (Function as FunctionExpression).Convert(Parameter);
            }
            else
            {
                return this;
            }
        }

        #region IExpression Members

        public IExpression Replace(VariableExpression old_expression, IExpression expression)
        {
            return new FunctionApplicationExpression(Function.Replace(old_expression, expression), Parameter.Replace(old_expression, expression));
        }

        public IEnumerable<VariableExpression> GetVariables()
        {
            return Function.GetVariables().Concat(Parameter.GetVariables()).Distinct();
        }

        #endregion IExpression Members
    }
}
