namespace ParserCombinatorLibrary
{
    internal interface INode
    {
        string Name
        {
            get;
        }

        int GetNext();
    }
}
