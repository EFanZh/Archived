#pragma once

class Solution
{
public:
    int hIndex(const vector<int> &citations)
    {
        int result = 0;
        priority_queue<int, vector<int>, greater<int>> tops;

        for (size_t i = 0; i < citations.size(); ++i)
        {
            while (!tops.empty() && tops.top() < static_cast<int>(tops.size()) + 1)
            {
                tops.pop();
            }

            if (citations[i] > tops.size())
            {
                tops.push(citations[i]);
            }

            result = max<int>(result, static_cast<int>(tops.size()));
        }

        return result;
    }
};
