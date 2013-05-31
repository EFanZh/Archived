namespace ParserCombinatorLibrary
{
    internal class Result<T> : IResult
    {
        public Result(T value, int next)
        {
            Value = value;
            Next = next;
        }

        public T Value
        {
            get;
            private set;
        }

        #region IResult Members

        public int Next
        {
            get;
            private set;
        }

        #endregion IResult Members
    }
}
