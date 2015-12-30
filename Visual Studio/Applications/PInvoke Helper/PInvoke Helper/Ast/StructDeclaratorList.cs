using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructDeclaratorList
    {
        public StructDeclaratorList(StructDeclarator[] declarators)
        {
            Declarators = declarators;
        }

        public StructDeclarator[] Declarators
        {
            get;
        }

        public static Parser<StructDeclaratorList> Parser
        {
            get;
        } = StructDeclarator.Parser.List(from ignored1 in Parsers.OptionalWhitespaces
                                         from ignored2 in Parsers.Character(',')
                                         from ignored3 in Parsers.OptionalWhitespaces
                                         select IgnoredResult.Value).Select(declarators => new StructDeclaratorList(declarators));
    }
}
