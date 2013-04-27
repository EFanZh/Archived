#ifndef PRIME_H
#define PRIME_H

// The traditional method, realiable but slow.
int is_prime_1(int n);

// Use Fermat's little theorem, not realiable but fast.
int is_prime_2(int n, int times);

#endif // PRIME_H
