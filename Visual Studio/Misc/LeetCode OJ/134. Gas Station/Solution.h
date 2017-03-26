#pragma once

class Solution
{
public:
    int canCompleteCircuit(vector<int> &gas, vector<int> &cost)
    {
        size_t minIndex = 0;
        int minGas = 0;
        int currentGas = 0;

        for (size_t i = 0; i < gas.size(); ++i)
        {
            currentGas += gas[i] - cost[i];

            if (currentGas < minGas)
            {
                minGas = currentGas;
                minIndex = i + 1;
            }
        }

        return currentGas < 0 ? -1 : static_cast<int>(minIndex % gas.size());
    }
};
