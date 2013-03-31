#include "Grammar.h"

using std::find;
using std::make_pair;
using std::set_intersection;
using std::set_union;

Grammar::Grammar(void)
{
  start_symbol = TS("S");

  {
    Symbol t[] = { TS("S"), TS("T"), TS("A"), TS("B"), TS("C") };
    non_terminals.insert(t, t + sizeof(t) / sizeof(*t));
  }

  {
    Symbol t[] = { TS("a"), TS("t"), TS("b"), TS("c"), TS("x") };
    terminals.insert(t, t + sizeof(t) / sizeof(*t));
  }

  String a, b;

  // S_s \to S T
  a.push_back(TS("S"));
  b.push_back(TS("S"));
  b.push_back(TS("T"));
  rules.insert(make_pair(a, b));

  // S \to A a
  a.clear();
  a.push_back(TS("S"));
  b.clear();
  b.push_back(TS("A"));
  b.push_back(TS("a"));
  rules.insert(make_pair(a, b));

  // T \to A t
  a.clear();
  a.push_back(TS("T"));
  b.clear();
  b.push_back(TS("A"));
  b.push_back(TS("t"));
  rules.insert(make_pair(a, b));

  // A \to B b
  a.clear();
  a.push_back(TS("A"));
  b.clear();
  b.push_back(TS("B"));
  b.push_back(TS("b"));
  rules.insert(make_pair(a, b));

  // B \to C c
  a.clear();
  a.push_back(TS("B"));
  b.clear();
  b.push_back(TS("C"));
  b.push_back(TS("c"));
  rules.insert(make_pair(a, b));

  // C \to x
  a.clear();
  a.push_back(TS("C"));
  b.clear();
  b.push_back(TS("x"));
  rules.insert(make_pair(a, b));

  GetLeftCornerSet();
}

Grammar::~Grammar(void)
{
}

bool Grammar::Validate() const
{
  // V_N \cap V_T = \varnothing
  {
    String t;
    set_intersection(non_terminals.begin(), non_terminals.end(), terminals.begin(), terminals.end(), t.begin());
    if (t.size() > 0)
    {
      return false;
    }
  }

  {
    String t;
    set_union(non_terminals.begin(), non_terminals.end(), terminals.begin(), terminals.end(), t.begin());
    auto first = t.begin();
    auto last = t.end();

    for (auto &p : rules)
    {
      // P \in \left(V_N \cup V_T\right)^+
      if (p.first.size() == 0)
      {
        return false;
      }
      for (auto &s : p.first)
      {
        if (find(first, last, s) == last)
        {
          return false;
        }
      }

      // P \in \left(V_N \cup V_T\right)^*
      for (auto &s : p.second)
      {
        if (find(first, last, s) == last)
        {
          return false;
        }
      }
    }
  }

  // S \in V_N
  if (non_terminals.find(start_symbol) == non_terminals.end())
  {
    return false;
  }

  return true;
}

void Grammar::GetLeftCornerSet() const
{
  using std::pair;

  set<pair<Symbol, Symbol>> result;

  for (auto &rule : rules)
  {
    if (rule.first.size() > 1)
    {
      return;
    }
    if (find(non_terminals.begin(), non_terminals.end(), rule.first[0]) == non_terminals.end())
    {
      return;
    }
    if (rule.second.size() == 0)
    {
      continue;
    }
    if (find(non_terminals.begin(), non_terminals.end(), rule.second[0]) == non_terminals.end())
    {
      continue;
    }
    result.insert(make_pair(rule.second[0], rule.first[0]));
  }

  set<pair<Symbol, Symbol>> new_rules = result;
  while (true)
  {
    set<pair<Symbol, Symbol>> new_rules_2;
    for (auto &i : result)
    {
      for (auto &j : new_rules)
      {
        if (i.second == j.first)
        {
          auto rule = make_pair(i.first, j.second);
          if (result.find(rule) == result.end())
          {
            new_rules_2.insert(rule);
          }
        }
        else if(j.second == i.first)
        {
          auto rule = make_pair(j.first, i.second);
          if (result.find(rule) == result.end())
          {
            new_rules_2.insert(rule);
          }
        }
      }
    }
    if (new_rules_2.size() == 0)
    {
      break;
    }
    result.insert(new_rules_2.begin(), new_rules_2.end());
    new_rules = new_rules_2;
  }

  for (auto &i : result)
  {
    std::TF(cout) << i.first << TS(" < ") << i.second << std::endl;
  }
}
