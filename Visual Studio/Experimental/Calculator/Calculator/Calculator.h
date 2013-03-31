#ifndef CALCULATOR_H
#define CALCULATOR_H

#include "Integer.h"

class Calculator
{
public:
  static Integer Plus(const Integer &x, const Integer &y);
  static Integer Minus(const Integer &x, const Integer &y);
};

#endif // CALCULATOR_H
