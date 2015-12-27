using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DeclarationSpecifiers
    {
        public DeclarationSpecifiers(DeclarationSpecifier[] specifiers)
        {
            Specifiers = specifiers;
        }

        public DeclarationSpecifier[] Specifiers
        {
            get;
        }

        public static Parser<DeclarationSpecifiers> Parser
        {
            get;
        } = DeclarationSpecifier.Parser.List(Parsers.Whitespaces).Select(specifiers => new DeclarationSpecifiers(specifiers));
    }
}
