using System.Collections.Generic;
using System.Linq;

namespace Automata
{
    class NFA<TState, TSymbol> : FA<TState, TSymbol>
    {
        public Dictionary<KeyValuePair<TState, TSymbol>, HashSet<TState>> TransitionRelation
        {
            get;
            set;
        }

        public TSymbol EmptySymbol
        {
            get;
            set;
        }

        public DFA<TState, TSymbol> ToDFA(IEnumerable<TState> newStates, TState deadState)
        {
            #region Create DFA.

            var newTransitioRelation = new Dictionary<KeyValuePair<TState, TSymbol>, TState>();
            var tempNewStateList = new List<HashSet<TState>>();
            var dictStates = new Dictionary<HashSet<TState>, TState>(HashSet<TState>.CreateSetComparer());
            var iterNewState = newStates.GetEnumerator();
            var newState0 = GetEClosure(InitialState);
            tempNewStateList.Add(newState0);
            iterNewState.MoveNext();
            dictStates[newState0] = iterNewState.Current;
            for (int i = 0; i < tempNewStateList.Count; i++)
            {
                foreach (var symbol in InputSymbols)
                {
                    // nextStates: next states through symbol
                    var nextStates = GetNextStates(tempNewStateList[i], symbol);

                    if (nextStates.Count > 0)
                    {
                        if (!tempNewStateList.Contains(nextStates, HashSet<TState>.CreateSetComparer()))
                        {
                            tempNewStateList.Add(nextStates);
                            iterNewState.MoveNext();
                            dictStates[nextStates] = iterNewState.Current;
                        }
                        newTransitioRelation[new KeyValuePair<TState, TSymbol>(dictStates[tempNewStateList[i]], symbol)] = dictStates[nextStates];
                    }
                }
            }

            var dfa = new DFA<TState, TSymbol>()
            {
                States = new HashSet<TState>(dictStates.Values),
                InputSymbols = new HashSet<TSymbol>(InputSymbols),
                TransitionRelation = newTransitioRelation,
                InitialState = dictStates[newState0],
                AcceptingStates = new HashSet<TState>(from state in tempNewStateList where state.IsSupersetOf(AcceptingStates) select dictStates[state]),
                DeadState = deadState
            };

            #endregion

            #region Simplify DFA.

            dfa.AddDeadState();
            var div = dfa.DivideStates();
            newTransitioRelation = new Dictionary<KeyValuePair<TState, TSymbol>, TState>();
            dictStates = new Dictionary<HashSet<TState>, TState>(HashSet<TState>.CreateSetComparer());
            iterNewState = newStates.GetEnumerator();
            foreach (var state in div)
            {
                if (state.Contains(dfa.DeadState))
                {
                    dictStates[state] = dfa.DeadState;
                }
                else
                {
                    iterNewState.MoveNext();
                    dictStates[state] = iterNewState.Current;
                }
            }
            newTransitioRelation = new Dictionary<KeyValuePair<TState, TSymbol>, TState>();
            foreach (var state in div)
            {
                foreach (var symbol in InputSymbols)
                {
                    newTransitioRelation.Add(new KeyValuePair<TState, TSymbol>(dictStates[state], symbol), dictStates[(from state2 in div where state2.IsSupersetOf(dfa.GetNextStates(state, symbol)) select state2).Single()]);
                }
            }
            var newDfa = new DFA<TState, TSymbol>()
            {
                States = new HashSet<TState>(dictStates.Values),
                InputSymbols = InputSymbols,
                TransitionRelation = newTransitioRelation,
                InitialState = dictStates[(from state in div where state.Contains(dfa.InitialState) select state).First()],
                AcceptingStates = new HashSet<TState>(from state in div where state.IsSubsetOf(dfa.AcceptingStates) select dictStates[state]),
                DeadState = dfa.DeadState
            };
            newDfa.RemoveDeadState();

            #endregion

            return newDfa;
        }

        private HashSet<TState> GetEClosure(TState state)
        {
            var hs = new HashSet<TState>();
            hs.Add(state);
            var key = new KeyValuePair<TState, TSymbol>(state, EmptySymbol);
            if (TransitionRelation.ContainsKey(key))
            {
                foreach (var nextState in TransitionRelation[key])
                {
                    hs.UnionWith(GetEClosure(nextState));
                }
            }
            return hs;
        }

        private HashSet<TState> GetEClosure(IEnumerable<TState> states)
        {
            var hs = new HashSet<TState>(states);
            foreach (var state in states)
            {
                hs.UnionWith(GetEClosure(state));
            }
            return hs;
        }

        private HashSet<TState> GetNextStates(TState state, TSymbol symbol)
        {
            var hs = new HashSet<TState>();
            foreach (var s in GetEClosure(state))
            {
                var key = new KeyValuePair<TState, TSymbol>(s, symbol);
                if (TransitionRelation.ContainsKey(key))
                {
                    hs.UnionWith(GetEClosure(TransitionRelation[key]));
                }
            }
            return hs;
        }

        private HashSet<TState> GetNextStates(IEnumerable<TState> states, TSymbol symbol)
        {
            var hs = new HashSet<TState>();
            foreach (var s in states)
            {
                hs.UnionWith(GetNextStates(s, symbol));
            }
            return hs;
        }
    }
}
