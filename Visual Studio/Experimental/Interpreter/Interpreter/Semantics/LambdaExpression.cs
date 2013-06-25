using Interpreter.Parse;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Semantics
{
    internal class LambdaExpression : IExpression
    {
        public LambdaExpression(IEnumerable<string> formal_parameters, IEnumerable<IExpression> body)
        {
            FormalParameters = formal_parameters.ToArray();
            Body = body.ToArray();
        }

        public string[] FormalParameters
        {
            get;
            private set;
        }

        public IExpression[] Body
        {
            get;
            private set;
        }
    }
}
