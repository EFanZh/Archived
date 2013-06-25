using Interpreter.Parse;

namespace Interpreter.Semantics
{
    internal class IfExpression : IExpression
    {
        public IfExpression(IExpression predicate, IExpression consequent, IExpression alternative)
        {
            Predicate = predicate;
            Consequent = consequent;
            Alternative = alternative;
        }

        public IExpression Predicate
        {
            get;
            private set;
        }

        public IExpression Consequent
        {
            get;
            private set;
        }

        public IExpression Alternative
        {
            get;
            private set;
        }
    }
}
