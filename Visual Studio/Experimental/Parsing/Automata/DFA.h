#ifndef DFA_H
#define DFA_H

class DFA
{
  int dfa_start_state;
  std::set<int> dfa_final_states;
  std::map<int, std::map<std::string, int>> dfa_transitions;

  std::set<int> GetReachableStates() const
  {
    using namespace std;

    set<int> reachable_states;

    reachable_states.insert(dfa_start_state);

    set<int> new_reachable_states(reachable_states);
    while (!new_reachable_states.empty())
    {
      // Store current round's new reachable states.
      set<int> temp_new_reachable_states;

      for (auto state : new_reachable_states)
      {
        auto state_map_iter = dfa_transitions.find(state);
        if (state_map_iter != dfa_transitions.end())
        {
          for (auto &symbol_state_pair : state_map_iter->second)
          {
            // Not in reachable_states.
            if (reachable_states.find(symbol_state_pair.second) == reachable_states.end())
            {
              reachable_states.insert(symbol_state_pair.second);
              temp_new_reachable_states.insert(symbol_state_pair.second);
            }
          }
        }
      }
      new_reachable_states = move(temp_new_reachable_states);
    }

    return move(reachable_states);
  }

public:
  DFA(int start_state, std::set<int> final_states, std::map<int, std::map<std::string, int>> transitions):
    dfa_start_state(start_state),
    dfa_final_states(std::move(final_states)),
    dfa_transitions(std::move(transitions))
  {
  }

  std::tuple<DFA, std::vector<std::set<int>>> Simplify() const
  {
    using namespace std;

    // Reachable states (which is reacheable from start state).
    auto reachable_states = GetReachableStates();

    // Useful transitions. Eliminated useless states.
    map<int, map<string, set<int>>> reversed_useful_transitions;

    // Useful symbols.
    set<string> useful_symbols;
    for (auto &state_map_pair : dfa_transitions)
    {
      if (reachable_states.find(state_map_pair.first) != reachable_states.end())
      {
        // Notice we only need to ensure for each transition, source state is reachable & target state is alive.
        for (auto &symbol_state_pair : state_map_pair.second)
        {
          reversed_useful_transitions[symbol_state_pair.second][symbol_state_pair.first].insert(state_map_pair.first);
          useful_symbols.insert(symbol_state_pair.first);
        }
      }
    }

    if (useful_symbols.empty())
    {
      // This is an empty DFA.
      return make_tuple(Empty(), vector<set<int>>());
    }

    // Useful final states.
    set<int> useful_final_states;
    auto is_reachable = [&reachable_states] (int state)
    {
      return reachable_states.find(state) != reachable_states.end();
    };
    copy_if(dfa_final_states.begin(), dfa_final_states.end(), inserter(useful_final_states, useful_final_states.begin()), is_reachable);

    // Get the state set which through a symbol to target states.
    auto get_can_direct_reach_states = [&reversed_useful_transitions] (set<int> targets, string symbol)
    {
      set<int> result;

      for (auto state : targets)
      {
        auto state_map_iter = reversed_useful_transitions.find(state);
        if (state_map_iter != reversed_useful_transitions.end())
        {
          auto symbol_states_iter = state_map_iter->second.find(symbol);
          if (symbol_states_iter != state_map_iter->second.end())
          {
            result.insert(symbol_states_iter->second.begin(), symbol_states_iter->second.end());
          }
        }
      }

      return move(result);
    };

    // Simplification.
    // Algorithm from Wikipedia: https://en.wikipedia.org/wiki/DFA_minimization#Hopcroft.27s_algorithm

    vector<set<int>> set_partitions;

    set<set<int>> set_wait;
    set_partitions.push_back(useful_final_states);
    set_wait.insert(useful_final_states);

    set<int> set_t;
    set_difference(reachable_states.begin(), reachable_states.end(), useful_final_states.begin(), useful_final_states.end(), inserter(set_t, set_t.begin()));

    set_partitions.push_back(move(set_t));

    while (!set_wait.empty())
    {
      auto set_splitter_target = *set_wait.begin();
      set_wait.erase(set_wait.begin());
      for (auto &symbol : useful_symbols)
      {
        auto set_splitter_source = get_can_direct_reach_states(set_splitter_target, symbol);
        auto set_partition_iter = set_partitions.begin();
        while (set_partition_iter != set_partitions.end())
        {
          set<int> set_split_intersection;
          set_intersection(set_splitter_source.begin(), set_splitter_source.end(), set_partition_iter->begin(), set_partition_iter->end(), inserter(set_split_intersection, set_split_intersection.begin()));
          if (!set_split_intersection.empty())
          {
            set<int> set_split_difference;
            set_difference(set_partition_iter->begin(), set_partition_iter->end(), set_splitter_source.begin(), set_splitter_source.end(), inserter(set_split_difference, set_split_difference.begin()));
            if (!set_split_difference.empty())
            {
              auto set_wait_iter = set_wait.find(*set_partition_iter);
              if (set_wait_iter != set_wait.end())
              {
                set_wait_iter = set_wait.erase(set_wait_iter);
                set_wait_iter = set_wait.insert(set_wait_iter, set_split_intersection);
                ++set_wait_iter;
                set_wait.insert(set_wait_iter, set_split_difference);
              }
              else
              {
                if (set_split_intersection.size() <= set_split_difference.size())
                {
                  set_wait.insert(set_split_intersection);
                }
                else
                {
                  set_wait.insert(set_split_difference);
                }
              }

              set_partition_iter = set_partitions.erase(set_partition_iter);
              set_partition_iter = set_partitions.insert(set_partition_iter, set_split_intersection);
              ++set_partition_iter;
              set_partition_iter = set_partitions.insert(set_partition_iter, set_split_difference);
              ++set_partition_iter;
            }
            else
            {
              ++set_partition_iter;
            }
          }
          else
          {
            ++set_partition_iter;
          }
        }
      }
    }

    // Combine partition.

    // State to partition map.
    map<int, int> state_map;
    for (int i = 0; i < set_partitions.size(); ++i)
    {
      for (auto state : set_partitions[i])
      {
        state_map[state] = i;
      }
    }

    set<int> new_final_states;
    for (auto state : useful_final_states)
    {
      new_final_states.insert(state_map[state]);
    }

    map<int, map<string, int>> new_dfa_transition;
    for (auto &state_map_pair : reversed_useful_transitions)
    {
      int state_target = state_map[state_map_pair.first];
      for (auto symbol_states_pair : state_map_pair.second)
      {
        for (auto state_source : symbol_states_pair.second)
        {
          new_dfa_transition[state_map[state_source]][symbol_states_pair.first] = state_target;
        }
      }
    }

    return make_tuple(DFA(state_map[dfa_start_state], move(new_final_states), move(new_dfa_transition)), move(set_partitions));
  }

  static const DFA &Empty()
  {
    using namespace std;

    static DFA empty(-1, set<int>(), map<int, map<string, int>>());

    return empty;
  }
};

#endif // DFA_H
