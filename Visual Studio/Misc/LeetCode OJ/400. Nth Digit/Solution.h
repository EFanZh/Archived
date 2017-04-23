#pragma once

class Solution
{
public:
    int findNthDigit(int n)
    {
        static const int tenPowers[] = { 0, 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
        static const int digits[] = { 1, 10, 190, 2890, 38890, 488890, 5888890, 68888890, 788888890 };

        const auto i = static_cast<int>(upper_bound(begin(digits), end(digits), n) - begin(digits));
        const auto t = n - digits[i - 1];
        const auto number = tenPowers[i] + t / i;

        return (number / tenPowers[i - t % i]) % 10;
    }
};
