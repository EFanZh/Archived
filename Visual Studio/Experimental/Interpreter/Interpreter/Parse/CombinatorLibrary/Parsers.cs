using System;
using System.Linq;

namespace Interpreter.Parse.CombinatorLibrary
{
    public delegate Result Parser(string input, int index);
    public delegate Result<T> Parser<T>(string input, int index);

    public class Parsers
    {
        public static Parser Character(char ch)
        {
            return (input, index) =>
            {
                if (index < input.Length && input[index] == ch)
                {
                    return new Result(index + 1);
                }
                else
                {
                    return null;
                }
            };
        }

        public static Parser Character(Predicate<char> predicate)
        {
            return (input, index) =>
            {
                if (index < input.Length && predicate(input[index]))
                {
                    return new Result(index + 1);
                }
                else
                {
                    return null;
                }
            };
        }

        public static Parser Empty()
        {
            return (input, index) =>
            {
                return new Result(index);
            };
        }

        public static Parser<T> Empty<T>()
        {
            return (input, index) =>
            {
                return new Result<T>(default(T), index);
            };
        }

        public static Parser Terminal(string terminal)
        {
            return (input, index) =>
            {
                if (index < input.Length && input.Skip(index).Take(terminal.Length).SequenceEqual(terminal))
                {
                    return new Result(index + terminal.Length);
                }
                else
                {
                    return null;
                }
            };
        }
    }
}
