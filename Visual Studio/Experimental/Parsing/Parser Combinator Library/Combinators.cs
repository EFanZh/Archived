using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCombinatorLibrary
{
    public class Combinators
    {
        public static Parser<T> Alternation<T>(params Parser<T>[] parsers)
        {
            return (input, index) =>
            {
                return (from parser in parsers
                        from sub_result in parser(input, index)
                        select sub_result).ToArray();
            };
        }

        public static Parser<T3> Concat<T1, T2, T3>(Parser<T1> parser_1, Parser<T2> parser_2, Func<T1, T2, T3> make_result)
        {
            return (input, index) =>
            {
                return (from result_1 in parser_1(input, index)
                        from result_2 in parser_2(input, result_1.Next)
                        select new Result<T3>(make_result(result_1.Value, result_2.Value), result_2.Next)).ToArray();
            };
        }

        public static Parser<T3> LeftAssociativeSequenceCombinator<T1, T2, T3>(Parser<T1> item_parser, Parser<T2> delimiter_parser, Func<T1, T3> make_term, Func<T3, T1, T2, T3> make_result)
        {
            return (input, index) =>
            {
                var results = new List<Result<T3>>();
                var new_results = new List<Result<T3>>(from sub_result in item_parser(input, index)
                                                       select new Result<T3>(make_term(sub_result.Value), sub_result.Next));

                while (new_results.Count > 0)
                {
                    var temp_new_results = new List<Result<T3>>();

                    foreach (var new_result in new_results)
                    {
                        var delimiter_results = delimiter_parser(input, new_result.Next);

                        if (delimiter_results.Length == 0)
                        {
                            results.Add(new_result);
                        }
                        else
                        {
                            foreach (var delimiter_result in delimiter_results)
                            {
                                foreach (var sub_item_result in item_parser(input, delimiter_result.Next))
                                {
                                    temp_new_results.Add(new Result<T3>(make_result(new_result.Value, sub_item_result.Value, delimiter_result.Value), sub_item_result.Next));
                                }
                            }
                        }
                    }
                    new_results = temp_new_results;
                }

                return results.ToArray();
            };
        }

        public static Parser<T> Optional<T>(Parser<T> parser)
        {
            return Alternation<T>(Parsers.Empty<T>(), parser);
        }

        public static Parser<Result<T>[]> OptionalSequence<T>(Parser<T> parser)
        {
            return Optional(Sequence(parser));
        }

        public static Parser<Result<T>[]> Sequence<T>(Parser<T> parser)
        {
            return (input, index) =>
            {
                var results = new List<Result<Result<T>[]>>();
                var new_results = new List<Result<Result<T>[]>>(from sub_result in parser(input, index)
                                                                select new Result<Result<T>[]>(new[] { sub_result }, sub_result.Next));

                while (new_results.Count > 0)
                {
                    var temp_new_results = new List<Result<Result<T>[]>>();

                    foreach (var new_result in new_results)
                    {
                        var rest_results = parser(input, new_result.Next);

                        if (rest_results.Length == 0)
                        {
                            results.Add(new_result);
                        }
                        else
                        {
                            foreach (var rest_result in rest_results)
                            {
                                temp_new_results.Add(new Result<Result<T>[]>(new_result.Value.Concat(new[] { rest_result }).ToArray(), rest_result.Next));
                            }
                        }
                    }

                    new_results = temp_new_results;
                }

                return results.ToArray();
            };
        }

        public static Parser<string> Terminal<T>(Parser<T> parser)
        {
            return (input, index) =>
            {
                return (from result in parser(input, index)
                        select new Result<string>(input.Substring(index, result.Next - index), result.Next)).ToArray();
            };
        }
    }
}
