// https://oj.leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/

#include "Solution.h"

int main()
{
    pair<vector<int>, vector<int>> testCases[] = { { { 2, 1, 3 }, { 2, 3, 1 } },
                                                   { { 2, 1 }, { 2, 1 } } };
    Solution s;

    for (auto testCase : testCases)
    {
        s.buildTree(testCase.first, testCase.second);
    }
}
