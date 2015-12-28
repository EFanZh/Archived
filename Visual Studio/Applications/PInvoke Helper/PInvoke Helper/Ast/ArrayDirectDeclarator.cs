using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ArrayDirectDeclarator : DirectDeclarator
    {
        public ArrayDirectDeclarator(DirectDeclarator array, ConstantExpression subscript)
        {
            Array = array;
            Subscript = subscript;
        }

        public DirectDeclarator Array
        {
            get;
        }

        public ConstantExpression Subscript
        {
            get;
        }

        public new static Parser<ArrayDirectDeclarator> Parser
        {
            get;
        } = from array in DirectDeclarator.Parser
            from ignored1 in Parsers.OptionalWhitespaces
            from ignored2 in Parsers.Character('[')
            from ignored3 in Parsers.OptionalWhitespaces
            from subscript in ConstantExpression.Parser
            from ignored4 in Parsers.OptionalWhitespaces
            from ignored5 in Parsers.Character(']')
            select new ArrayDirectDeclarator(array, subscript);
    }
}
