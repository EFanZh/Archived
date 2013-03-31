#include "utilities.h"

int RoundToInt(double x)
{
  int s = (int)x;
  double t = fabs(x - s);

  if ((t < 0.5) || (t == 0.5 && s % 2 == 0))
  {
    return s;
  }
  else
  {
    if (x < 0)
    {
      return s - 1;
    }
    else
    {
      return s + 1;
    }
  }
}

double Square(double x)
{
  return x * x;
}
