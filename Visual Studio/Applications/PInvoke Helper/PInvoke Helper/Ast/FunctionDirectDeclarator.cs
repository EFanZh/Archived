using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class FunctionDirectDeclarator : DirectDeclarator
    {
        public FunctionDirectDeclarator(DirectDeclarator function, ParameterTypeList parameters)
        {
            Function = function;
            Parameters = parameters;
        }

        public DirectDeclarator Function
        {
            get;
        }

        public ParameterTypeList Parameters
        {
            get;
        }

        public new static Parser<FunctionDirectDeclarator> Parser
        {
            get;
        } = from function in DirectDeclarator.Parser
            from ignored1 in Parsers.OptionalWhitespaces
            from ignored2 in Parsers.Character('(')
            from ignored3 in Parsers.OptionalWhitespaces
            from parameters in ParameterTypeList.Parser
            from ignored4 in Parsers.OptionalWhitespaces
            from ignored5 in Parsers.Character(')')
            select new FunctionDirectDeclarator(function, parameters);
    }
}
