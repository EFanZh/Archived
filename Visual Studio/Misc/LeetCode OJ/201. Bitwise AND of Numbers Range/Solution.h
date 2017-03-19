#pragma once

class Solution
{
    static unsigned int rangeBitwiseAndHelper(unsigned int m, unsigned int n)
    {
        unsigned int result = 0;
        unsigned int offset = n - m;

        for (unsigned int i = 0; (1u << i) <= m; ++i)
        {
            unsigned int bit = 1u << i;

            if ((m & bit) && (offset < bit - (m & (bit - 1))))
            {
                result |= bit;
            }
        }

        return result;
    }

public:
    int rangeBitwiseAnd(int m, int n)
    {
        return static_cast<int>(rangeBitwiseAndHelper(m, n));
    }
};
