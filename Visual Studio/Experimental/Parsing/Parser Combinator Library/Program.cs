using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCombinatorLibrary
{
    using Parser = Combinators.Parser;

    internal class Program
    {
        private static Parser left_paren_parser = Combinators.GetTerminalParser("(");
        private static Parser right_paren_parser = Combinators.GetTerminalParser(")");
        private static Parser s = new Parser(S);
        private static Parser t = new Parser(T);

        // S -> T | T S.
        private static ISet<INode> S(string input, int index)
        {
            return Combinators.AlternationCombinator(Combinators.SequenceCombinator("S", t), Combinators.SequenceCombinator("S", t, s))(input, index);
        }

        // T -> ( ) | ( S ).
        private static ISet<INode> T(string input, int index)
        {
            return Combinators.AlternationCombinator(Combinators.SequenceCombinator("T", left_paren_parser, right_paren_parser), Combinators.SequenceCombinator("T", left_paren_parser, s, right_paren_parser))(input, index);
        }

        private static void Main(string[] args)
        {
            const string prompt = "Input: ";
            Console.Write(prompt);
            string line = Console.ReadLine();

            Parser digit_parser = Combinators.AlternationCombinator(Combinators.GetTerminalParser("0"),
                Combinators.GetTerminalParser("1"),
                Combinators.GetTerminalParser("2"),
                Combinators.GetTerminalParser("3"),
                Combinators.GetTerminalParser("4"),
                Combinators.GetTerminalParser("5"),
                Combinators.GetTerminalParser("6"),
                Combinators.GetTerminalParser("7"),
                Combinators.GetTerminalParser("8"),
                Combinators.GetTerminalParser("9"));

            Parser s = Combinators.Experimental.LeftAssociativeSequenceCombinator("S", Combinators.GetTerminalParser("1"), Combinators.GetTerminalParser("+"));

            while (!string.Equals(line, "exit"))
            {
                foreach (var result in s(line, 0).Where(r => r.GetNext() == line.Length))
                {
                    Console.WriteLine("=> {0}", result);
                }
                Console.WriteLine();

                Console.Write(prompt);
                line = Console.ReadLine();
            }
        }
    }
}
