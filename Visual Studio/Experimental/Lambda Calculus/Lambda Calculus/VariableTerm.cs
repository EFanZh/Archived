using System;
using System.Collections.Generic;

namespace LambdaCalculus
{
    internal class VariableTerm : Term, IEquatable<VariableTerm>
    {
        public VariableTerm(string token)
        {
            Token = token;
        }

        public string Token
        {
            get;
            private set;
        }

        public static implicit operator VariableTerm(string name)
        {
            return new VariableTerm(name);
        }

        public override Term Evaluate(EvaluationOrder evaluation_order)
        {
            return this;
        }

        public override ISet<VariableTerm> GetFreeVariables()
        {
            return new HashSet<VariableTerm>() { this };
        }

        public override Term Substitute(VariableTerm old_expression, Term expression)
        {
            if (Token.Equals(old_expression.Token))
            {
                // x[x := r] = r
                return expression;
            }
            else
            {
                // y[x := r] = y if x ¡Ù y
                return this;
            }
        }

        public override string ToString()
        {
            return Token;
        }

        #region IEquatable<VariableTerm> Members

        public bool Equals(VariableTerm other)
        {
            return Token.Equals(other.Token);
        }

        #endregion IEquatable<VariableTerm> Members

        public override int GetHashCode()
        {
            return Token.GetHashCode();
        }
    }
}
