using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class InitDeclarator
    {
        public static Parser<InitDeclarator> Parser
        {
            get;
        } = Combinators.Or<InitDeclarator>(DeclaratorInitDeclarator.Parser,
                                           ScalarInitializationDeclarator.Parser);
    }
}
