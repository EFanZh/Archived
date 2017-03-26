#pragma once

class Solution
{
public:
    int removeDuplicates(int A[], int n)
    {
        return static_cast<int>(unique(A, A + n) - A);
    }
};
