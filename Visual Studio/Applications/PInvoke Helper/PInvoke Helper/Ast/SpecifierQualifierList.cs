using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class SpecifierQualifierList
    {
        public SpecifierQualifierList(SpecifierQualifier[] specifierQualifiers)
        {
            SpecifierQualifiers = specifierQualifiers;
        }

        public SpecifierQualifier[] SpecifierQualifiers
        {
            get;
        }

        public static Parser<SpecifierQualifierList> Parser
        {
            get;
        } = SpecifierQualifier.Parser.List(Parsers.Whitespaces).Select(specifierQualifiers => new SpecifierQualifierList(specifierQualifiers));
    }
}
