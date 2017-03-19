// https://leetcode.com/problems/minimum-height-trees/

#include "Solution.h"

int main()
{
    pair<int, vector<pair<int, int>>> testCases[] =
        { { 4, { { 1, 0 }, { 1, 2 }, { 1, 3 } } },
          { 7, { { 0, 1 }, { 1, 2 }, { 1, 3 }, { 2, 4 }, { 3, 5 }, { 4, 6 } } },
          { 6, { { 3, 0 }, { 3, 1 }, { 3, 2 }, { 3, 4 }, { 5, 4 } } } };
    Solution s;

    for (const auto testCase : testCases)
    {
        s.findMinHeightTrees(testCase.first, testCase.second);
    }
}
