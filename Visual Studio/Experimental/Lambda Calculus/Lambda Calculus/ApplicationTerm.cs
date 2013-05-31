using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class ApplicationTerm : Term
    {
        public ApplicationTerm(VariableTerm function, Term parameter) :
            this((Term)function, parameter)
        {
        }

        public ApplicationTerm(Term function, VariableTerm parameter) :
            this(function, (Term)parameter)
        {
        }

        public ApplicationTerm(VariableTerm function, VariableTerm parameter) :
            this((Term)function, (Term)parameter)
        {
        }

        public ApplicationTerm(Term function, Term parameter)
        {
            Function = function;
            Parameter = parameter;
        }

        public Term Function
        {
            get;
            private set;
        }

        public Term Parameter
        {
            get;
            private set;
        }

        public override Term Evaluate(EvaluationOrder evaluation_order)
        {
            if (Function is VariableTerm)
            {
                return new ApplicationTerm(Function, Parameter.Evaluate(evaluation_order));
            }
            else if (Function is AbstractionTerm)
            {
                switch (evaluation_order)
                {
                    case EvaluationOrder.CallByName:
                        return (Function as AbstractionTerm).Convert(Parameter).Evaluate(evaluation_order);
                    case EvaluationOrder.CallByValue:
                        return (Function as AbstractionTerm).Convert(Parameter.Evaluate(evaluation_order)).Evaluate(evaluation_order);
                    default:
                        throw new NotSupportedException("Evaluation order not supported.");
                }
            }
            else if (Function is ApplicationTerm)
            {
                Term func = Function.Evaluate(evaluation_order);
                if (func is AbstractionTerm)
                {
                    switch (evaluation_order)
                    {
                        case EvaluationOrder.CallByName:
                            return (func as AbstractionTerm).Convert(Parameter).Evaluate(evaluation_order);
                        case EvaluationOrder.CallByValue:
                            return (func as AbstractionTerm).Convert(Parameter.Evaluate(evaluation_order)).Evaluate(evaluation_order);
                        default:
                            throw new NotSupportedException("Evaluation order not supported.");
                    }
                }
                else
                {
                    return new ApplicationTerm(func, Parameter.Evaluate(evaluation_order));
                }
            }
            else
            {
                throw new NotSupportedException("Expression type not supported.");
            }
        }

        public override ISet<VariableTerm> GetFreeVariables()
        {
            return new HashSet<VariableTerm>(Function.GetFreeVariables().Union(Parameter.GetFreeVariables()));
        }

        public override Term Substitute(VariableTerm old_expression, Term expression)
        {
            // (t s)[x := r] = (t[x := r]) (s[x := r])
            return new ApplicationTerm(Function.Substitute(old_expression, expression), Parameter.Substitute(old_expression, expression));
        }

        public override string ToString()
        {
            string left;
            if (Function is AbstractionTerm)
            {
                left = string.Format("({0})", Function);
            }
            else
            {
                left = Function.ToString();
            }

            string right;
            if (Parameter is ApplicationTerm)
            {
                right = string.Format("({0})", Parameter);
            }
            else
            {
                right = Parameter.ToString();
            }

            return string.Format("{0} {1}", left, right);
        }
    }
}
