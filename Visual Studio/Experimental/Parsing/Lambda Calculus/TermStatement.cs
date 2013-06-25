using System.Collections.Generic;

namespace LambdaCalculus
{
    internal class TermStatement : IStatement
    {
        public TermStatement(ITerm term)
        {
            Term = term;
        }

        public ITerm Term
        {
            get;
            private set;
        }

        #region IStatement Members

        public ITerm Evaluate(Dictionary<string, ITerm> environment)
        {
            return Term.Evaluate(environment);
        }

        #endregion IStatement Members

        public override string ToString()
        {
            return Term.ToString();
        }
    }
}
