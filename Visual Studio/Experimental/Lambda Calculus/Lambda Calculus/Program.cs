using System;

namespace LambdaCalculus
{
    internal static class Program
    {
        private static void Main()
        {
            FunctionApplicationExpression c = new FunctionApplicationExpression(
                new FunctionExpression("x",
                    new FunctionApplicationExpression(
                        new VariableExpression("f"),
                        new VariableExpression("x"))), new VariableExpression("y"));
            Console.WriteLine(c.ToString());
            Console.WriteLine(c.Calculate().ToString());
        }
    }
}
