using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PInvokeHelper.Parser
{
    class MultipleDeclarator
    {
        public MultipleDeclarator(string resultType, Expression[] callExpressions)
        {
            ResultType = resultType;
            CallExpressions = callExpressions;
        }

        public string ResultType
        {
            get;
        }

        public Expression[] CallExpressions
        {
            get;
        }

        public static MultipleDeclarator Parse(string input, ref int i)
        {
            var j = i;

            var resultType = Helper.ParseIdentifier(input, ref j);

            if (resultType == null)
            {
                return null;
            }

            Helper.SkipWhitespaces(input, ref j);

            var callExpressions = new List<Expression>();
            var callExpression = Expression.Parse(input, ref j);
            
            if (callExpression != null)
            {
                callExpressions.Add(callExpression);

                Helper.SkipWhitespaces(input, ref j);

                while (Helper.ParseString(input, ref j, ","))
                {
                    callExpression = Expression.Parse(input, ref j);

                    if (callExpression == null)
                    {
                        return null;
                    }
                    else
                    {
                        callExpressions.Add(callExpression);
                    }

                    Helper.SkipWhitespaces(input, ref j);
                }

                i = j;
                
                return new MultipleDeclarator(resultType, callExpressions.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}
