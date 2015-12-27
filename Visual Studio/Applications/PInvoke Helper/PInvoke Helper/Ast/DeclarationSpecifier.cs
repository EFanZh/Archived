using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DeclarationSpecifier
    {
        public static Parser<DeclarationSpecifier> Parser
        {
            get;
        } = Combinators.Or<DeclarationSpecifier>(StorageClassSpecifier.Parser,
                                                 SpecifierQualifier.Parser);
    }
}
