using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class Pointer
    {
        public Pointer(TypeQualifierList[] qualifierLists)
        {
            QualifierLists = qualifierLists;
        }

        public TypeQualifierList[] QualifierLists
        {
            get;
        }

        public static Parser<Pointer> Parser
        {
            get;
        } = (from ignored1 in Parsers.Character('*')
             from ignored2 in Parsers.OptionalWhitespaces
             from qualifierList in TypeQualifierList.Parser.Optional()
             select qualifierList
            ).List(Parsers.OptionalWhitespaces).Select(qualifierList => new Pointer(qualifierList));
    }
}
