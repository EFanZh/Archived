#pragma once

class Solution
{
public:
    bool searchMatrix(const vector<vector<int>> &matrix, int target)
    {
        if (matrix.empty() || matrix.front().empty())
        {
            return false;
        }

        size_t rowBegin = 0, rowEnd = matrix.size();
        size_t columnBegin = 0, columnEnd = matrix.front().size();

        while (rowBegin < rowEnd && columnBegin < columnEnd)
        {
            // Get the bottom left corner.
            int current = matrix[rowEnd - 1][columnBegin];

            if (current == target)
            {
                return true;
            }
            else if (current < target)
            {
                ++columnBegin;
            }
            else
            {
                --rowEnd;
            }
        }

        return false;
    }
};
