#pragma once

class Solution
{
public:
    vector<vector<int>> generateMatrix(int n)
    {
        vector<vector<int>> result(n, vector<int>(n));
        int k = 1;
        int levels = n / 2;

        for (int level = 0; level < levels; ++level)
        {
            int level2 = n - 1 - level;
            int offset1 = level2 - level;
            int offset2 = offset1 + offset1;
            int offset3 = offset2 + offset1;

            for (int i = level; i < level2; ++i)
            {
                int i2 = n - 1 - i;
                int first = k + (i - level);

                result[level][i] = first;
                result[i][level2] = first + offset1;
                result[level2][i2] = first + offset2;
                result[i2][level] = first + offset3;
            }

            k += offset2 + offset2;
        }

        if (n % 2 != 0)
        {
            result[levels][levels] = n * n;
        }

        return result;
    }
};
