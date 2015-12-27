using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ExtendedDeclModifier
    {
        public static Parser<ExtendedDeclModifier> Parser
        {
            get;
        } = Combinators.Or<ExtendedDeclModifier>(ThreadExtendedDeclModifier.Parser,
                                                 NakedExtendedDeclModifier.Parser,
                                                 DllImportExtendedDeclModifier.Parser,
                                                 DllExportExtendedDeclModifier.Parser);
    }
}
