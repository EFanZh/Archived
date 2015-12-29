using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumerationConstantEnumeratorWithValue : Enumerator
    {
        public EnumerationConstantEnumeratorWithValue(EnumerationConstant enumerationConstant, ConstantExpression value)
        {
            EnumerationConstant = enumerationConstant;
            Value = value;
        }

        public EnumerationConstant EnumerationConstant
        {
            get;
        }

        public ConstantExpression Value
        {
            get;
        }

        public new static Parser<EnumerationConstantEnumeratorWithValue> Parser
        {
            get;
        } = from enumerationConstant in EnumerationConstant.Parser
            from ignored1 in Parsers.OptionalWhitespaces
            from ignored2 in Parsers.Character('=')
            from ignored3 in Parsers.OptionalWhitespaces
            from value in ConstantExpression.Parser
            select new EnumerationConstantEnumeratorWithValue(enumerationConstant, value);
    }
}
