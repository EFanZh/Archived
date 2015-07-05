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
    static bool hasPathSumHelper(TreeNode *root, int sum)
    {
        if (root->left == nullptr)
        {
            if (root->right == nullptr)
            {
                return root->val == sum;
            }
            else
            {
                return hasPathSumHelper(root->right, sum - root->val);
            }
        }
        else
        {
            if (root->right == nullptr)
            {
                return hasPathSumHelper(root->left, sum - root->val);
            }
            else
            {
                return hasPathSumHelper(root->right, sum - root->val) || hasPathSumHelper(root->left, sum - root->val);
            }
        }
    }

public:
    bool hasPathSum(TreeNode *root, int sum)
    {
        if (root == nullptr)
        {
            return false;
        }
        else
        {
            return hasPathSumHelper(root, sum);
        }
    }
};
