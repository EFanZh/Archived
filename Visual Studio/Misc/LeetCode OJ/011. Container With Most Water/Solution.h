#pragma once

class Solution
{
public:
    int maxArea(vector<int> &height)
    {
        size_t left = 0;
        size_t right = height.size() - 1;
        size_t result = min(height[left], height[right]) * (right - left);

        while (left < right)
        {
            if (height[left] < height[right])
            {
                ++left;
            }
            else
            {
                --right;
            }

            result = max(result, min(height[left], height[right]) * (right - left));
        }

        return result;
    }
};
