// https://oj.leetcode.com/problems/combination-sum-ii/

#include "Solution.h"

int main()
{
    pair<vector<int>, int> testCases[] = { { { 9,  24, 26, 24, 16, 15, 5,  7,  25, 18, 31, 18, 14, 11,
                                               34, 34, 15, 21, 26, 9,  22, 17, 7,  27, 12, 34, 27, 8,
                                               31, 15, 13, 6,  34, 26, 17, 7,  7,  17, 9,  27, 23, 27,
                                               11, 24, 34, 32, 15, 6,  18, 28, 32, 27, 10, 26, 19 },
                                             22 },
                                           { { 1, 1 }, 1 },
                                           { { 4, 4, 2, 1, 4, 2, 2, 1, 3 }, 6 },
                                           { { 4, 1, 1, 4, 4, 4, 4, 2, 3, 5 }, 10 } };
    Solution s;

    for (auto &testCase : testCases)
    {
        auto result = s.combinationSum2(testCase.first, testCase.second);
    }
}
