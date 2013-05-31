namespace ParserCombinatorLibrary
{
    internal class LeafNode : INode
    {
        private int ln_next;

        public LeafNode(string name, int next)
        {
            Name = name;
            ln_next = next;
        }

        #region INode Members

        public string Name
        {
            get;
            private set;
        }

        public int GetNext()
        {
            return ln_next;
        }

        #endregion INode Members

        public override string ToString()
        {
            return string.Format("\"{0}\"", Name.Replace("\"", "\\\""));
        }
    }
}
