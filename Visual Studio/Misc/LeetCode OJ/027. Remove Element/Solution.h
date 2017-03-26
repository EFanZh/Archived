#pragma once

class Solution
{
public:
    int removeElement(int A[], int n, int elem)
    {
        return static_cast<int>(remove(A, A + n, elem) - A);
    }
};
