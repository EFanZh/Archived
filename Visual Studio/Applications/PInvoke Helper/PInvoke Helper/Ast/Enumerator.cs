using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class Enumerator
    {
        public static Parser<Enumerator> Parser
        {
            get;
        } = Combinators.Or<Enumerator>(EnumerationConstantEnumerator.Parser,
                                       EnumerationConstantEnumeratorWithValue.Parser);
    }
}
