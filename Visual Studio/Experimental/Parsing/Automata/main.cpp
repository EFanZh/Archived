#include "NFA.h"

int main()
{
    using namespace std;

    int start_state = 1;
    set<int> final_states;
    set<tuple<int, string, int>> transitions;

    final_states.insert(12);

    transitions.emplace(1, "s", 1);
    transitions.emplace(1, NFA::Epsilon(), 2);
    transitions.emplace(2, "d", 3);
    transitions.emplace(3, "d", 3);
    transitions.emplace(3, NFA::Epsilon(), 8);
    transitions.emplace(2, ".", 4);
    transitions.emplace(4, "d", 5);
    transitions.emplace(5, "d", 5);
    transitions.emplace(5, NFA::Epsilon(), 8);
    transitions.emplace(4, NFA::Epsilon(), 8);
    transitions.emplace(2, ".", 6);
    transitions.emplace(6, "d", 7);
    transitions.emplace(7, "d", 7);
    transitions.emplace(7, NFA::Epsilon(), 8);
    transitions.emplace(8, "e", 9);
    transitions.emplace(9, "d", 10);
    transitions.emplace(10, "d", 10);
    transitions.emplace(10, NFA::Epsilon(), 11);
    transitions.emplace(8, NFA::Epsilon(), 11);
    transitions.emplace(11, "s", 11);
    transitions.emplace(11, "[E]", 12);

    NFA nfa(start_state, move(final_states), move(transitions));
    DFA dfa = get<0>(nfa.ToDFA());
    DFA dfa_simplified = get<0>(dfa.Simplify());

    cout << "#states\n";
    set<int> states;
    set<string> alphabet;
    for (auto &transition_1 : dfa_simplified.dfa_transitions)
    {
        for (auto &transition_2 : transition_1.second)
        {
            states.emplace(transition_1.first);
            states.emplace(transition_2.second);
            alphabet.emplace(transition_2.first);
        }
    }

    for (auto state : states)
    {
        cout << state << '\n';
    }

    cout << "#initial\n";
    cout << dfa_simplified.dfa_start_state << '\n';

    cout << "#accepting\n";
    for (auto state : dfa_simplified.dfa_final_states)
    {
        cout << state << '\n';
    }

    cout << "#alphabet\n";
    for (auto &letter : alphabet)
    {
        cout << letter << '\n';
    }

    cout << "#transitions\n";
    for (auto &transition_1 : dfa_simplified.dfa_transitions)
    {
        for (auto &transition_2 : transition_1.second)
        {
            cout << transition_1.first << ':' << transition_2.first << '>' << transition_2.second << '\n';
        }
    }
}
