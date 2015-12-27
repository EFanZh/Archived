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
        } = ExtendedDeclModifier.Parser.OptionalList(Parsers.Whitespaces).Select(modifiers => new ExtendedDeclModifierSeq(modifiers));
    }
}
