#ifndef GRAMMAR_H
#define GRAMMAR_H

#include "Rule.h"
#include "Symbol.h"

using std::set;

class Grammar
{
  set<Symbol> non_terminals, terminals;
  set<Rule> rules;
  Symbol start_symbol;

public:
  Grammar(void);
  ~Grammar(void);

  bool Validate() const;
  void GetLeftCornerSet() const;
};

#endif // GRAMMAR_H
