using System;

namespace LambdaCalculus
{
    internal static class Program
    {
        private static void PrintTerm(string name, Term term)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(": {1}", name, term);
        }

        private static void Main()
        {
            AbstractionTerm successor = new AbstractionTerm("¦Ñ",
                new AbstractionTerm("f",
                    new AbstractionTerm("x",
                        new ApplicationTerm("f",
                            new ApplicationTerm(new ApplicationTerm("¦Ñ", "f"), "x")))));

            Term zero = new AbstractionTerm("f", new AbstractionTerm("x", "x"));
            Term one = new ApplicationTerm(successor, zero).Evaluate(EvaluationOrder.CallByValue);
            Term two = new ApplicationTerm(successor, one).Evaluate(EvaluationOrder.CallByValue);
            Term three = new ApplicationTerm(successor, two).Evaluate(EvaluationOrder.CallByValue);
            Term four = new ApplicationTerm(successor, three).Evaluate(EvaluationOrder.CallByValue);
            Term five = new ApplicationTerm(successor, four).Evaluate(EvaluationOrder.CallByValue);

            PrintTerm("successor", successor);
            PrintTerm("        0", zero);
            PrintTerm("        1", one);
            PrintTerm("        2", two);
            PrintTerm("        3", three);
            PrintTerm("        4", four);
            PrintTerm("        5", five);
            PrintTerm("    4 ^ 3", (new ApplicationTerm(three, four)).Evaluate(EvaluationOrder.CallByValue));
        }
    }
}
