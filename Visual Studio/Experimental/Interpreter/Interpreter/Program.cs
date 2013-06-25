using Interpreter.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string prompt = "Input: ";

            while (true)
            {
                Console.Write(prompt);

                var result = InterpreterParser.Parse(Console.ReadLine());

                if (result != null)
                {
                    Console.WriteLine("{0}\n", string.Join("\n", result.AsEnumerable()));
                }
            }
        }
    }
}
