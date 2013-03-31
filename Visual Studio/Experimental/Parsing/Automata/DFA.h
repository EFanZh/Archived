#ifndef DFA_H
#define DFA_H

#include "FA.h"

using std::map;
using std::pair;

template<class TState, class TSymbol>
class DFA : public FA<TState, TSymbol>
{
  map<pair<TState, TSymbol>, TState> transition_function;
};

#endif // DFA_H
