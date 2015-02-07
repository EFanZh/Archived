// https://oj.leetcode.com/problems/path-sum-ii/

#include "Solution.h"

int main()
{
    pair<const char *, int> testCases[] = { { "{ 1, 2 }", 0 } };
    Solution s;

    for (auto testCase : testCases)
    {
        vector<unique_ptr<TreeNode>> pool;
        stringstream input(testCase.first);

        s.pathSum(MakeTree(pool, input), testCase.second);
    }
}
