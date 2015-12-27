using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class SpecifierQualifier : DeclarationSpecifier
    {
        public new static Parser<SpecifierQualifier> Parser
        {
            get;
        } = Combinators.Or<SpecifierQualifier>(TypeSpecifier.Parser,
                                               TypeQualifier.Parser);
    }
}
