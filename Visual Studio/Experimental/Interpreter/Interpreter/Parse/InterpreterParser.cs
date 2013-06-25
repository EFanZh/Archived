using Interpreter.Parse.CombinatorLibrary;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Parse
{
    internal static class InterpreterParser
    {
        private static Parser dot = Parsers.Character('.');
        private static Parser left_paren = Parsers.Character('(');
        private static Parser right_paren = Parsers.Character(')');

        private static Parser digit = Parsers.Character(char.IsDigit);
        private static Parser letter = Parsers.Character(char.IsLetter);
        private static Parser letter_or_digit = Parsers.Character(char.IsLetterOrDigit);
        private static Parser white_space = Parsers.Character(char.IsWhiteSpace);

        private static Parser digit_sequence = digit.Sequence();
        private static Parser white_space_optional_sequence = white_space.OptionalSequence();

        private static Parser<NumberAtom> number_atom = digit_sequence.Concat(dot.Concat(digit_sequence).Optional()).Select(str => new NumberAtom(double.Parse(str)));
        private static Parser<SymbolAtom> symbol_atom = letter.Concat(letter_or_digit.OptionalSequence()).Select(str => new SymbolAtom(str));
        private static Parser<IAtom> atom = number_atom.Select(result => result as IAtom).Alternation(symbol_atom.Select(result => result as IAtom));
        private static Parser<ListExpression> list = left_paren.Concat<IReadOnlyList<IExpression>>(ExpressionList).Concat(right_paren).Select(expr_list => new ListExpression(expr_list));
        private static Parser<IExpression> expression = atom.Select(result => result as IExpression).Alternation(list.Select(result => result as IExpression));
        private static Parser<IReadOnlyList<IExpression>> expression_list = white_space_optional_sequence.Concat(expression).OptionalSequence();

        private static Result<IReadOnlyList<IExpression>> ExpressionList(string input, int index)
        {
            return expression_list(input, index);
        }

        public static IExpression[] Parse(string input)
        {
            var result = expression_list(input, 0);

            if (result != null)
            {
                return result.Value.ToArray();
            }

            return null;
        }
    }
}
