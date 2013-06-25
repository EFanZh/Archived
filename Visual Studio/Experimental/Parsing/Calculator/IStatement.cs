using System.Collections.Generic;

namespace Calculator
{
    internal interface IStatement
    {
        double Evaluate(Dictionary<string, double> identifier_table);
    }
}
