using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class TypeQualifierList
    {
        public TypeQualifierList(TypeQualifier[] qualifiers)
        {
            Qualifiers = qualifiers;
        }

        public TypeQualifier[] Qualifiers
        {
            get;
        }

        public static Parser<TypeQualifierList> Parser
        {
            get;
        } = TypeQualifier.Parser.List(Parsers.Whitespaces).Select(qualifiers => new TypeQualifierList(qualifiers));
    }
}
