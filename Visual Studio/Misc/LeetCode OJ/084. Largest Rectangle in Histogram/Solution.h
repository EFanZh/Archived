#pragma once

class Solution
{
public:
    int largestRectangleArea(vector<int> &height)
    {
        if (height.empty())
        {
            return 0;
        }

        size_t currentMaxArea = 0;
        stack<size_t> s;

        for (size_t i = 0; i < height.size(); ++i)
        {
            while (!s.empty() && height[s.top()] > height[i])
            {
                int minValue = height[s.top()];

                s.pop();
                currentMaxArea = max(currentMaxArea, minValue * (s.empty() ? i : i - 1 - s.top()));
            }

            s.emplace(i);
        }

        while (!s.empty())
        {
            int minValue = height[s.top()];

            s.pop();
            currentMaxArea = max(currentMaxArea, minValue * (s.empty() ? height.size() : height.size() - 1 - s.top()));
        }

        return currentMaxArea;
    }
};
