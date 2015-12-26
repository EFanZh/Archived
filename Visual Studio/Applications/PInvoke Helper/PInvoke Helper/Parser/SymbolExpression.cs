namespace PInvokeHelper.Parser
{
    internal class SymbolExpression : NonFunctionApplicationExpression
    {
        public SymbolExpression(FunctionCallConvention callConvention, string symbol)
        {
            CallConvention = callConvention;
            Symbol = symbol;
        }

        public FunctionCallConvention CallConvention
        {
            get;
        }

        public string Symbol
        {
            get;
        }

        public new static SymbolExpression Parse(string input, ref int i)
        {
            var j = i;

            var callConvention = FunctionCallConvention.Parse(input, ref j);

            Helper.SkipWhitespaces(input, ref j);

            var identifier = Helper.ParseIdentifier(input, ref j);

            if (identifier != null)
            {
                i = j;

                return new SymbolExpression(callConvention, identifier);
            }
            else
            {
                return null;
            }
        }
    }
}
