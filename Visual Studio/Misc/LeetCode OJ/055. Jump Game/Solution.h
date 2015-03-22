#pragma once

class Solution
{
public:
    bool canJump(int A[], int n)
    {
        size_t a = 0;
        size_t b = 0;

        while (a <= b)
        {
            size_t oldB = b;

            for (size_t i = a; i <= oldB; ++i)
            {
                b = max(b, i + A[i]);
            }

            a = oldB + 1;

            if (b >= static_cast<size_t>(n - 1))
            {
                return true;
            }
        }

        return false;
    }
};
