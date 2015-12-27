using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class TypeDefName : TypeSpecifier
    {
        public new static Parser<TypeDefName> Parser
        {
            get;
        }
    }
}
