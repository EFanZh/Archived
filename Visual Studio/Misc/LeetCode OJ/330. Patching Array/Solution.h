#pragma once

class Solution
{
public:
    int minPatches(const vector<int> &nums, int n)
    {
        unsigned int max = 0;
        int result = 0;

        for (auto num : nums)
        {
            while (static_cast<unsigned int>(num) > max + 1)
            {
                max += max + 1;

                ++result;

                if (max >= static_cast<unsigned int>(n))
                {
                    return result;
                }
            }

            max += static_cast<unsigned int>(num);

            if (max >= static_cast<unsigned int>(n))
            {
                return result;
            }
        }

        while (max < static_cast<unsigned int>(n))
        {
            max += max + 1;
            ++result;
        }

        return result;
    }
};
