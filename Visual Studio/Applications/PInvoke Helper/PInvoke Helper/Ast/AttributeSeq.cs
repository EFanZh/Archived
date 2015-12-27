using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class AttributeSeq
    {
        public AttributeSeq(Attribute[] attributes)
        {
            Attributes = attributes;
        }

        public Attribute[] Attributes
        {
            get;
        }

        public static Parser<AttributeSeq> Parser
        {
            get;
        } = Attribute.Parser.List(Parsers.Whitespaces).Select(attributes => new AttributeSeq(attributes));
    }
}
