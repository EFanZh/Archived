using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class InitDeclaratorList
    {
        public InitDeclaratorList(InitDeclarator[] declarators)
        {
            Declarators = declarators;
        }

        public InitDeclarator[] Declarators
        {
            get;
        }

        public static Parser<InitDeclaratorList> Parser
        {
            get;
        } = InitDeclarator.Parser.List(from ignored1 in Parsers.OptionalWhitespaces
                                       from ignored2 in Parsers.Character(',')
                                       from ignored3 in Parsers.OptionalWhitespaces
                                       select IgnoredResult.Value).Select(declarators => new InitDeclaratorList(declarators));
    }
}
