namespace ParserCombinatorLibrary
{
    public class Result<T>
    {
        public Result(T value, int next)
        {
            Value = value;
            Next = next;
        }

        public int Next
        {
            get;
            private set;
        }

        public T Value
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
