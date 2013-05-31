using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCombinatorLibrary
{
    internal class Combinators2
    {
        private delegate ISet<Result<T>> Parser<T>(string input, int index);

        public static Parser<char> GetCharacterParser(char ch)
        {
            return GetCharacterParser(c => c == ch);
        }

        public static Parser<char> GetCharacterParser(Predicate<char> pred)
        {
            return (input, index) =>
            {
                if (index < input.Length && pred(input[index]))
                {
                    return new HashSet<Result<char>>() { new Result<char>(input[index], index + 1) };
                }
                else
                {
                    return new HashSet<Result<char>>();
                }
            };
        }

        public static Parser<string> GetTerminalParser(string terminal)
        {
            return (input, index) =>
            {
                if (index < input.Length && input.Skip(index).Take(terminal.Length).SequenceEqual(terminal))
                {
                    return new HashSet<Result<string>>() { new Result<string>(terminal, index + terminal.Length) };
                }
                else
                {
                    return new HashSet<Result<string>>();
                }
            };
        }

        public static Parser<object> GetEmptyParser()
        {
            return (input, index) =>
            {
                return new HashSet<Result<object>>() { new Result<object>(null, index) };
            };
        }

        public static Parser<T> AlternationCombinator<T>(params Parser<T>[] parsers)
        {
            return (input, index) =>
            {
                return new HashSet<Result<T>>(from parser in parsers
                                              from sub_result in parser(input, index)
                                              select sub_result);
            };
        }
    }
}
