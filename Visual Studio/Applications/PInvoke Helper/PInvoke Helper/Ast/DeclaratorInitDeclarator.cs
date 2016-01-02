using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DeclaratorInitDeclarator : InitDeclarator
    {
        public DeclaratorInitDeclarator(Declarator declarator)
        {
            Declarator = declarator;
        }

        public Declarator Declarator
        {
            get;
        }

        public new static Parser<DeclaratorInitDeclarator> Parser
        {
            get;
        } = Declarator.Parser.Select(declarator => new DeclaratorInitDeclarator(declarator));
    }
}
