#include "permutation.h"

// https://en.wikipedia.org/wiki/Permutation#Generation_in_lexicographic_order
int get_next_permutation(int *a, int n)
{
  int i, k = -1, l = 0, t1, t2;

  // Step 1
  for (i = 0; i < n; ++i)
  {
    if (a[i] < a[i + 1]) k = i;
  }
  if (k == -1) return 0;  // Last permutation;

  // Step 2
  for (i = 0; i < n; ++i)
  {
    if (a[k] < a[i]) l = i;
  }

  // Step 3
  t1 = a[k];
  a[k] = a[l];
  a[l] = t1;

  // Step 4
  t2 = (n + k + 1) / 2;
  for (i = k + 1; i < t2; ++i)
  {
    // Swap a[i] and a[n + k - i]
    t1 = a[i];
    a[i] = a[n + k - i];
    a[n + k - i] = t1;
  }
  return 1;
}
