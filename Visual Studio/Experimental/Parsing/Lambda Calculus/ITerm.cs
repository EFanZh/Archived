using System.Collections.Generic;

namespace LambdaCalculus
{
    internal interface ITerm
    {
        ITerm Evaluate(IDictionary<string, ITerm> environment);

        string[] GetFreeVariables();

        ITerm Substitute(string variable, ITerm expression);
    }
}
