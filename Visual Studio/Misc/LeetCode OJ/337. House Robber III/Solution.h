#pragma once

/**
 * Definition for a binary tree node.
 * struct TreeNode {
 *     int val;
 *     TreeNode *left;
 *     TreeNode *right;
 *     TreeNode(int x) : val(x), left(NULL), right(NULL) {}
 * };
 */
class Solution
{
    static pair<int, int> robHelper(const TreeNode *root)
    {
        if (root == nullptr)
        {
            return { 0, 0 };
        }
        else
        {
            const auto left = robHelper(root->left);
            const auto right = robHelper(root->right);
            const auto value = left.first + right.first;

            return { max(root->val + left.second + right.second, value), value };
        }
    }

public:
    int rob(const TreeNode *root)
    {
        return robHelper(root).first;
    }
};
