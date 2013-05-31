#ifndef NFA_H
#define NFA_H

#include "DFA.h"

class NFA
{
  int nfa_start_state;
  std::set<int> nfa_final_states;
  std::map<int, std::map<std::string, std::set<int>>> nfa_transitions;

  std::set<int> GetEpsilonClosure(int state) const
  {
    using namespace std;

    set<int> closure;

    closure.insert(state);

    // New states generated in round one.
    auto new_states(closure);
    while (!new_states.empty())
    {
      // Store current round's new states.
      set<int> temp_new_states;
      for (int state : new_states)
      {
        auto state_map_iter = nfa_transitions.find(state);
        if (state_map_iter != nfa_transitions.end())
        {
          auto symbol_states_iter = state_map_iter->second.find(Epsilon());
          if (symbol_states_iter != state_map_iter->second.end())
          {
            for (auto new_state : symbol_states_iter->second)
            {
              if (closure.find(new_state) == closure.end())
              {
                closure.insert(new_state);
                temp_new_states.insert(new_state);
              }
            }
          }
        }
      }
      new_states = move(temp_new_states);
    }

    return move(closure);
  }

  std::set<std::string> GetSymbols() const
  {
    std::set<std::string> symbols;

    for (auto &state_map_pair : nfa_transitions)
    {
      for (auto &symbol_states_pair : state_map_pair.second)
      {
        if (symbol_states_pair.first != Epsilon())
        {
          symbols.insert(symbol_states_pair.first);
        }
      }
    }

    return std::move(symbols);
  }

public:
  NFA(int start_state, std::set<int> final_states, std::set<std::tuple<int, std::string, int>> transitions) :
    nfa_start_state(start_state),
    nfa_final_states(std::move(final_states))
  {
    using std::get;

    for (auto &transition : transitions)
    {
      nfa_transitions[get<0>(transition)][get<1>(transition)].insert(get<2>(transition));
    }
  }

  std::tuple<DFA, std::vector<std::set<int>>> ToDFA() const
  {
    using namespace std;

    // DFA state sets;
    vector<set<int>> dfa_states;

    // DFA transitions.
    map<int, map<string, int>> dfa_transitions;

    // DFA final states.
    set<int> dfa_final_states;

    // Add the start state set.
    dfa_states.push_back(GetEpsilonClosure(nfa_start_state));

    auto contains_final_states = [this] (const set<int> &states)
    {
      return any_of(states.begin(), states.end(), [this] (int elem_1)
      {
        return any_of(nfa_final_states.begin(), nfa_final_states.end(), [elem_1] (int elem_2)
        {
          return elem_1 == elem_2;
        });
      });
    };

    // Determine if start state is final state.
    if (contains_final_states(dfa_states[0]))
    {
      dfa_final_states.insert(0);
    }

    auto symbols = GetSymbols();

    // For each existing DFA state.
    for (size_t i = 0u; i < dfa_states.size(); ++i)
    {
      // For each symbol.
      for (auto &symbol : symbols)
      {
        // Next DFA state.
        set<int> next_dfa_state;

        // For each existing DFA state.
        for (auto state : dfa_states[i])
        {
          // Next state iterator.
          auto state_map_pair_iter = nfa_transitions.find(state);
          if (state_map_pair_iter != nfa_transitions.end())
          {
            auto symbol_states_pair_iter = state_map_pair_iter->second.find(symbol);
            if (symbol_states_pair_iter != state_map_pair_iter->second.end())
            {
              // For each next state from state through symbol.
              for (auto next : symbol_states_pair_iter->second)
              {
                auto next_closure = GetEpsilonClosure(next);
                next_dfa_state.insert(next_closure.begin(), next_closure.end());
              }
            }
          }
        }

        // Make sure the new state is not empty.
        if (!next_dfa_state.empty())
        {
          // Determine if next state already exists.
          auto state_iter = find(dfa_states.begin(), dfa_states.end(), next_dfa_state);
          if (state_iter == dfa_states.end())
          {
            // Determine if next state is final state.
            if (contains_final_states(next_dfa_state))
            {
              dfa_final_states.insert(dfa_states.size());
            }

            // Set DFA transition with the newly added state.
            dfa_transitions[i][symbol] = dfa_states.size();

            // Add the newly generated state to state set.
            dfa_states.push_back(move(next_dfa_state));
          }
          else
          {
            // Set DFA transitions.
            dfa_transitions[i][symbol] = state_iter - dfa_states.begin();
          }
        }
      }
    }

    return make_tuple(DFA(0, move(dfa_final_states), move(dfa_transitions)), move(dfa_states));
  }

  static const std::string &Epsilon()
  {
    static const std::string epsilon;

    return epsilon;
  }
};

#endif // NFA_H
