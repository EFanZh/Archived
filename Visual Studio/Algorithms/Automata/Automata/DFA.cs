using System.Collections.Generic;
using System.Linq;

namespace Automata
{
    class DFA<TState, TSymbol> : FA<TState, TSymbol>
    {
        public Dictionary<KeyValuePair<TState, TSymbol>, TState> TransitionRelation
        {
            get;
            set;
        }

        public TState DeadState
        {
            get;
            set;
        }

        public HashSet<HashSet<TState>> DivideStates()
        {
            var hs = new HashSet<HashSet<TState>>()
            {
                AcceptingStates,
                new HashSet<TState>(States.Except(AcceptingStates))
            };
            int setCount;
            do
            {
                setCount = hs.Count;
                hs = DivideStateSets(hs);
            } while (setCount < hs.Count);
            return hs;
        }

        private HashSet<HashSet<TState>> DivideStateSets(HashSet<HashSet<TState>> stateSets)
        {
            var hs = new HashSet<HashSet<TState>>(stateSets);
            foreach (var symbol in InputSymbols)
            {
                hs = DivideStateSets(hs, symbol);
            }
            return hs;
        }

        private HashSet<HashSet<TState>> DivideStateSets(HashSet<HashSet<TState>> stateSets, TSymbol symbol)
        {
            var hs = new HashSet<HashSet<TState>>(stateSets);
            foreach (var stateSet in stateSets)
            {
                var s = DivideStateSet(stateSets, symbol, stateSet);
                if (s.Count > 1)
                {
                    hs.Remove(stateSet);
                    hs.UnionWith(s);
                }
            }
            return hs;
        }

        private HashSet<HashSet<TState>> DivideStateSet(HashSet<HashSet<TState>> stateSets, TSymbol symbol, HashSet<TState> stateSet)
        {
            var hs = new HashSet<HashSet<TState>>();
            foreach (var set in stateSets)
            {
                var s = from state in stateSet where set.Contains(GetNextState(state, symbol)) select state;
                if (s.Count() > 0)
                {
                    hs.Add(new HashSet<TState>(s));
                }
            }
            return hs;
        }

        public void AddDeadState()
        {
            bool flag = false;
            foreach (var state in States)
            {
                foreach (var symbol in InputSymbols)
                {
                    var key = new KeyValuePair<TState, TSymbol>(state, symbol);
                    if (!TransitionRelation.ContainsKey(key))
                    {
                        TransitionRelation[key] = DeadState;
                        flag = true;
                    }
                    TransitionRelation[new KeyValuePair<TState, TSymbol>(DeadState, symbol)] = DeadState;
                }
            }
            if (flag)
            {
                foreach (var symbol in InputSymbols)
                {
                    TransitionRelation[new KeyValuePair<TState, TSymbol>(DeadState, symbol)] = DeadState;
                }
                States.Add(DeadState);
            }
        }

        public void RemoveDeadState()
        {
            var dict = new Dictionary<KeyValuePair<TState, TSymbol>, TState>(TransitionRelation);
            foreach (var tr in TransitionRelation)
            {
                if (tr.Value.Equals(DeadState))
                {
                    dict.Remove(tr.Key);
                }
            }
            foreach (var symbol in InputSymbols)
            {
                dict.Remove(new KeyValuePair<TState, TSymbol>(DeadState, symbol));
            }
            TransitionRelation = dict;
            States.Remove(DeadState);
        }

        public TState GetNextState(TState state, TSymbol symbol)
        {
            return TransitionRelation[new KeyValuePair<TState, TSymbol>(state, symbol)];
        }

        public HashSet<TState> GetNextStates(HashSet<TState> states, TSymbol symbol)
        {
            var hs = new HashSet<TState>();
            foreach (var state in states)
            {
                hs.Add(GetNextState(state, symbol));
            }
            return hs;
        }
    }
}
