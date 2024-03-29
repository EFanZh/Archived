#pragma once

class Solution
{
public:
    void rotate(vector<vector<int>> &matrix)
    {
        size_t levels = matrix.size() / 2;

        for (size_t level = 0; level < levels; ++level)
        {
            size_t level2 = matrix.size() - 1 - level;

            for (size_t i = level; i < level2; ++i)
            {
                size_t i2 = matrix.size() - 1 - i;
                int saved = matrix[level][i];

                matrix[level][i] = matrix[i2][level];
                matrix[i2][level] = matrix[level2][i2];
                matrix[level2][i2] = matrix[i][level2];
                matrix[i][level2] = saved;
            }
        }
    }
};
