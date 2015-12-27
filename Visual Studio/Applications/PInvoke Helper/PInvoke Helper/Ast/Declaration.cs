using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class Declaration
    {
        public Declaration(DeclarationSpecifiers specifiers, AttributeSeq attributes, InitDeclaratorList initDeclarators)
        {
            Specifiers = specifiers;
            Attributes = attributes;
            InitDeclarators = initDeclarators;
        }

        public DeclarationSpecifiers Specifiers
        {
            get;
        }

        public AttributeSeq Attributes
        {
            get;
        }

        public InitDeclaratorList InitDeclarators
        {
            get;
        }

        public static Parser<Declaration> Parser
        {
            get;
        } = from specifiers in DeclarationSpecifiers.Parser
            from ignored1 in Parsers.OptionalWhitespaces
            from attributes in AttributeSeq.Parser.Optional()
            from ignored2 in Parsers.OptionalWhitespaces
            from initDeclarators in InitDeclaratorList.Parser.Optional()
            select new Declaration(specifiers, attributes, initDeclarators);
    }
}
