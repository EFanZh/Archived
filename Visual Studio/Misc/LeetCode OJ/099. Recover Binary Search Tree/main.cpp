// https://oj.leetcode.com/problems/recover-binary-search-tree/

#include "Solution.h"

int main()
{
    const char *testCases[] = { "{ 10, 5, 15, 0, 8, 13, 20, 2, -5, 6, 9, 12, 14, 18, 25 }" };

    Solution s;

    for (auto testCase : testCases)
    {
        vector<unique_ptr<TreeNode>> pool;
        stringstream input(testCase);

        TreeNode *root = MakeTree(pool, input);

        s.recoverTree(root);
    }
}
