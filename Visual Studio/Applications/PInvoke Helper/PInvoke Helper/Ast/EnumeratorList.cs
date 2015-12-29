using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumeratorList
    {
        public EnumeratorList(Enumerator[] enumerators)
        {
            Enumerators = enumerators;
        }

        public Enumerator[] Enumerators
        {
            get;
        }

        public static Parser<EnumeratorList> Parser
        {
            get;
        } = Enumerator.Parser.List(from ignored1 in Parsers.OptionalWhitespaces
                                   from ignored2 in Parsers.Character(',')
                                   from ignored3 in Parsers.OptionalWhitespaces
                                   select IgnoredResult.Value).Select(enumerators => new EnumeratorList(enumerators));
    }
}
