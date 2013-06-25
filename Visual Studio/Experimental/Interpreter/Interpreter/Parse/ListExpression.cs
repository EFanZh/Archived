using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Parse
{
    internal class ListExpression : IExpression
    {
        public ListExpression(IEnumerable<IExpression> items)
        {
            Items = items.ToArray();
        }

        public IExpression[] Items
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format("({0})", string.Join(" ", Items.AsEnumerable()));
        }
    }
}
