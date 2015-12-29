using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ParameterTypeList
    {
        public ParameterTypeList(ParameterList parameters, bool hasEllipsis)
        {
            Parameters = parameters;
            HasEllipsis = hasEllipsis;
        }

        public ParameterList Parameters
        {
            get;
        }

        public bool HasEllipsis
        {
            get;
        }

        public static Parser<ParameterTypeList> Parser
        {
            get;
        } = from parameters in ParameterList.Parser
            from ellipsis in (from ignored1 in Parsers.OptionalWhitespaces
                              from ignored2 in Parsers.Character(',')
                              from ignored3 in Parsers.OptionalWhitespaces
                              from ignored4 in Parsers.String("...")
                              select true).Optional()
            select new ParameterTypeList(parameters, ellipsis);
    }
}
