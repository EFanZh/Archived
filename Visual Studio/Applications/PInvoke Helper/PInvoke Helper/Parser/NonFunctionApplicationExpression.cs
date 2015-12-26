namespace PInvokeHelper.Parser
{
    internal class NonFunctionApplicationExpression : Expression
    {
        public new static NonFunctionApplicationExpression Parse(string input, ref int i)
        {
            var j = i;

            NonFunctionApplicationExpression expression = SymbolExpression.Parse(input, ref j);

            if (expression != null)
            {
                i = j;
            }

            return expression;
        }
    }
}
