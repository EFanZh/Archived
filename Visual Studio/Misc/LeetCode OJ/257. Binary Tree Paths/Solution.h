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
    void binaryTreePaths(const TreeNode *root, const string &base, vector<string> &result)
    {
        string current = base + "->" + to_string(root->val);

        if (root->left == nullptr)
        {
            if (root->right == nullptr)
            {
                result.emplace_back(current);
            }
            else
            {
                binaryTreePaths(root->right, current, result);
            }
        }
        else
        {
            binaryTreePaths(root->left, current, result);

            if (root->right != nullptr)
            {
                binaryTreePaths(root->right, current, result);
            }
        }
    }

public:
    vector<string> binaryTreePaths(const TreeNode *root)
    {
        if (root == nullptr)
        {
            return{};
        }

        vector<string> result;
        string base = to_string(root->val);

        if (root->left == nullptr)
        {
            if (root->right == nullptr)
            {
                result.emplace_back(base);
            }
            else
            {
                binaryTreePaths(root->right, base, result);
            }
        }
        else
        {
            binaryTreePaths(root->left, base, result);

            if (root->right != nullptr)
            {
                binaryTreePaths(root->right, base, result);
            }
        }

        return result;
    }
};
