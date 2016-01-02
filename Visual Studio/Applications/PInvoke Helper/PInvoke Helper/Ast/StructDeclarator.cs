using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructDeclarator
    {
        public static Parser<StructDeclarator> Parser
        {
            get;
        } = Combinators.Or<StructDeclarator>(DeclaratorStructDeclarator.Parser,
                                             BitFieldStructDeclarator.Parser);
    }
}
