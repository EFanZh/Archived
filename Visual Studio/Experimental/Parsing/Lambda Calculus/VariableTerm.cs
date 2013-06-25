using System;
using System.Collections.Generic;

namespace LambdaCalculus
{
    internal class VariableTerm : ITerm, IEquatable<VariableTerm>
    {
        public VariableTerm(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol
        {
            get;
            private set;
        }

        #region ITerm Members

        public ITerm Evaluate(IDictionary<string, ITerm> environment)
        {
            if (environment.ContainsKey(Symbol))
            {
                return environment[Symbol];
            }
            else
            {
                return this;
            }
        }

        public string[] GetFreeVariables()
        {
            return new[] { Symbol };
        }

        public ITerm Substitute(string variable, ITerm expression)
        {
            if (Symbol == variable)
            {
                return expression;
            }
            else
            {
                return this;
            }
        }

        #endregion ITerm Members

        #region IEquatable<VariableTerm> Members

        public bool Equals(VariableTerm other)
        {
            return Symbol.Equals(other.Symbol);
        }

        #endregion IEquatable<VariableTerm> Members

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
