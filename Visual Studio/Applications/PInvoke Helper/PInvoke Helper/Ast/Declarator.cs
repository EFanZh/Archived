using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class Declarator : InitDeclarator
    {
        public Declarator(Pointer pointer, DirectDeclarator directDeclarator)
        {
            Pointer = pointer;
            DirectDeclarator = directDeclarator;
        }

        public Pointer Pointer
        {
            get;
        }

        public DirectDeclarator DirectDeclarator
        {
            get;
        }

        public new static Parser<Declarator> Parser
        {
            get;
        } = from pointer in Pointer.Parser.Optional()
            from directDeclarator in DirectDeclarator.Parser
            select new Declarator(pointer, directDeclarator);
    }
}
