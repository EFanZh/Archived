#pragma once

class Solution
{
public:
    int maxProfit(vector<int> &prices)
    {
        int profit = 0;
        int min = numeric_limits<int>::max();

        for (auto i : prices)
        {
            if (min > i)
            {
                min = i;
            }
            if (i - min > profit)
            {
                profit = i - min;
            }
        }

        return profit;
    }
};
