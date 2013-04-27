#include "exponentiation.h"

// Exponentiation by squaring: https://en.wikipedia.org/wiki/Exponentiation_by_squaring
int exponentiation(int a, int n)
{
  if (n == 0)
  {
    return 1;
  }
  else if (n % 2 == 0)
  {
    int t = exponentiation(a, n / 2);
    return t * t;
  }
  else
  {
    return a * exponentiation(a, n - 1);
  }
}
