#pragma once

class Solution2
{
public:
    void nextPermutation(vector<int> &num)
    {
        size_t i = num.size() - 2;

        for (;;)
        {
            if (i < num.size())
            {
                if (!(num[i] < num[i + 1]))
                {
                    --i;
                }
                else
                {
                    swap(*upper_bound(num.rbegin(), num.rend() - (i + 1), num[i]), num[i]);
                    reverse(num.begin() + (i + 1), num.end());

                    break;
                }
            }
            else
            {
                reverse(num.begin(), num.end());

                break;
            }
        }
    }
};
