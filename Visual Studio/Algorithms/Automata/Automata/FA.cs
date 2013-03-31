using System.Collections.Generic;

namespace Automata
{
    class FA<TState, TSymbol>
    {
        public HashSet<TState> States
        {
            get;
            set;
        }

        public HashSet<TSymbol> InputSymbols
        {
            get;
            set;
        }

        public TState InitialState
        {
            get;
            set;
        }

        public HashSet<TState> AcceptingStates
        {
            get;
            set;
        }
    }
}
