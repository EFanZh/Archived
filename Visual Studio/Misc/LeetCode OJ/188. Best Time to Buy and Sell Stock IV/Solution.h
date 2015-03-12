#pragma once

class Solution
{
public:
    int maxProfit(int k, vector<int> &prices)
    {
        int segmentCount = 0;
        int result = 0;

        for (size_t i = 0; i + 1 < prices.size();)
        {
            size_t first = i;

            while (i + 1 < prices.size() && prices[i] <= prices[i + 1])
            {
                ++i;
            }

            if (i > first)
            {
                ++segmentCount;
                result += prices[i] - prices[first];
            }

            while (i + 1 < prices.size() && prices[i] >= prices[i + 1])
            {
                ++i;
            }
        }

        if (k >= segmentCount)
        {
            return result;
        }

        if (k == 0)
        {
            return 0;
        }

        vector<pair<int, int>> cache(k, make_pair(numeric_limits<int>::min(), 0));

        for (size_t i = 0; i < prices.size(); i++)
        {
            cache[0].first = max(cache[0].first, -prices[i]);
            cache[0].second = max(cache[0].second, cache[0].first + prices[i]);
            for (size_t j = 1; j < cache.size(); j++)
            {
                cache[j].first = max(cache[j].first, cache[j - 1].second - prices[i]);
                cache[j].second = max(cache[j].second, cache[j].first + prices[i]);
            }
        }

        return max_element(cache.cbegin(),
                           cache.cend(),
                           [](const pair<int, int> &lhs, const pair<int, int> &rhs)
                           {
                               return lhs.second < rhs.second;
                           })->second;
    }
};
