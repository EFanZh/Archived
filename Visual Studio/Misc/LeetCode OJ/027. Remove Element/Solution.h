#pragma once

class Solution
{
public:
    int removeElement(int A[], int n, int elem)
    {
        return remove(A, A + n, elem) - A;
    }
};
