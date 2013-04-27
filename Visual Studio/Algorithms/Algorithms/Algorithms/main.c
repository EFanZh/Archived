#include <stdio.h>
#include "exponentiation.h"
#include "gcd.h"
#include "permutation.h"
#include "prime.h"

#define PRINT_SEPARATOR() putchar('\n')
#define PRINT_INTEGER(expr) printf(#expr " = %d\n", expr)
#define ARRAYSIZE(arr) (sizeof(arr) / sizeof(*arr))

int main(void)
{
  // Exponentiation
  PRINT_INTEGER(exponentiation(2, 12));

  PRINT_SEPARATOR();

  // Greatest common divisor
  PRINT_INTEGER(gcd(108, 72));

  PRINT_SEPARATOR();

  // All permutation
  {
    int a[] = { 1, 2, 3 }, i;
    do
    {
      for (i = 0; i < ARRAYSIZE(a) - 1; i++)
      {
        printf("%d, ", a[i]);
      }
      printf("%d\n", a[ARRAYSIZE(a) - 1]);
    }
    while (get_next_permutation(a, ARRAYSIZE(a)));
  }

  PRINT_SEPARATOR();

  // Prime number test
  PRINT_INTEGER(is_prime_1(97));
  PRINT_INTEGER(is_prime_1(117421));
  PRINT_INTEGER(is_prime_2(97, 3));
  PRINT_INTEGER(is_prime_2(117421, 3));

  PRINT_SEPARATOR();
}
