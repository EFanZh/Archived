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
public:
    void flatten(TreeNode *root)
    {
        if (root == nullptr)
        {
            return;
        }

        stack<TreeNode *> s;
        TreeNode *prev = root;

        if (root->right != nullptr)
        {
            s.emplace(root->right);
        }

        if (root->left != nullptr)
        {
            s.emplace(root->left);
            root->left = nullptr;
        }

        while (!s.empty())
        {
            TreeNode *current = s.top();

            s.pop();

            if (current->right != nullptr)
            {
                s.emplace(current->right);
            }

            if (current->left != nullptr)
            {
                s.emplace(current->left);
                current->left = nullptr;
            }

            prev->right = current;
            prev = current;
        }
    }
};
