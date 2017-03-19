#pragma once

class Solution
{
public:
    int hIndex(const vector<int> &citations)
    {
        return lower_bound(citations.crbegin(), citations.crend(), *citations.crbegin(),
                           [](const int &lhs, const int &rhs) { return lhs >= &rhs - &lhs + 1; }) -
               citations.crbegin();
    }
};
