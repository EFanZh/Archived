using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructOrUnionSpecifierWithDeclarations : StructOrUnionSpecifier
    {
        public StructOrUnionSpecifierWithDeclarations(StructOrUnion structOrUnion, Identifier identifier, StructDeclarationList declarations)
        {
            StructOrUnion = structOrUnion;
            Identifier = identifier;
            Declarations = declarations;
        }

        public StructOrUnion StructOrUnion
        {
            get;
        }

        public Identifier Identifier
        {
            get;
        }

        public StructDeclarationList Declarations
        {
            get;
        }

        public new static Parser<StructOrUnionSpecifierWithDeclarations> Parser
        {
            get;
        } = from structOrUnion in StructOrUnion.Parser
            from identifier in (from ignored in Parsers.Whitespaces
                                from identifier in Identifier.Parser.Optional()
                                select identifier).Optional()
            from ignored1 in Parsers.OptionalWhitespaces
            from ignored2 in Parsers.Character('{')
            from declarations in StructDeclarationList.Parser
            from ignored3 in Parsers.OptionalWhitespaces
            from ignored4 in Parsers.Character('}')
            select new StructOrUnionSpecifierWithDeclarations(structOrUnion, identifier, declarations);
    }
}
