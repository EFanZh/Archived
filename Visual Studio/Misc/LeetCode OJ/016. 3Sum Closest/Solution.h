#pragma once

class Solution
{
public:
    int threeSumClosest(vector<int> &num, int target)
    {
        sort(num.begin(), num.end());

        int result = accumulate(num.cbegin(), num.cbegin() + 3, 0);

        for (size_t i = 0; i < num.size() - 2; ++i)
        {
            size_t j = i + 1;
            size_t k = num.size() - 1;

            while (j < k)
            {
                int sum = num[i] + num[j] + num[k];

                if (sum == target)
                {
                    return sum;
                }
                else if (abs(sum - target) < abs(result - target))
                {
                    result = sum;
                }

                if (sum < target)
                {
                    ++j;
                }
                else
                {
                    --k;
                }
            }
        }

        return result;
    }
};
