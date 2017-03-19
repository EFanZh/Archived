#pragma once

class Solution
{
public:
    int hIndex(const vector<int> &citations)
    {
        auto result = 0;
        auto tops = priority_queue<int, vector<int>, greater<int>>();

        for (auto i = size_t(0); i < citations.size(); ++i)
        {
            while (!tops.empty() && tops.top() < static_cast<int>(tops.size()) + 1)
            {
                tops.pop();
            }

            if (static_cast<size_t>(citations[i]) > tops.size())
            {
                tops.push(citations[i]);
            }

            result = max(result, static_cast<int>(tops.size()));
        }

        return result;
    }
};
