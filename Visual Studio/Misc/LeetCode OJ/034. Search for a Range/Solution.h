#pragma once

class Solution
{
public:
    vector<int> searchRange(int A[], int n, int target)
    {
        auto it1 = lower_bound(A, A + n, target);

        if (it1 == A + n || *it1 != target)
        {
            return { -1, -1 };
        }
        else
        {
            return { static_cast<int>(it1 - A), static_cast<int>(upper_bound(it1 + 1, A + n, target) - A - 1) };
        }
    }
};
