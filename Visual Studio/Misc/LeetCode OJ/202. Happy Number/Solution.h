#pragma once

class Solution
{
    static int next(int n)
    {
        int result = 0;

        for (; n > 0; n /= 10)
        {
            int t = n % 10;

            result += t * t;
        }

        return result;
    }

public:
    bool isHappy(int n)
    {
        if (n == 1)
        {
            return true;
        }

        unordered_set<int> seen = { n };

        for (;;)
        {
            n = next(n);

            if (n == 1)
            {
                return true;
            }
            else if (seen.count(n) == 0)
            {
                seen.emplace(n);
            }
            else
            {
                break;
            }
        }

        return false;
    }
};
