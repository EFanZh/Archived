using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ParenthesesDirectDeclarator : DirectDeclarator
    {
        public ParenthesesDirectDeclarator(Declarator declarator)
        {
            Declarator = declarator;
        }

        public Declarator Declarator
        {
            get;
        }

        public new static Parser<ParenthesesDirectDeclarator> Parser
        {
            get;
        } = from ignored1 in Parsers.Character('(')
            from ignored2 in Parsers.OptionalWhitespaces
            from declarator in Declarator.Parser
            from ignored3 in Parsers.OptionalWhitespaces
            from ignored4 in Parsers.Character(')')
            select new ParenthesesDirectDeclarator(declarator);
    }
}
