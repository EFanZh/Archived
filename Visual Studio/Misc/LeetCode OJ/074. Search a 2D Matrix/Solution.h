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
            int row_count = matrix.size();
            int column_count = matrix.front().size();
            int left = 0, right = column_count * row_count;

            while (left < right)
            {
                int mid = (left + right) / 2;
                int v = matrix[mid / column_count][mid % column_count];

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
