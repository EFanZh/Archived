namespace PInvokeHelper.Parser
{
    class FunctionType : Type
    {
        public FunctionModifier Modifier
        {
            get;
        }

        public Type ReturnType
        {
            get;
        }

        public FunctionParameter[] Parameters
        {
            get;
        }
    }
}