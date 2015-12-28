using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DirectDeclarator
    {
        public static Parser<DirectDeclarator> Parser
        {
            get;
        } = Combinators.Or<DirectDeclarator>(IdentifierDirectDeclarator.Parser,
                                             ParenthesesDirectDeclarator.Parser,
                                             ArrayDirectDeclarator.Parser,
                                             FunctionDirectDeclarator.Parser);
    }
}
