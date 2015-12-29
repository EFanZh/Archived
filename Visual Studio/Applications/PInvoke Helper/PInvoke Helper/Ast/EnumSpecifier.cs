using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumSpecifier : TypeSpecifier
    {
        public new static Parser<EnumSpecifier> Parser
        {
            get;
        } = Combinators.Or<EnumSpecifier>(EnumSpecifierWithEnumeratorList.Parser,
                                          EnumSpecifierWithoutEnumeratorList.Parser);
    }
}
