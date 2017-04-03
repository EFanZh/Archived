#pragma once

class Solution
{
public:
    bool searchMatrix(vector<vector<int>> &matrix, int target)
    {
        if (matrix.empty() || matrix.front().empty())
        {
            return false;
        }
        else
        {
            const auto row_count = matrix.size();
            const auto column_count = matrix.front().size();
            auto left = size_t(0);
            auto right = column_count * row_count;

            while (left < right)
            {
                const auto mid = (left + right) / 2;
                const auto v = matrix[mid / column_count][mid % column_count];

                if (target < v)
                {
                    right = mid;
                }
                else if (target == v)
                {
                    return true;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return false;
        }
    }
};
