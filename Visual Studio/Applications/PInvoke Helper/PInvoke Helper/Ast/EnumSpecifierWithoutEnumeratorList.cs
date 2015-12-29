using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumSpecifierWithoutEnumeratorList : EnumSpecifier
    {
        public EnumSpecifierWithoutEnumeratorList(Identifier identifier)
        {
            Identifier = identifier;
        }

        public Identifier Identifier
        {
            get;
        }

        public new static Parser<EnumSpecifierWithoutEnumeratorList> Parser
        {
            get;
        } = from ignored1 in Parsers.String("enum")
            from ignored2 in Parsers.OptionalWhitespaces
            from identifier in Identifier.Parser
            select new EnumSpecifierWithoutEnumeratorList(identifier);
    }
}
