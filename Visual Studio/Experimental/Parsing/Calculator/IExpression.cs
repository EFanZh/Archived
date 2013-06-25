using System.Collections.Generic;

namespace Calculator
{
    internal interface IExpression
    {
        double Evaluate(Dictionary<string, double> identifier_table);
    }
}
