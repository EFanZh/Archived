using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructOrUnionSpecifierWithoutDeclarations : StructOrUnionSpecifier
    {
        public StructOrUnionSpecifierWithoutDeclarations(StructOrUnion structOrUnion, Identifier identifier)
        {
            StructOrUnion = structOrUnion;
            Identifier = identifier;
        }

        public StructOrUnion StructOrUnion
        {
            get;
        }

        public Identifier Identifier
        {
            get;
        }

        public new static Parser<StructOrUnionSpecifierWithoutDeclarations> Parser
        {
            get;
        } = from structOrUnion in StructOrUnion.Parser
            from ignored in Parsers.Whitespaces
            from identifier in Identifier.Parser
            select new StructOrUnionSpecifierWithoutDeclarations(structOrUnion, identifier);
    }
}
