using System;
using System.Linq;

namespace ParserCombinatorLibrary
{
    public delegate Result<T>[] Parser<T>(string input, int index);

    public class Parsers
    {
        public static Parser<char> Character(char ch)
        {
            return Character(c => c == ch);
        }

        public static Parser<char> Character(Predicate<char> pred)
        {
            return (input, index) =>
            {
                if (index < input.Length && pred(input[index]))
                {
                    return new[] { new Result<char>(input[index], index + 1) };
                }
                else
                {
                    return new Result<char>[0];
                }
            };
        }

        public static Parser<T> Empty<T>()
        {
            return (input, index) =>
            {
                return new[] { new Result<T>(default(T), index) };
            };
        }

        public static Parser<string> Terminal(string terminal)
        {
            return (input, index) =>
            {
                if (index < input.Length && input.Skip(index).Take(terminal.Length).SequenceEqual(terminal))
                {
                    return new[] { new Result<string>(terminal, index + terminal.Length) };
                }
                else
                {
                    return new Result<string>[0];
                }
            };
        }
    }
}
