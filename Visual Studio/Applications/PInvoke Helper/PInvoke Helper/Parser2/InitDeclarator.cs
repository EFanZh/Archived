namespace PInvokeHelper.Parser2
{
    internal class InitDeclarator
    {
        public InitDeclarator(Declarator declarator)
        {
            Declarator = declarator;
        }

        public Declarator Declarator
        {
            get;
        }

        public static InitDeclarator Parse(string input, ref int index)
        {
            var declarator = Declarator.Parse(input, ref index);

            return declarator != null ? new InitDeclarator(declarator) : null;

            // = initializer.
        }
    }
}
