using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class EnumerationConstantEnumerator : Enumerator
    {
        public EnumerationConstantEnumerator(EnumerationConstant enumerationConstant)
        {
            EnumerationConstant = enumerationConstant;
        }

        public EnumerationConstant EnumerationConstant
        {
            get;
        }

        public new static Parser<EnumerationConstantEnumerator> Parser
        {
            get;
        } = EnumerationConstant.Parser.Select(enumerationConstant => new EnumerationConstantEnumerator(enumerationConstant));
    }
}
