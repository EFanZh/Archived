namespace PInvokeHelper.Parser
{
    internal class PointerType : Type
    {
        public PointerType(Type pointedType)
        {
            PointedType = pointedType;
        }

        private Type PointedType
        {
            get;
        }
    }
}