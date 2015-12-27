using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ExtendedDeclModifierSeq
    {
        public ExtendedDeclModifierSeq(ExtendedDeclModifier[] modifiers)
        {
            Modifiers = modifiers;
        }

        public ExtendedDeclModifier[] Modifiers
        {
            get;
        }

        public static Parser<ExtendedDeclModifierSeq> Parser
        {
            get;
        } = from result in ExtendedDeclModifier.Parser.OptionalList(Parsers.Whitespaces)
            select new ExtendedDeclModifierSeq(result);
    }
}
