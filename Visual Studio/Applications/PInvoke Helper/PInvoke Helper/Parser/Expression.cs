using System.Collections.Generic;

namespace PInvokeHelper.Parser
{
    internal class Expression
    {
        public ModifierType[] Modifiers
        {
            get;
        }

        public static Expression Parse(string input, ref int i)
        {
            var j = i;

            var expression = FunctionApplicaionExpression.Parse(input, ref j) ??
                             (Expression)SymbolExpression.Parse(input, ref j);
            
            if (expression != null)
            {
                i = j;

                return expression;
            }
            else
            {
                return null;
            }
        }
    }
}
