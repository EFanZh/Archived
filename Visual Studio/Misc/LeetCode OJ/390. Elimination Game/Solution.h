#pragma once

class Solution
{
    static unsigned int fromLeft(unsigned int n)
    {
        if (n == 1)
        {
            return 1;
        }
        else
        {
            return fromRight(n / 2) * 2;
        }
    }

    static unsigned int fromRight(unsigned int n)
    {
        if (n == 1)
        {
            return 1;
        }
        else
        {
            return fromLeft(n / 2) * 2 - ((n + 1) % 2);
        }
    }

public:
    int lastRemaining(int n)
    {
        return static_cast<int>(fromLeft(n));
    }
};
