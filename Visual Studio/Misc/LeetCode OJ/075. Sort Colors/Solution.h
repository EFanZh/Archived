#pragma once

class Solution
{
public:
    void sortColors(int A[], int n)
    {
        array<size_t, 3> colors = {};

        for (int i = 0; i < n; ++i)
        {
            ++colors[A[i]];
        }

        size_t k = 0;

        for (size_t i = 0; i < colors.size(); ++i)
        {
            fill(A + k, A + k + colors[i], i);
            k += colors[i];
        }
    }
};
