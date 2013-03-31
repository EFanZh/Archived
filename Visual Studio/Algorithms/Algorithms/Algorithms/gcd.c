#include "euclidean_algorithm.h"

// Euclidean Algorithm: https://en.wikipedia.org/wiki/Euclidean_algorithm#Implementations
int gcd(int a, int b)
{
  while (b)
  {
    int t = b;
    b = a % b;
    a = t;
  }
  return a;
}
