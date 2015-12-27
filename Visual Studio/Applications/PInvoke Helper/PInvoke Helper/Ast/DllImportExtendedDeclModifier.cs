using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DllImportExtendedDeclModifier : ExtendedDeclModifier
    {
        public new static Parser<DllImportExtendedDeclModifier> Parser
        {
            get;
        } = Parsers.String("dllimport").Select(ignored => new DllImportExtendedDeclModifier());
    }
}
