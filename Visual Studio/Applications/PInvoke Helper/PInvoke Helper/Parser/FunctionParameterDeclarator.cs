namespace PInvokeHelper.Parser
{
    internal class FunctionParameterDeclarator
    {
        public FunctionParameterDeclarator(FunctionParameterAnnotation annotation, Declarator declarator)
        {
            Annotation = annotation;
            Declarator = declarator;
        }

        public FunctionParameterAnnotation Annotation
        {
            get;
        }

        public Declarator Declarator
        {
            get;
        }

        public static FunctionParameterDeclarator Parse(string input, ref int i)
        {
            var j = i;

            var annotation = FunctionParameterAnnotation.Parse(input, ref j);

            Helper.SkipWhitespaces(input, ref j);

            var declarator = Declarator.Parse(input, ref j);

            if (declarator != null)
            {
                i = j;

                return new FunctionParameterDeclarator(annotation, declarator);
            }
            else
            {
                return null;
            }
        }
    }
}
