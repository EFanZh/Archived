using System;

namespace Interpreter.Parse.CombinatorLibrary
{
    public class Result
    {
        public Result(int next)
        {
            Next = next;
        }

        public int Next
        {
            get;
            private set;
        }
    }

    public class Result<T> : Result
    {
        public Result(T value, int next)
            : base(next)
        {
            Value = value;
        }

        public T Value
        {
            get;
            private set;
        }

        public Result<TResult> Convert<TResult>(Func<T, TResult> make_value)
        {
            return new Result<TResult>(make_value(Value), Next);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
