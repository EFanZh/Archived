using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PInvokeHelper.Parser
{
    internal class SymbolDeclaration
    {
        public SymbolDeclaration(Type type, Symbol[] declaredSymbols)
        {
            Type = type;
            DeclaredSymbols = declaredSymbols;
        }

        public Type Type
        {
            get;
        }

        public Symbol[] DeclaredSymbols
        {
            get;
        }
    }
}
