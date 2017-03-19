#pragma once

class Solution
{
    static uint64_t countDigitOneHelper(uint64_t n)
    {
        static const uint64_t tenPowers[] =
            { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
        static const uint64_t bases[] = { 1, 20, 300, 4000, 50000, 600000, 7000000, 80000000, 900000000, 10000000000 };

        const auto digits = upper_bound(begin(tenPowers), end(tenPowers), n) - begin(tenPowers);

        if (digits <= 1)
        {
            return digits;
        }

        const auto base = bases[digits - 2];
        const auto firstDigit = n / tenPowers[digits - 1];
        const auto remains = n % tenPowers[digits - 1];

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
