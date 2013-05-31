using System.Collections.Generic;

namespace LambdaCalculus
{
    internal abstract class Term
    {
        public abstract Term Evaluate(EvaluationOrder evaluation_order);

        public abstract ISet<VariableTerm> GetFreeVariables();

        public abstract Term Substitute(VariableTerm old_expression, Term expression);

        public abstract override string ToString();
    }
}
