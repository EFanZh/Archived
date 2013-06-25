using ParserCombinatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    internal class Program
    {
        private static Parser<Result<char>[]> digit_sequence_parser = Combinators.Sequence(Parsers.Character(char.IsDigit));
        private static Parser<object> number_postfix = Combinators.Optional(Combinators.Concat(Parsers.Character('.'), digit_sequence_parser, (a, b) => new object()));

        private static Parser<string> number_parser = GetWhitespaceItemParser(Combinators.Terminal(Combinators.Concat(digit_sequence_parser, number_postfix, (a, b) => new object())));
        private static Parser<string> identifier_parser = GetWhitespaceItemParser(Combinators.Terminal(Combinators.Concat(Parsers.Character(ch => char.IsLetter(ch) || ch == '_'), Combinators.OptionalSequence(Parsers.Character(ch => char.IsLetterOrDigit(ch) || ch == '_')), (a, b) => new object())));

        private static Parser<IExpression> expression_parser = Combinators.LeftAssociativeSequenceCombinator(TermParser, GetWhitespaceItemParser(Parsers.Character(ch => ch == '+' | ch == '-')), term => term, MakeExpression);
        private static Parser<IExpression> term_parser = Combinators.LeftAssociativeSequenceCombinator(FactorParser, GetWhitespaceItemParser(Parsers.Character(ch => ch == '*' | ch == '/')), factor => factor, MakeTerm);
        private static Parser<char> assign_parser = GetWhitespaceItemParser(Parsers.Character('='));
        private static Parser<char> left_paren_parser = GetWhitespaceItemParser(Parsers.Character('('));
        private static Parser<char> right_paren_parser = GetWhitespaceItemParser(Parsers.Character(')'));
        private static Parser<char> positive_parser = GetWhitespaceItemParser(Parsers.Character('+'));
        private static Parser<char> negative_parser = GetWhitespaceItemParser(Parsers.Character('-'));

        private static Parser<IStatement> start_parser = Combinators.Concat(StatementParser, Parsers.Character('\0'), (result, ch) => result);

        // This returns a parser with optional leading whitespaces.
        private static Parser<T> GetWhitespaceItemParser<T>(Parser<T> parser)
        {
            return Combinators.Concat(Combinators.OptionalSequence(Parsers.Character(char.IsWhiteSpace)), parser, (ws, result) => result);
        }

        private static IExpression MakeExpression(IExpression expression, IExpression term, char delimiter)
        {
            if (delimiter == '+')
            {
                return new PlusExpression(expression, term);
            }
            else if (delimiter == '-')
            {
                return new MinusExpression(expression, term);
            }
            else
            {
                throw new Exception();
            }
        }

        private static IExpression MakeTerm(IExpression term, IExpression factor, char delimiter)
        {
            if (delimiter == '*')
            {
                return new MultiplyExpression(term, factor);
            }
            else if (delimiter == '/')
            {
                return new DivideExpression(term, factor);
            }
            else
            {
                throw new Exception();
            }
        }

        private static Result<IStatement>[] StatementParser(string input, int index)
        {
            var results_1 = from result_1 in identifier_parser(input, index)
                            from result_2 in assign_parser(input, result_1.Next)
                            from result_3 in expression_parser(input, result_2.Next)
                            select new Result<IStatement>(new AssignmentStatement(result_1.Value, result_3.Value), result_3.Next);

            var results_2 = from result in expression_parser(input, index)
                            select new Result<IStatement>(new ExpressionStatement(result.Value), result.Next);

            return results_1.Union(results_2).ToArray();
        }

        private static Result<IExpression>[] ExpressionParser(string input, int index)
        {
            return expression_parser(input, index);
        }

        private static Result<IExpression>[] TermParser(string input, int index)
        {
            return term_parser(input, index);
        }

        private static Result<IExpression>[] FactorParser(string input, int index)
        {
            var results_1 = from result_1 in left_paren_parser(input, index)
                            from result_2 in ExpressionParser(input, result_1.Next)
                            from result_3 in right_paren_parser(input, result_2.Next)
                            select new Result<IExpression>(result_2.Value, result_3.Next);

            var results_2 = from result in number_parser(input, index)
                            select new Result<IExpression>(new NumberExpression(double.Parse(result.Value)), result.Next);

            var results_3 = from result in identifier_parser(input, index)
                            select new Result<IExpression>(new IdentifierExpression(result.Value), result.Next);

            var results_4 = from result_1 in positive_parser(input, index)
                            from result_2 in FactorParser(input, result_1.Next)
                            select new Result<IExpression>(new PositiveExpression(result_2.Value), result_2.Next);

            var results_5 = from result_1 in negative_parser(input, index)
                            from result_2 in FactorParser(input, result_1.Next)
                            select new Result<IExpression>(new NegativeExpression(result_2.Value), result_2.Next);

            return results_1.Union(results_2).Union(results_3).Union(results_4).Union(results_5).ToArray();
        }

        private static Result<IStatement>[] StartParse(string input)
        {
            return start_parser(input.Trim() + '\0', 0);
        }

        private static void Main(string[] args)
        {
            const string prompt = "Input: ";
            var identifier_table = new Dictionary<string, double>();

            Console.Write(prompt);
            string line = Console.ReadLine();

            while (true)
            {
                switch (line)
                {
                    case "clear":
                        identifier_table.Clear();
                        break;

                    case "list":
                        foreach (var item in identifier_table)
                        {
                            Console.WriteLine(">> {0}: {1}", item.Key, item.Value);
                        }
                        break;

                    case "exit":
                        return;

                    default:
                        foreach (var result in StartParse(line))
                        {
                            Console.WriteLine(">> AST: {0}", result.Value);

                            try
                            {
                                Console.WriteLine(">> Value: {0}", result.Value.Evaluate(identifier_table));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(">> Error: {0}", ex.Message);
                            }
                        }
                        break;
                }
                Console.WriteLine();

                Console.Write(prompt);
                line = Console.ReadLine();
            }
        }
    }
}
