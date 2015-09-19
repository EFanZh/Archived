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
    static bool getPath(TreeNode *root, TreeNode *node, vector<TreeNode *> &result)
    {
        if (root == nullptr)
        {
            return false;
        }

        result.emplace_back(root);

        if (root == node ||
            getPath(root->left, node, result) ||
            getPath(root->right, node, result))
        {
            return true;
        }
        else
        {
            result.pop_back();

            return false;
        }
    }

public:
    TreeNode *lowestCommonAncestor(TreeNode *root, TreeNode *p, TreeNode *q)
    {
        vector<TreeNode *> path1, path2;

        getPath(root, p, path1);
        getPath(root, q, path2);

        size_t i = 1;
        size_t length = min(path1.size(), path2.size());

        while (i < length && path1[i] == path2[i])
        {
            ++i;
        }

        return path1[i - 1];
    }
};
