#pragma once

// Note: Not working.
class Solution
{
    // Because std::abs may not handle INT_MIN well.
    static unsigned int abs(int x)
    {
        return x < 0 ? -x : x;
    }

public:
    int divide(int dividend, int divisor)
    {
        unsigned int positiveDividend = abs(dividend);
        unsigned int positiveDivisor = abs(divisor);
        int positiveResult = 0;

        for (uintmax_t currentDivisor = positiveDivisor; positiveDividend >= positiveDivisor; currentDivisor = positiveDivisor)
        {
            uintmax_t currentResult = 1;

            while (currentDivisor <= positiveDividend)
            {
                currentResult <<= 1;
                currentDivisor <<= 1;
            }
            positiveResult += currentResult >> 1;
            positiveDividend -= currentDivisor >> 1;
        }

        return (dividend < 0) == (divisor < 0) ? positiveResult : -positiveResult;
    }
};
