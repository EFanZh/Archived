using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructDeclaration
    {
        public StructDeclaration(SpecifierQualifierList specifierQualifiers, StructDeclaratorList declarators)
        {
            SpecifierQualifiers = specifierQualifiers;
            Declarators = declarators;
        }

        public SpecifierQualifierList SpecifierQualifiers
        {
            get;
        }

        public StructDeclaratorList Declarators
        {
            get;
        }

        public static Parser<StructDeclaration> Parser
        {
            get;
        } = from specifierQualifiers in SpecifierQualifierList.Parser
            from ignored in Parsers.Whitespaces
            from declarators in StructDeclaratorList.Parser
            select new StructDeclaration(specifierQualifiers, declarators);
    }
}
