// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/

#include "Solution.h"

int main()
{
    pair<vector<int>, int> testCases[] = { { { 2, 3, 4 }, 6 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.twoSum(testCase.first, testCase.second);
    }
}
