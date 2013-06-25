using System.Collections.Generic;

namespace LambdaCalculus
{
    internal class AssignmentStatement : IStatement
    {
        public AssignmentStatement(string identifier, ITerm term)
        {
            Identifier = identifier;
            Term = term;
        }

        public string Identifier
        {
            get;
            private set;
        }

        public ITerm Term
        {
            get;
            private set;
        }

        #region IStatement Members

        public ITerm Evaluate(Dictionary<string, ITerm> environment)
        {
            var value = Term.Evaluate(environment);

            environment[Identifier] = value;

            return value;
        }

        #endregion IStatement Members

        public override string ToString()
        {
            return string.Format("{0} = {1}", Identifier, Term);
        }
    }
}
