using System.Collections.Generic;
using System.Linq;

namespace ParserCombinatorLibrary
{
    internal static class Combinators
    {
        public delegate ISet<INode> Parser(string input, int index);

        public static Parser GetEmptyParser()
        {
            return (input, index) =>
            {
                var result = new HashSet<INode>() { new LeafNode("ε", index) };

                return result;
            };
        }

        public static Parser GetTerminalParser(string terminal)
        {
            return (input, index) =>
            {
                var result = new HashSet<INode>();

                if (index < input.Length && terminal.Length <= input.Length && input.Skip(index).Take(terminal.Length).SequenceEqual(terminal))
                {
                    result.Add(new LeafNode(terminal, index + terminal.Length));
                }

                return result;
            };
        }

        public static Parser AlternationCombinator(params Parser[] parsers)
        {
            return (input, index) =>
            {
                var results = new HashSet<INode>();

                foreach (var parser in parsers)
                {
                    foreach (var sub_result in parser(input, index))
                    {
                        results.Add(sub_result);
                    }
                }

                return results;
            };
        }

        public static Parser SequenceCombinator(string name, params Parser[] parsers)
        {
            // User should never actually use when parsers.Length == 0.
            if (parsers.Length == 0)
            {
                return (input, index) =>
                {
                    return new HashSet<INode>() { new BranchNode(name) };
                };
            }
            else
            {
                return (input, index) =>
                {
                    var results = new HashSet<INode>();

                    foreach (var first_result in parsers[0](input, index))
                    {
                        foreach (var rest_result in SequenceCombinator(string.Empty, parsers.Skip(1).ToArray())(input, first_result.GetNext()))
                        {
                            var node = new BranchNode(name) { Children = { first_result } };

                            foreach (var rest_result_child in (rest_result as BranchNode).Children)
                            {
                                node.Children.Add(rest_result_child);
                            }

                            results.Add(node);
                        }
                    }

                    return results;
                };
            }
        }

        public static class Experimental
        {
            public static Parser LeftAssociativeSequenceCombinator(string name, Parser item_parser, Parser delimiter_parser)
            {
                return (input, index) =>
                {
                    var results = new HashSet<INode>();
                    var new_results = new HashSet<INode>(from sub_result in item_parser(input, index) select new BranchNode(name) { Children = { sub_result } });

                    while (new_results.Count > 0)
                    {
                        var temp_new_results = new HashSet<INode>();

                        foreach (var new_result in new_results)
                        {
                            var delimiter_results = delimiter_parser(input, new_result.GetNext());

                            if (delimiter_results.Count == 0)
                            {
                                results.Add(new_result);
                            }
                            else
                            {
                                foreach (var delimiter_result in delimiter_results)
                                {
                                    foreach (var sub_item_result in item_parser(input, delimiter_result.GetNext()))
                                    {
                                        temp_new_results.Add(new BranchNode(name) { Children = { new_result, delimiter_result, sub_item_result } });
                                    }
                                }
                            }
                        }
                        new_results = temp_new_results;
                    }

                    return results;
                };
            }
        }
    }
}
