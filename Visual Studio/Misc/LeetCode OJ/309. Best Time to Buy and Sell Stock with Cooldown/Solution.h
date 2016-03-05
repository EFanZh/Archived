#pragma once

class Solution
{
public:
    int maxProfit(const vector<int> &prices)
    {
        if (prices.empty())
        {
            return 0;
        }

        auto buy = -prices.front();
        auto sell1 = 0;
        auto sell2 = 0;

        for (auto i = vector<int>::size_type(1); i < prices.size(); ++i)
        {
            tie(buy, sell1, sell2) = make_tuple(max(buy, sell2 - prices[i]), max(sell1, buy + prices[i]), sell1);
        }

        return sell1;
    }
};
