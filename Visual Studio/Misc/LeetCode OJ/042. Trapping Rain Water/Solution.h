#pragma once

class Solution
{
    template <class T>
    static int trapHelper(T first, T last)
    {
        int result = 0;
        int prevMax = 0;

        for (T it = first; it != last; ++it)
        {
            if (*it < prevMax)
            {
                result += prevMax - *it;
            }
            else
            {
                prevMax = *it;
            }
        }

        return result;
    }

public:
    int trap(vector<int> &height)
    {
        if (height.empty())
        {
            return 0;
        }

        size_t firstMax = 0;
        size_t lastMax = 0;

        for (size_t i = 1; i < height.size(); ++i)
        {
            if (height[i] == height[firstMax])
            {
                lastMax = i;
            }
            else if (height[i] > height[firstMax])
            {
                firstMax = i;
                lastMax = i;
            }
        }

        return trapHelper(height.cbegin(), height.cbegin() + firstMax) +
               trapHelper(height.crbegin(), height.crbegin() + (height.size() - 1 - lastMax)) +
               (height[firstMax] * (lastMax - firstMax) -
                accumulate(height.cbegin() + firstMax, height.cbegin() + lastMax, 0));
    }
};
