// https://leetcode.com/problems/coin-change/

#include "Solution.h"

int main()
{
    pair<vector<int>, int> testCases[] = { { { 1, 2, 5 }, 100 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.coinChange(testCase.first, testCase.second);
    }
}
