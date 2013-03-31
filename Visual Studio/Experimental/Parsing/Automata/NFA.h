#ifndef NFA_H
#define NFA_H

#include "FA.h"
#include "DFA.h"

using std::map;
using std::pair;
using std::set;

template<class TState, class TSymbol>
class NFA : public FA<TState, TSymbol>
{
  map<pair<TState, TSymbol>, set<TState>> transition_function;

public:
  DFA<set<TState>, TSymbol> ToDFA();
};

#endif // NFA_H
