#pragma once

class Solution
{
public:
    vector<int> spiralOrder(vector<vector<int>> &matrix)
    {
        vector<int> result;

        if (!matrix.empty() && !matrix.front().empty())
        {
            size_t rows = matrix.size();
            size_t columns = matrix.front().size();
            size_t levels = min(matrix.size(), matrix.front().size()) / 2;

            for (size_t i = 0; i < levels; ++i)
            {
                result.insert(result.end(), matrix[i].cbegin() + i, matrix[i].cend() - i);

                for (size_t j = i + 1; j < rows - i; ++j)
                {
                    result.emplace_back(matrix[j][columns - 1 - i]);
                }

                result.insert(result.end(), matrix[rows - 1 - i].crbegin() + i + 1, matrix[rows - 1 - i].crend() - i);

                for (size_t j = rows - i - 2; j > i; --j)
                {
                    result.emplace_back(matrix[j][i]);
                }
            }

            if (min(rows, columns) % 2 != 0)
            {
                if (rows < columns)
                {
                    result.insert(result.end(), matrix[levels].cbegin() + levels, matrix[levels].cend() - levels);
                }
                else
                {
                    for (size_t i = levels; i < rows - levels; ++i)
                    {
                        result.insert(result.end(), matrix[i][levels]);
                    }
                }
            }
        }

        return result;
    }
};
