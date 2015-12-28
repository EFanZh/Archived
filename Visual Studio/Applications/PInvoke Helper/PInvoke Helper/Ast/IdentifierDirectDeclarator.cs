using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class IdentifierDirectDeclarator : DirectDeclarator
    {
        public IdentifierDirectDeclarator(Identifier identifier)
        {
            Identifier = identifier;
        }

        public Identifier Identifier
        {
            get;
        }

        public new static Parser<IdentifierDirectDeclarator> Parser
        {
            get;
        } = Identifier.Parser.Select(identifier => new IdentifierDirectDeclarator(identifier));
    }
}
