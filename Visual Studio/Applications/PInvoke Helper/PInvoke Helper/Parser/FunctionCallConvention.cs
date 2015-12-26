namespace PInvokeHelper.Parser
{
    internal class FunctionCallConvention
    {
        public FunctionCallConvention(FunctionCallConventionType type)
        {
            Type = type;
        }

        private FunctionCallConventionType Type
        {
            get;
        }

        public static FunctionCallConvention Parse(string input, ref int i)
        {
            var j = i;

            var typeString = Helper.ParseIdentifier(input, ref j);
            FunctionCallConventionType type;

            switch (typeString)
            {
                case "CALLBACK":
                    type = FunctionCallConventionType.Callback;
                    break;

                case "WINAPI":
                    type = FunctionCallConventionType.WinApi;
                    break;

                default:
                    return null;
            }

            i = j;

            return new FunctionCallConvention(type);
        }
    }
}
