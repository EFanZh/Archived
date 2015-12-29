using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ParameterList
    {
        public ParameterList(ParameterDeclaration[] declarations)
        {
            Declarations = declarations;
        }

        public ParameterDeclaration[] Declarations
        {
            get;
        }

        public static Parser<ParameterList> Parser
        {
            get;
        } = ParameterDeclaration.Parser.List(from ignored1 in Parsers.OptionalWhitespaces
                                             from ignored2 in Parsers.Character(',')
                                             from ignored3 in Parsers.OptionalWhitespaces
                                             select IgnoredResult.Value).Select(declarations => new ParameterList(declarations));
    }
}
