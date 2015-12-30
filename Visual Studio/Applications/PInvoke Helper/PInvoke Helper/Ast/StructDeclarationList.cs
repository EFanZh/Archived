using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructDeclarationList
    {
        public StructDeclarationList(StructDeclaration[] declarations)
        {
            Declarations = declarations;
        }

        public StructDeclaration[] Declarations
        {
            get;
        }

        public static Parser<StructDeclarationList> Parser
        {
            get;
        } = StructDeclaration.Parser.List(Parsers.OptionalWhitespaces).Select(declarations => new StructDeclarationList(declarations));
    }
}
