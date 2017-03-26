#pragma once

class Solution
{
public:
    int maxArea(const vector<int> &height)
    {
        auto left = size_t(0);
        auto right = height.size() - 1;
        auto result = min(height[left], height[right]) * (right - left);

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

        return static_cast<int>(result);
    }
};
