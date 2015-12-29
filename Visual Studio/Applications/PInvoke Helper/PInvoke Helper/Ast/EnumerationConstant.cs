using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumerationConstant
    {
        public EnumerationConstant(Identifier identifier)
        {
            Identifier = identifier;
        }

        public Identifier Identifier
        {
            get;
        }

        public static Parser<EnumerationConstant> Parser
        {
            get;
        } = Identifier.Parser.Select(identifier => new EnumerationConstant(identifier));
    }
}
