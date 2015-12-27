using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ScalarInitializationDeclarator : InitDeclarator
    {
        public new static Parser<ScalarInitializationDeclarator> Parser
        {
            get;
        }
    }
}
