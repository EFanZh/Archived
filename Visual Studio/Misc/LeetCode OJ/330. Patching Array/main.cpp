// https://leetcode.com/problems/patching-array/

#include "Solution.h"

int main()
{
    pair<vector<int>, int> testCases[] = {
        { { 1, 2, 2, 6, 34, 38, 41, 44, 47, 47, 56, 59, 62, 73, 77, 83, 87, 89, 94 }, 20 },
        { { 1, 2, 31, 33 }, 2147483647 },
        { { 1, 2, 2 }, 5 },
        { { 1, 5, 10 }, 20 },
        { { 1, 3 }, 6 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.minPatches(testCase.first, testCase.second);
    }
}
