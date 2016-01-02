using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class BitFieldStructDeclarator : StructDeclarator
    {
        public BitFieldStructDeclarator(TypeSpecifier typeSpecifier, Declarator declarator, ConstantExpression width)
        {
            TypeSpecifier = typeSpecifier;
            Declarator = declarator;
            Width = width;
        }

        public TypeSpecifier TypeSpecifier
        {
            get;
        }

        public Declarator Declarator
        {
            get;
        }

        public ConstantExpression Width
        {
            get;
        }

        public new static Parser<BitFieldStructDeclarator> Parser
        {
            get;
        } = from typeSpecifier in TypeSpecifier.Parser
            from declarator in (from ignored in Parsers.Whitespaces
                                from declarator in Declarator.Parser
                                select declarator).Optional()
            from ignored1 in Parsers.OptionalWhitespaces
            from ignored2 in Parsers.Character(':')
            from ignored3 in Parsers.OptionalWhitespaces
            from width in ConstantExpression.Parser
            select new BitFieldStructDeclarator(typeSpecifier, declarator, width);
    }
}