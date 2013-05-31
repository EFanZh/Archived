using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal class AbstractionTerm : Term
    {
        public AbstractionTerm(VariableTerm token, VariableTerm body) :
            this(token, (Term)body)
        {
        }

        public AbstractionTerm(VariableTerm token, Term body)
        {
            Variable = token;
            Body = body;
        }

        public VariableTerm Variable
        {
            get;
            private set;
        }

        public Term Body
        {
            get;
            private set;
        }

        public Term Convert(Term expression)
        {
            return Body.Substitute(Variable, expression);
        }

        public override Term Evaluate(EvaluationOrder evaluation_order)
        {
            return new AbstractionTerm(Variable, Body.Evaluate(evaluation_order));
        }

        public override ISet<VariableTerm> GetFreeVariables()
        {
            return new HashSet<VariableTerm>(Body.GetFreeVariables().Except(new[] { Variable }));
        }

        public override Term Substitute(VariableTerm old_expression, Term expression)
        {
            if (old_expression == Variable)
            {
                // (¦Ë x . t)[x := r] = ¦Ë x . t
                return this;
            }
            else
            {
                var new_free_variables = expression.GetFreeVariables();
                if (new_free_variables.Contains(Variable))
                {
                    // Need to do an ¦Á-conversion.
                    VariableTerm t = Utilities.GetFreeVariable(Body.GetFreeVariables().Union(new_free_variables));
                    return new AbstractionTerm(t, Body.Substitute(Variable, t).Substitute(old_expression, expression));
                }
                else
                {
                    // (¦Ë y . t)[x := r] = ¦Ë y . (t[x := r])
                    return new AbstractionTerm(Variable, Body.Substitute(old_expression, expression));
                }
            }
        }

        public override string ToString()
        {
            return string.Format("¦Ë {0} . {1}", Variable, Body);
        }
    }
}
