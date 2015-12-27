using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class InitDeclarator
    {
        public static Parser<InitDeclarator> Parser
        {
            get;
        } = Combinators.Or<InitDeclarator>(Declarator.Parser,
                                           ScalarInitializationDeclarator.Parser);
    }
}
