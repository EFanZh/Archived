using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class AbstractionTerm : ITerm
    {
        public AbstractionTerm(string token, ITerm body)
        {
            Variable = token;
            Body = body;
        }

        public string Variable
        {
            get;
            private set;
        }

        public ITerm Body
        {
            get;
            private set;
        }

        public ITerm Apply(ITerm expression)
        {
            return Body.Substitute(Variable, expression);
        }

        #region ITerm Members

        public ITerm Evaluate(IDictionary<string, ITerm> environment)
        {
            var new_env = new Dictionary<string, ITerm>(environment);
            new_env.Remove(Variable);
            return new AbstractionTerm(Variable, Body.Evaluate(new_env));
        }

        public string[] GetFreeVariables()
        {
            return Body.GetFreeVariables().Except(new[] { Variable }).ToArray();
        }

        public ITerm Substitute(string variable, ITerm expression)
        {
            if (variable == Variable)
            {
                // (λ x . t)[x := r] = λ x . t
                return this;
            }
            else
            {
                var free_variables = expression.GetFreeVariables();
                if (free_variables.Contains(Variable))
                {
                    // Need to do an α-conversion.
                    string t = Utilities.GetFreeVariable(Body.GetFreeVariables().Union(free_variables));
                    return new AbstractionTerm(t, Body.Substitute(Variable, new VariableTerm(t)).Substitute(variable, expression));
                }
                else
                {
                    // (λ y . t)[x := r] = λ y . (t[x := r])
                    return new AbstractionTerm(Variable, Body.Substitute(variable, expression));
                }
            }
        }

        #endregion ITerm Members

        public override string ToString()
        {
            return string.Format("(λ {0} . {1})", Variable, Body);
        }
    }
}
