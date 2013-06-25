using Interpreter.Parse;
using System.Collections.Generic;

namespace Interpreter.Semantics
{
    internal class DefineExpression : IExpression
    {
        public DefineExpression(string variable, IExpression value)
        {
            Variable = variable;
            Value = value;
        }

        public string Variable
        {
            get;
            private set;
        }

        public IExpression Value
        {
            get;
            private set;
        }
    }
}
