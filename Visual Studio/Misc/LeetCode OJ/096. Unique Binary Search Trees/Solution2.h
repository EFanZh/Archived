#pragma once

// http://en.wikipedia.org/wiki/Catalan_number
class Solution2
{
public:
    int numTrees(int n)
    {
        int result = 1;

        for (int i = 0; i < n; ++i)
        {
            result = result * (i * 2 + 1) * 2 / (i + 2);
        }

        return result;
    }
};
