using ParserCombinatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class Program
    {
        private static char[] reversed_characters = new char[] { '\\', '.', '=', '(', ')', '\0' };

        private static Parser<char> lambda_parser = GetWhitespaceItemParser(Parsers.Character('\\'));
        private static Parser<char> dot_parser = GetWhitespaceItemParser(Parsers.Character('.'));
        private static Parser<char> left_paren_parser = GetWhitespaceItemParser(Parsers.Character('('));
        private static Parser<char> right_paren_parser = GetWhitespaceItemParser(Parsers.Character(')'));
        private static Parser<char> assign_parser = GetWhitespaceItemParser(Parsers.Character('='));

        private static Parser<string> identifier_parser = GetWhitespaceItemParser(Combinators.Terminal(Combinators.Sequence(Parsers.Character(ch => !char.IsWhiteSpace(ch) && !reversed_characters.Contains(ch)))));
        private static Parser<ITerm> term_parser = Combinators.Alternation(FunctionTermParser, ApplicationTermParser);

        private static Parser<IStatement> start_parser = Combinators.Concat(StatementParser, Parsers.Character('\0'), (result, ch) => result);

        // This returns a parser with optional leading whitespaces.
        private static Parser<T> GetWhitespaceItemParser<T>(Parser<T> parser)
        {
            return Combinators.Concat(Combinators.OptionalSequence(Parsers.Character(char.IsWhiteSpace)), parser, (ws, result) => result);
        }

        private static Result<IStatement>[] StatementParser(string input, int index)
        {
            var results_1 = from result_1 in identifier_parser(input, index)
                            from result_2 in assign_parser(input, result_1.Next)
                            from result_3 in term_parser(input, result_2.Next)
                            select new Result<IStatement>(new AssignmentStatement(result_1.Value, result_3.Value), result_3.Next);

            var results_2 = from result in term_parser(input, index)
                            select new Result<IStatement>(new TermStatement(result.Value), result.Next);

            return results_1.Union(results_2).ToArray();
        }

        private static Result<ITerm>[] FunctionTermParser(string input, int index)
        {
            return (from result_1 in lambda_parser(input, index)
                    from result_2 in identifier_parser(input, result_1.Next)
                    from result_3 in dot_parser(input, result_2.Next)
                    from result_4 in term_parser(input, result_3.Next)
                    select new Result<ITerm>(new AbstractionTerm(result_2.Value, result_4.Value), result_4.Next)).ToArray(); // ?
        }

        private static Result<ITerm>[] ApplicationTermParser(string input, int index)
        {
            var results = new List<Result<ITerm>>();
            var new_results = new List<Result<ITerm>>(from sub_result in AtomicParser(input, index)
                                                      select new Result<ITerm>(sub_result.Value, sub_result.Next));

            while (new_results.Count > 0)
            {
                var temp_new_results = new List<Result<ITerm>>();

                foreach (var new_result in new_results)
                {
                    var follow_results = AtomicParser(input, new_result.Next);

                    if (follow_results.Length == 0)
                    {
                        var function_results = FunctionTermParser(input, new_result.Next);

                        if (function_results.Length == 0)
                        {
                            results.Add(new_result);
                        }
                        else
                        {
                            foreach (var funciton_result in function_results)
                            {
                                temp_new_results.Add(new Result<ITerm>(new ApplicationTerm(new_result.Value, funciton_result.Value), funciton_result.Next));
                            }
                        }
                    }
                    else
                    {
                        foreach (var sub_item_result in follow_results)
                        {
                            temp_new_results.Add(new Result<ITerm>(new ApplicationTerm(new_result.Value, sub_item_result.Value), sub_item_result.Next));
                        }
                    }
                }

                new_results = temp_new_results;
            }

            return results.ToArray();
        }

        private static Result<ITerm>[] AtomicParser(string input, int index)
        {
            var results_1 = from result_1 in left_paren_parser(input, index)
                            from result_2 in term_parser(input, result_1.Next)
                            from result_3 in right_paren_parser(input, result_2.Next)
                            select new Result<ITerm>(result_2.Value, result_3.Next);

            var results_2 = from result in identifier_parser(input, index)
                            select new Result<ITerm>(new VariableTerm(result.Value), result.Next);

            return results_1.Union(results_2).ToArray();
        }

        private static Result<IStatement>[] StartParse(string input)
        {
            return start_parser(input.Trim() + '\0', 0);
        }

        private static void Main(string[] args)
        {
            const string prompt = "Input: ";
            var global_environment = new Dictionary<string, ITerm>();

            Console.Write(prompt);
            string line = Console.ReadLine();

            while (true)
            {
                switch (line)
                {
                    case "clear":
                        global_environment.Clear();
                        break;

                    case "list":
                        foreach (var item in global_environment)
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
                                Console.WriteLine(">> Value: {0}", result.Value.Evaluate(global_environment));
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
