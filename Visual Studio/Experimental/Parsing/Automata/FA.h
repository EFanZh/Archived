#ifndef FA_H
#define FA_H

using std::set;

template<class TState, class TSymbol>
class FA
{
  set<TState> states;
  set<TSymbol> input_alphabet;
  TState start_state;
  set<TState> final_states;
};

#endif // FA_H
