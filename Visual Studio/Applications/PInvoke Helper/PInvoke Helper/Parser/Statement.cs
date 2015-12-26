namespace PInvokeHelper.Parser
{
    internal class Statement
    {
        public Statement(MultipleDeclarator declarator)
        {
            Declarator = declarator;
        }

        public MultipleDeclarator Declarator
        {
            get;
        }

        public static Statement Parse(string input, ref int i)
        {
            var j = i;

            var declarator = MultipleDeclarator.Parse(input, ref j);

            if (declarator == null)
            {
                return null;
            }

            Helper.SkipWhitespaces(input, ref j);

            if (Helper.ParseString(input, ref j, ";"))
            {
                i = j;

                return new Statement(declarator);
            }
            else
            {
                return null;
            }
        }
    }
}
