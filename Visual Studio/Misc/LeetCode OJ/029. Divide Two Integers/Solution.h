#pragma once

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
        auto positiveDividend = abs(dividend);
        auto positiveDivisor = abs(divisor);
        auto positiveResult = 0;

        for (auto currentDivisor = static_cast<uintmax_t>(positiveDivisor); positiveDividend >= positiveDivisor;
             currentDivisor = positiveDivisor)
        {
            auto currentResult = uintmax_t(1);

            while (currentDivisor <= positiveDividend)
            {
                currentResult <<= 1;
                currentDivisor <<= 1;
            }

            positiveResult += static_cast<int>(currentResult >> 1);
            positiveDividend -= static_cast<int>(currentDivisor >> 1);
        }

        if (positiveResult < 0)
        {
            return (dividend < 0) == (divisor < 0) ? numeric_limits<int>::max() : positiveResult;
        }
        else
        {
            return (dividend < 0) == (divisor < 0) ? positiveResult : -positiveResult;
        }
    }
};
