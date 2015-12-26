namespace PInvokeHelper.Parser
{
    internal class FunctionParameterAnnotation
    {
        public FunctionParameterAnnotation(FunctionParameterAnnotationType type)
        {
            Type = type;
        }

        private FunctionParameterAnnotationType Type
        {
            get;
        }

        public static FunctionParameterAnnotation Parse(string input, ref int i)
        {
            var j = i;

            var typeString = Helper.ParseIdentifier(input, ref j);
            FunctionParameterAnnotationType type;

            switch (typeString)
            {
                case "_In_":
                    type = FunctionParameterAnnotationType.In;
                    break;

                case "_In_opt_":
                    type = FunctionParameterAnnotationType.InOptional;
                    break;

                default:
                    return null;
            }

            i = j;

            return new FunctionParameterAnnotation(type);
        }
    }
}
