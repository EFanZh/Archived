using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ThreadExtendedDeclModifier : ExtendedDeclModifier
    {
        public new static Parser<ThreadExtendedDeclModifier> Parser
        {
            get;
        } = Parsers.String("thread").Select(ignored => new ThreadExtendedDeclModifier());
    }
}
