// https://leetcode.com/problems/minimum-path-sum/

#include "Solution.h"

int main()
{
    vector<vector<int>> testCases[] = { { { 1, 2, 5 }, { 3, 2, 1 } }, { { 0, 1 }, { 1, 0 } }, { { 1, 2 }, { 1, 1 } } };
    Solution s;

    for (auto &testCase : testCases)
    {
        s.minPathSum(testCase);
    }
}
