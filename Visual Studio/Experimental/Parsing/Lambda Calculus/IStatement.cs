using System.Collections.Generic;

namespace LambdaCalculus
{
    internal interface IStatement
    {
        ITerm Evaluate(Dictionary<string, ITerm> environment);
    }
}
