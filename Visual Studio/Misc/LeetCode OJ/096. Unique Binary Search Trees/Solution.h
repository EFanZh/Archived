#pragma once

class Solution
{
public:
    int numTrees(int n)
    {
        if (n == 0)
        {
            return 1;
        }
        else
        {
            int result = 0;

            for (int left = 0; left < n; ++left)
            {
                result += numTrees(left) * numTrees(n - left - 1);
            }

            return result;
        }
    }
};
