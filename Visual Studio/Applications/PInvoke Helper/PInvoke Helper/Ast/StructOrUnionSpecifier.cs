using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructOrUnionSpecifier : TypeSpecifier
    {
        public new static Parser<StructOrUnionSpecifier> Parser
        {
            get;
        }
    }
}
