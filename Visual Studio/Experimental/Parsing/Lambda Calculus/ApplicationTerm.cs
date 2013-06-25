using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class ApplicationTerm : ITerm
    {
        public ApplicationTerm(ITerm function, ITerm parameter)
        {
            Function = function;
            Parameter = parameter;
        }

        public ITerm Function
        {
            get;
            private set;
        }

        public ITerm Parameter
        {
            get;
            private set;
        }

        #region ITerm Members

        public ITerm Evaluate(IDictionary<string, ITerm> environment)
        {
            ITerm abstraction = Function.Evaluate(environment);
            ITerm parameter = Parameter.Evaluate(environment);

            if (abstraction is AbstractionTerm)
            {
                return (abstraction as AbstractionTerm).Apply(parameter).Evaluate(environment);
            }
            else
            {
                return new ApplicationTerm(abstraction, parameter);
            }
        }

        public string[] GetFreeVariables()
        {
            return Function.GetFreeVariables().Union(Parameter.GetFreeVariables()).ToArray();
        }

        public ITerm Substitute(string variable, ITerm expression)
        {
            // (t s)[x := r] = (t[x := r]) (s[x := r])
            return new ApplicationTerm(Function.Substitute(variable, expression), Parameter.Substitute(variable, expression));
        }

        #endregion ITerm Members

        public override string ToString()
        {
            return string.Format("({0} {1})", Function, Parameter);
        }
    }
}
