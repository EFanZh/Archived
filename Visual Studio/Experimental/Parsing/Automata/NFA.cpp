#include "NFA.h"

template<class TState, class TSymbol>
DFA<set<TState>, TSymbol> NFA<TState, TSymbol>::ToDFA()
{
  using std::unique_ptr;

  auto dfa = std::make_shared<DFA<set<TState>, TSymbol>>();
}
