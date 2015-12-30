using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructOrUnionSpecifier : TypeSpecifier
    {
        public new static Parser<StructOrUnionSpecifier> Parser
        {
            get;
        } = Combinators.Or<StructOrUnionSpecifier>(StructOrUnionSpecifierWithDeclarations.Parser,
                                                   StructOrUnionSpecifierWithoutDeclarations.Parser);
    }
}
