namespace PInvokeHelper.Parser
{
    internal class TypeDefDeclaration : SymbolDeclaration
    {
        public Type OriginalType
        {
            get;
        }

        public TypeDefType[] DefinedTypes
        {
            get;
        }
    }
}