using System;
using System.Collections.Generic;

namespace Interpreter.Parse.CombinatorLibrary
{
    internal static class Combinators
    {
        public static Parser<T> Alternation<T>(this Parser<T> parser, Parser<T> another_parser)
        {
            return (input, index) =>
            {
                Result<T> result;

                if ((result = parser(input, index)) != null)
                {
                    return new Result<T>(result.Value, result.Next);
                }
                else if ((result = another_parser(input, index)) != null)
                {
                    return new Result<T>(result.Value, result.Next);
                }
                else
                {
                    return null;
                }
            };
        }

        public static Parser Concat(this Parser parser, Parser another_parser)
        {
            return (input, index) =>
            {
                Result result_1 = parser(input, index);

                if (result_1 != null)
                {
                    Result result_2 = another_parser(input, result_1.Next);

                    if (result_2 != null)
                    {
                        return new Result(result_2.Next);
                    }
                }

                return null;
            };
        }

        public static Parser<T> Concat<T>(this Parser parser, Parser<T> another_parser)
        {
            return (input, index) =>
            {
                var result_1 = parser(input, index);

                if (result_1 != null)
                {
                    var result_2 = another_parser(input, result_1.Next);

                    if (result_2 != null)
                    {
                        return result_2;
                    }
                }

                return null;
            };
        }

        public static Parser<T> Concat<T>(this Parser<T> parser, Parser another_parser)
        {
            return (input, index) =>
            {
                var result_1 = parser(input, index);

                if (result_1 != null)
                {
                    var result_2 = another_parser(input, result_1.Next);

                    if (result_2 != null)
                    {
                        return result_1;
                    }
                }

                return null;
            };
        }

        public static Parser<string> MakeValue(this Parser parser)
        {
            return (input, index) =>
            {
                var result = parser(input, index);

                if (result != null)
                {
                    return new Result<string>(input.Substring(index, result.Next - index), result.Next);
                }
                else
                {
                    return null;
                }
            };
        }

        public static Parser<T> MakeValue<T>(this Parser parser, Func<string, T> make_value)
        {
            return (input, index) =>
            {
                var result = parser(input, index);

                if (result != null)
                {
                    return new Result<T>(make_value(input.Substring(index, result.Next - index)), result.Next);
                }
                else
                {
                    return null;
                }
            };
        }

        public static Parser Optional(this Parser parser)
        {
            return (input, index) =>
            {
                Result result = parser(input, index);

                if (result == null)
                {
                    return new Result(index);
                }
                else
                {
                    return result;
                }
            };
        }

        public static Parser OptionalSequence(this Parser parser)
        {
            return (input, index) =>
            {
                Result result;
                int i = index;

                while ((result = parser(input, i)) != null)
                {
                    i = result.Next;
                }

                return new Result(i);
            };
        }

        public static Parser<IReadOnlyList<T>> OptionalSequence<T>(this Parser<T> parser)
        {
            return (input, index) =>
            {
                Result<T> result;
                int i = index;
                var items = new List<T>();

                while ((result = parser(input, i)) != null)
                {
                    items.Add(result.Value);
                    i = result.Next;
                }

                return new Result<IReadOnlyList<T>>(items, i);
            };
        }

        public static Parser Sequence(this Parser parser)
        {
            return (input, index) =>
            {
                Result result;
                int i = index;

                while ((result = parser(input, i)) != null)
                {
                    i = result.Next;
                }

                if (i > index)
                {
                    return new Result(i);
                }

                return null;
            };
        }

        public static Parser<IReadOnlyList<T>> Sequence<T>(this Parser<T> parser)
        {
            return (input, index) =>
            {
                Result<T> result;
                int i = index;
                var items = new List<T>();

                while ((result = parser(input, i)) != null)
                {
                    items.Add(result.Value);
                    i = result.Next;
                }

                if (i > index)
                {
                    return new Result<IReadOnlyList<T>>(items, i);
                }

                return null;
            };
        }
    }
}
