using System;
using System.Collections.Generic;

namespace ParserCombinators
{
    public static class Combinators
    {
        public static Parser<T> Optional<T>(this Parser<T> parser)
        {
            return (string input, ref int index, out bool success) =>
            {
                var result = parser(input, ref index, out success);

                success = true;

                return result;
            };
        }

        public static Parser<T> Or<T>(params Parser<T>[] parsers)
        {
            return (string input, ref int index, out bool success) =>
            {
                foreach (var parser in parsers)
                {
                    var result = parser(input, ref index, out success);

                    if (success)
                    {
                        return result;
                    }
                }

                success = false;

                return default(T);
            };
        }

        public static Parser<TResult> LeftFoldAny<TSource, TSeparator, TResult>(this Parser<TSource> parser,
                                                                             Parser<TSeparator> separatorParser,
                                                                             TResult initialResult,
                                                                             Func<TResult, TSource, TResult> makeSingleResult,
                                                                             Func<TResult, TSource, TSeparator, TResult> combineResult)
        {
            return (string input, ref int index, out bool success) =>
                   {
                       var i = index;
                       var firstResult = parser(input, ref i, out success);

                       if (!success)
                       {
                           success = true;

                           return initialResult;
                       }

                       var result = makeSingleResult(initialResult, firstResult);

                       index = i;

                       var separator = separatorParser(input, ref i, out success);

                       while (success)
                       {
                           var item = parser(input, ref i, out success);

                           if (!success)
                           {
                               success = true;

                               return result;
                           }

                           result = combineResult(result, item, separator);

                           index = i;
                           separator = separatorParser(input, ref i, out success);
                       }

                       success = true;

                       return result;
                   };
        }

        public static Parser<TSource[]> List<TSource, TSeparator>(this Parser<TSource> parser, Parser<TSeparator> separatorParser)
        {
            return from list in parser.LeftFold(separatorParser,
                                                item => new List<TSource>() { item },
                                                (result, item, separator) =>
                                                {
                                                    result.Add(item);

                                                    return result;
                                                })
                   select list.ToArray();
        }

        public static Parser<TSource[]> OptionalList<TSource, TSeparator>(this Parser<TSource> parser, Parser<TSeparator> separatorParser)
        {
            return from list in parser.FoldLeftAny(separatorParser,
                                                   new List<TSource>(),
                                                   (result, item) =>
                                                   {
                                                       result.Add(item);

                                                       return result;
                                                   },
                                                   (result, item, separator) =>
                                                   {
                                                       result.Add(item);

                                                       return result;
                                                   })
                   select list.ToArray();
        }

        public static Parser<TResult> Select<TSource, TResult>(this Parser<TSource> parser,
                                                               Func<TSource, TResult> selector)
        {
            return (string input, ref int index, out bool success) =>
                   {
                       var result = parser(input, ref index, out success);

                       return success ? selector(result) : default(TResult);
                   };
        }

        public static Parser<TResult> SelectMany<TSource1, TSource2, TResult>(this Parser<TSource1> parser1,
                                                                              Func<TSource1, Parser<TSource2>> parserSelector,
                                                                              Func<TSource1, TSource2, TResult> resultSelector)
        {
            return (string input, ref int index, out bool success) =>
                   {
                       var i = index;
                       var result1 = parser1(input, ref i, out success);

                       if (!success)
                       {
                           return default(TResult);
                       }

                       var parser2 = parserSelector(result1);
                       var result2 = parser2(input, ref i, out success);

                       if (!success)
                       {
                           return default(TResult);
                       }

                       index = i;

                       return resultSelector(result1, result2);
                   };
        }

        public static Parser<TResult> FoldLeft<TSource, TSeparator, TResult>(this Parser<TSource> parser,
                                                                             Parser<TSeparator> separatorParser,
                                                                             Func<TSource, TResult> makeSingleResult,
                                                                             Func<TResult, TSource, TSeparator, TResult> combineResult)
        {
            return (string input, ref int index, out bool success) =>
                   {
                       var i = index;
                       var firstItem = parser(input, ref i, out success);

                       if (!success)
                       {
                           return default(TResult);
                       }

                       var result = makeSingleResult(firstItem);

                       index = i;

                       var separator = separatorParser(input, ref i, out success);

                       while (success)
                       {
                           var item = parser(input, ref i, out success);

                           if (!success)
                           {
                               return result;
                           }

                           result = combineResult(result, item, separator);

                           index = i;

                           separator = separatorParser(input, ref i, out success);
                       }

                       return result;
                   };
        }
    }
}
