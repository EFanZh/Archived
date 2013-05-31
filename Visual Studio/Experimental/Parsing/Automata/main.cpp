#include "NFA.h"

int main()
{
  using namespace std;

  string epsilon;

  int start_state = 1;
  set<int> final_states;
  set<tuple<int, string, int>> transitions;

  final_states.insert(4);

  transitions.emplace(1, "a", 4);
  transitions.emplace(1, "x", 2);
  transitions.emplace(2, "x", 3);
  transitions.emplace(3, "a", 4);
  transitions.emplace(3, NFA::Epsilon(), 1);

  NFA nfa(start_state, move(final_states), move(transitions));
  DFA dfa = get<0>(nfa.ToDFA());
  DFA dfa_simplified = get<0>(dfa.Simplify());

  cout << "Done." << endl;
}
