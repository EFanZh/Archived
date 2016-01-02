using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DeclaratorStructDeclarator : StructDeclarator
    {
        public DeclaratorStructDeclarator(Declarator declarator)
        {
            Declarator = declarator;
        }

        public Declarator Declarator
        {
            get;
        }

        public new static Parser<DeclaratorStructDeclarator> Parser
        {
            get;
        } = Declarator.Parser.Select(declarator => new DeclaratorStructDeclarator(declarator));
    }
}