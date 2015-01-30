#pragma once

class Solution
{
public:
    int maxProduct(int A[], int n)
    {
        bool has_zero = false;
        int result = A[0];

        for (int i = 0; i != n;)
        {
            // Skip zeros.
            while (i != n && A[i] == 0)
            {
                has_zero = true;
                ++i;
            }

            if (i != n)
            {
                int temp = A[i];
                int left = A[i];
                int right = A[i];
                int count = 1;
                ++i;

                while (i != n && A[i] != 0)
                {
                    temp *= A[i];

                    if (temp < 0 && left > 0)
                    {
                        left = temp;
                    }
                    if (A[i] < 0)
                    {
                        right = A[i];
                    }
                    else
                    {
                        right *= A[i];
                    }

                    ++count;
                    ++i;
                }

                if (temp < 0 && count > 1)
                {
                    temp /= max(left, right);
                }

                if (result < temp)
                {
                    result = temp;
                }
            }
        }

        return has_zero ? max(0, result) : result;
    }
};
