namespace PInvokeHelper.Parser
{
    internal class FunctionApplicaionExpression : Expression
    {
        public FunctionApplicaionExpression(Expression functionExpression, FunctionParameterList parameters)
        {
            FunctionExpression = functionExpression;
            Parameters = parameters;
        }

        public Expression FunctionExpression
        {
            get;
        }

        public FunctionParameterList Parameters
        {
            get;
        }

        public new static FunctionApplicaionExpression Parse(string input, ref int i)
        {
            var j = i;
            
            Helper.SkipWhitespaces(input, ref j);

            var firstFunctionExpression = NonFunctionApplicationExpression.Parse(input, ref j);

            if (firstFunctionExpression == null)
            {
                return null;
            }

            Helper.SkipWhitespaces(input, ref j);

            var parameters = FunctionParameterList.Parse(input, ref j);

            if (parameters == null)
            {
                return null;
            }

            var result = new FunctionApplicaionExpression(firstFunctionExpression, parameters);

            parameters = FunctionParameterList.Parse(input, ref j);

            while (parameters != null)
            {
                result = new FunctionApplicaionExpression(result, parameters);

                parameters = FunctionParameterList.Parse(input, ref j);
            }

            i = j;

            return result;
        }
    }
}
