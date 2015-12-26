namespace PInvokeHelper.Parser
{
    internal class Declarator
    {
        public Declarator(string resultType, Expression callExpression)
        {
            ResultType = resultType;
            CallExpression = callExpression;
        }

        public string ResultType
        {
            get;
        }

        public Expression CallExpression
        {
            get;
        }

        public static Declarator Parse(string input, ref int i)
        {
            var j = i;

            var resultType = Helper.ParseIdentifier(input, ref j);

            if (resultType == null)
            {
                return null;
            }

            Helper.SkipWhitespaces(input, ref j);
            
            var callExpression = Expression.Parse(input, ref j);

            if (callExpression != null)
            {
                i = j;

                return new Declarator(resultType, callExpression);
            }
            else
            {
                return null;
            }
        }
    }
}
