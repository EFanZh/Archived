namespace TreeGraphGenerator
{
    internal class TreeRowWidth
    {
        public TreeRowWidth(double left, double right)
        {
            Left = left;
            Right = right;
        }

        public double Left
        {
            get;
            set;
        }

        public double Right
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", Left, Right);
        }
    }
}
