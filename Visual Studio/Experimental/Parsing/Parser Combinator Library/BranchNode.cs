using System.Collections.Generic;

namespace ParserCombinatorLibrary
{
    internal class BranchNode : INode
    {
        public BranchNode(string name)
        {
            Children = new List<INode>();
            Name = name;
        }

        public IList<INode> Children
        {
            get;
            set;
        }

        #region INode Members

        public string Name
        {
            get;
            private set;
        }

        public int GetNext()
        {
            return Children[Children.Count - 1].GetNext();
        }

        #endregion INode Members

        public override string ToString()
        {
            return string.Format("({0} → {1})", Name, string.Join(", ", Children));
        }
    }
}
