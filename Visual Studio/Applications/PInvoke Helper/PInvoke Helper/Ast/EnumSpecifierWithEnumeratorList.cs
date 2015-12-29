using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumSpecifierWithEnumeratorList : EnumSpecifier
    {
        public EnumSpecifierWithEnumeratorList(Identifier identifier, EnumeratorList enumerators)
        {
            Identifier = identifier;
            Enumerators = enumerators;
        }

        public Identifier Identifier
        {
            get;
        }

        public EnumeratorList Enumerators
        {
            get;
        }

        public new static Parser<EnumSpecifierWithEnumeratorList> Parser
        {
            get;
        } = from ignored1 in Parsers.String("enum")
            from ignored2 in Parsers.OptionalWhitespaces
            from identifier in Identifier.Parser.Optional()
            from ignored3 in Parsers.OptionalWhitespaces
            from ignored4 in Parsers.Character('{')
            from ignored5 in Parsers.OptionalWhitespaces
            from enumeratorList in EnumeratorList.Parser
            from ignored6 in Parsers.OptionalWhitespaces
            from ignored7 in Parsers.Character('}')
            select new EnumSpecifierWithEnumeratorList(identifier, enumeratorList);
    }
}
