using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ParameterDeclaration
    {
        public static Parser<ParameterDeclaration> Parser
        {
            get;
        } = Combinators.Or<ParameterDeclaration>(NamedParameterDeclaration.Parser,
            AnonymousParameterDeclaration.Parser);
    }

    internal class NamedParameterDeclaration : ParameterDeclaration
    {
        public NamedParameterDeclaration(DeclarationSpecifier specifiers, Declarator declarator)
        {
            Specifiers = specifiers;
            Declarator = declarator;
        }

        public DeclarationSpecifier Specifiers
        {
            get;
        }

        public Declarator Declarator
        {
            get;
        }
    }

    internal class AnonymousParameterDeclaration : ParameterDeclaration
    {
        public new static Parser<NamedParameterDeclaration> Parser
        {
            get;
        }
    }
}
