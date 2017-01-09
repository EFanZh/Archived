// https://leetcode.com/problems/max-sum-of-sub-matrix-no-larger-than-k/

#include "Solution.h"

int main()
{
    pair<vector<vector<int>>, int> testCases[] = { { { { 1, 0, 1 }, { 0, -2, 3 } }, 2 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.maxSumSubmatrix(testCase.first, testCase.second);
    }
}
