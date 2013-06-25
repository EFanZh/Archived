using System;

namespace Interpreter.Parse.CombinatorLibrary
{
    internal static class LinqProvider
    {
        public static Parser<TResult> Select<TResult>(this Parser source, Func<string, TResult> selector)
        {
            return (input, index) =>
            {
                var result = source(input, index);

                if (result != null)
                {
                    return new Result<TResult>(selector(input.Substring(index, result.Next - index)), result.Next);
                }

                return null;
            };
        }

        public static Parser<TResult> Select<TSource, TResult>(this Parser<TSource> source, Func<TSource, TResult> selector)
        {
            return (input, index) =>
            {
                var result = source(input, index);

                if (result != null)
                {
                    return new Result<TResult>(selector(result.Value), result.Next);
                }

                return null;
            };
        }

        public static Parser<TResult> SelectMany<TResult>(this Parser source, Func<bool, Parser<TResult>> selector)
        {
            return (input, index) =>
            {
                var result = source(input, index);

                if (result != null)
                {
                    return selector(true)(input, result.Next);
                }

                return null;
            };
        }

        public static Parser<TResult> SelectMany<TCollection, TResult>(this Parser source, Func<bool, Parser<TCollection>> collectionSelector, Func<bool, TCollection, TResult> resultSelector)
        {
            return (input, index) =>
            {
                var result_1 = source(input, index);

                if (result_1 != null)
                {
                    var result_2 = collectionSelector(true)(input, result_1.Next);

                    if (result_2 != null)
                    {
                        return new Result<TResult>(resultSelector(true, result_2.Value), result_2.Next);
                    }
                }

                return null;
            };
        }

        public static Parser<TResult> SelectMany<TSource, TResult>(this Parser<TSource> source, Func<TSource, Parser<TResult>> selector)
        {
            return (input, index) =>
            {
                var result = source(input, index);

                if (result != null)
                {
                    return selector(result.Value)(input, result.Next);
                }

                return null;
            };
        }

        public static Parser<TResult> SelectMany<TSource, TCollection, TResult>(this Parser<TSource> source, Func<TSource, Parser<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            return (input, index) =>
            {
                var result_1 = source(input, index);

                if (result_1 != null)
                {
                    var result_2 = collectionSelector(result_1.Value)(input, result_1.Next);

                    if (result_2 != null)
                    {
                        return new Result<TResult>(resultSelector(result_1.Value, result_2.Value), result_2.Next);
                    }
                }

                return null;
            };
        }

        public static Parser<TResult> SelectMany<TSource, TResult>(this Parser<TSource> source, Func<TSource, Parser> collectionSelector, Func<TSource, bool, TResult> resultSelector)
        {
            return (input, index) =>
            {
                var result_1 = source(input, index);

                if (result_1 != null)
                {
                    var result_2 = collectionSelector(result_1.Value)(input, result_1.Next);

                    if (result_2 != null)
                    {
                        return new Result<TResult>(resultSelector(result_1.Value, true), result_2.Next);
                    }
                }

                return null;
            };
        }
    }
}
