#pragma once

class Solution
{
public:
    vector<vector<int>> generate(int numRows)
    {
        vector<vector<int>> result;
        result.resize(numRows);

        for (int i = 0; i != numRows; ++i)
        {
            result[i].resize(i + 1);

            for (int j = 0; j <= i; ++j)
            {
                if (j == 0 || j == i)
                {
                    result[i][j] = 1;
                }
                else
                {
                    result[i][j] = result[i - 1][j - 1] + result[i - 1][j];
                }
            }
        }

        return result;
    }
};
