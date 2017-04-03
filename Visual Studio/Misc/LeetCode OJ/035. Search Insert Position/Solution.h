#pragma once

class Solution
{
public:
    int searchInsert(int A[], int n, int target)
    {
        return static_cast<int>(lower_bound(A, A + n, target) - A);
    }
};
