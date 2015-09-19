#pragma once

class Solution
{
    static size_t countDigitOneHelper(size_t n)
    {
        static const size_t tenPowers[] = { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
        static const size_t bases[] = { 1, 20, 300, 4000, 50000, 600000, 7000000, 80000000, 900000000, 10000000000 };

        size_t digits = upper_bound(begin(tenPowers), end(tenPowers), n) - begin(tenPowers);

        if (digits <= 1)
        {
            return digits;
        }

        size_t base = bases[digits - 2];
        size_t firstDigit = n / tenPowers[digits - 1];
        size_t remains = n % tenPowers[digits - 1];

        if (firstDigit == 1)
        {
            return base + (remains + 1) + countDigitOneHelper(remains);
        }
        else
        {
            return base * firstDigit + tenPowers[digits - 1] + countDigitOneHelper(remains);
        }
    }

public:
    int countDigitOne(int n)
    {
        return n < 0 ? 0 : static_cast<int>(countDigitOneHelper(n));
    }
};
