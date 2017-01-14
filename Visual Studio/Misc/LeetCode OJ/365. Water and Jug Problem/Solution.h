#pragma once

class Solution
{
    static int gcd(int x, int y)
    {
        for (;;)
        {
            if (y == 0)
            {
                return x;
            }

            int z = x % y;

            if (z == 0)
            {
                return y;
            }

            x = y % z;

            if (x == 0)
            {
                return z;
            }

            y = z % x;
        }
    }

public:
    bool canMeasureWater(int x, int y, int z)
    {
        return z == 0 || (z <= x + y && z % gcd(x, y) == 0);
    }
};
