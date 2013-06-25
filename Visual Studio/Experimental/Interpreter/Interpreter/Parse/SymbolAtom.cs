using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parse
{
    internal class SymbolAtom : IAtom
    {
        public SymbolAtom(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
