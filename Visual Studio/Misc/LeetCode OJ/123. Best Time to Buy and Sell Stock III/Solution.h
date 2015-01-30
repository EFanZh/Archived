#pragma once

class Solution
{
public:
    int maxProfit(vector<int> &prices)
    {
        if (prices.size() < 2)
        {
            return 0;
        }

        vector<int> profits = { 0 };
        int k = prices.front();

        for (size_t i = 1; i < prices.size(); ++i)
        {
            profits.emplace_back(max(profits.back(), prices[i] - k));
            k = min(k, prices[i]);
        }

        int profit = 0;
        int result = profits.back();

        k = prices.back();
        for (size_t i = prices.size() - 2; i < prices.size(); --i)
        {
            profit = max(profit, k - prices[i]);
            result = max(result, profits[i] + profit);
            k = max(k, prices[i]);
        }

        return result;
    }
};
