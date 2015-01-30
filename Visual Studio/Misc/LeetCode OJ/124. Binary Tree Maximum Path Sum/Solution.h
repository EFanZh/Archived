#pragma once

/**
* Definition for binary tree
* struct TreeNode {
*     int val;
*     TreeNode *left;
*     TreeNode *right;
*     TreeNode(int x) : val(x), left(NULL), right(NULL) {}
* };
*/
class Solution
{
    pair<int, int> maxPathSumHelper(TreeNode *node)
    {
        if (node == nullptr)
        {
            return { numeric_limits<int>::min(), numeric_limits<int>::min() };
        }
        else
        {
            auto left = maxPathSumHelper(node->left);
            auto right = maxPathSumHelper(node->right);

            int maxResult = max({ left.first, right.first, max(left.second, 0) + max(right.second, 0) + node->val });
            int maxPath = max({ left.second, right.second, 0 }) + node->val;

            return { maxResult, maxPath };
        }
    }

public:
    int maxPathSum(TreeNode *root)
    {
        return maxPathSumHelper(root).first;
    }
};
