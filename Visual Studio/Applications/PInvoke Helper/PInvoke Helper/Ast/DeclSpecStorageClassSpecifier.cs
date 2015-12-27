using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class DeclSpecStorageClassSpecifier : StorageClassSpecifier
    {
        public DeclSpecStorageClassSpecifier(ExtendedDeclModifierSeq modifiers)
        {
            Modifiers = modifiers;
        }

        public ExtendedDeclModifierSeq Modifiers
        {
            get;
        }

        public new static Parser<DeclSpecStorageClassSpecifier> Parser
        {
            get;
        } = from ignored1 in Parsers.String("__declspec")
            from ignored2 in Parsers.OptionalWhitespaces
            from ignored3 in Parsers.Character('(')
            from ignored4 in Parsers.OptionalWhitespaces
            from modifiers in ExtendedDeclModifierSeq.Parser
            from ignored5 in Parsers.OptionalWhitespaces
            from ignored6 in Parsers.Character(')')
            select new DeclSpecStorageClassSpecifier(modifiers);
    }
}
