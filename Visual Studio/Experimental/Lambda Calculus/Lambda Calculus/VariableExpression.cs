using System;
using System.Collections.Generic;

namespace LambdaCalculus
{
    internal class VariableExpression : IExpression, IEquatable<VariableExpression>
    {
        public VariableExpression(string token)
        {
            Token = token;
        }

        public string Token
        {
            get;
            private set;
        }

        #region IExpression Members

        public IExpression Replace(VariableExpression old_expression, IExpression expression)
        {
            if (Token.Equals(old_expression))
            {
                return expression;
            }
            else
            {
                return this;
            }
        }

        public IEnumerable<VariableExpression> GetVariables()
        {
            return this;
        }

        #endregion IExpression Members

        #region IEquatable<TokenExpression> Members

        public bool Equals(VariableExpression other)
        {
            return string.Equals(this.Token, other.Token);
        }

        #endregion IEquatable<TokenExpression> Members

        public static implicit operator VariableExpression(string str)
        {
            return new VariableExpression(str);
        }
    }
}
