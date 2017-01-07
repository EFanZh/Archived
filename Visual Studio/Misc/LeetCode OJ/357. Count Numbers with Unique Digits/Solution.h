#pragma once

template <int N>
struct Factorial
{
    static constexpr auto value = Factorial<N - 1>::value * N;
};

template <>
struct Factorial<0>
{
    static constexpr auto value = 1;
};

template <int M, int N>
struct Permutation
{
    static constexpr auto value = Factorial<M>::value / Factorial<M - N>::value;
};

template <int N>
struct Helper
{
    static constexpr auto value = Helper<N - 1>::value + 9 * Permutation<9, N - 1>::value;
};

template <>
struct Helper<0>
{
    static constexpr auto value = 1;
};

class Solution
{
public:
    int countNumbersWithUniqueDigits(int n)
    {
        static const int results[] = { Helper<0>::value, Helper<1>::value, Helper<2>::value, Helper<3>::value,
                                       Helper<4>::value, Helper<5>::value, Helper<6>::value, Helper<7>::value,
                                       Helper<8>::value, Helper<9>::value, Helper<10>::value };

        return results[min(n, 10)];
    }
};
