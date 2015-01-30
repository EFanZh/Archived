#pragma once

static const int factorials[] = { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600 };

class Solution
{
    static size_t getNthUnused(uint16_t used, size_t i)
    {
        size_t j = 0;
        size_t count = 0;

        for (;;)
        {
            if (!(used & (1 << j)))
            {
                if (count == i)
                {
                    break;
                }

                ++count;
            }

            ++j;
        }

        return j;
    }

    static size_t getFirstUnused(uint16_t used)
    {
        size_t i = 0;

        while (used & (1 << i))
        {
            ++i;
        }

        return i;
    }

public:
    string getPermutation(int n, int k)
    {
        string result;
        uint16_t used = 0;

        --k;

        while (n > 1)
        {
            int first = k / factorials[n - 1];
            size_t use = getNthUnused(used, first);

            result += static_cast<char>('1' + use);

            --n;
            k -= factorials[n] * first;
            used |= (1 << use);
        }

        result += static_cast<char>('1' + getFirstUnused(used));

        return result;
    }
};
