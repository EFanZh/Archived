#include <stdlib.h>
#include "prime.h"

// Search for divisors, the traditional way to judge if a number is prime.
int is_prime_1(int n)
{
  int i = 2;

  while (i * i <= n)
  {
    if (n % i == 0)
    {
      return 0;
    }
    i++;
  }
  return 1;
}

// Calculate base ^ exp % m.
int expmod(int base, int exp, int m)
{
  if (exp == 0)
  {
    return 1;
  }
  else if (exp % 2 == 0)
  {
    int t = expmod(base, exp / 2, m);
    return t * t % m;
  }
  else
  {
    return base * expmod(base, exp - 1, m) % m;
  }
}

// The Fermat test.
// https://en.wikipedia.org/wiki/Fermat%27s_little_theorem
//
// This is not a reliable test method, the number that passed the test just
// have a good chance to be prime.
int is_prime_2(int n, int times)
{
  int i;

  for (i = 0; i < times; i++)
  {
    // Generate a random a from 1 to n - 1.
    int a = rand() % (n - 1) + 1;

    // Test if a ^ n % n == a, a is in [1, n - 1].
    if (expmod(a, n, n) != a)
    {
      return 0;
    }
  }
  return 1;
}
