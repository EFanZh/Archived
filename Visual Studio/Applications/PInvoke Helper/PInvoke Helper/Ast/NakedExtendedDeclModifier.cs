using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class NakedExtendedDeclModifier : ExtendedDeclModifier
    {
        public new static Parser<NakedExtendedDeclModifier> Parser
        {
            get;
        } = Parsers.String("naked").Select(ignored => new NakedExtendedDeclModifier());
    }
}
