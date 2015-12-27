using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumSpecifier : TypeSpecifier
    {
        public new static Parser<EnumSpecifier> Parser
        {
            get;
        }
    }
}
