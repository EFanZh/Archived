using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DllExportExtendedDeclModifier : ExtendedDeclModifier
    {
        public new static Parser<DllExportExtendedDeclModifier> Parser
        {
            get;
        } = Parsers.String("dllexport").Select(ignored => new DllExportExtendedDeclModifier());
    }
}
