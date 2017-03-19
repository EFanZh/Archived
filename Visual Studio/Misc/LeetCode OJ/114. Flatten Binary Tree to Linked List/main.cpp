// https://leetcode.com/problems/flatten-binary-tree-to-linked-list/

#include "Solution.h"

int main()
{
    const char *testCases[] = { "{ 1, 2, 3 }" };
    Solution s;

    for (const char *testCase : testCases)
    {
        stringstream input(testCase);
        vector<unique_ptr<TreeNode>> pool;

        s.flatten(MakeTree(pool, input));
    }
}
