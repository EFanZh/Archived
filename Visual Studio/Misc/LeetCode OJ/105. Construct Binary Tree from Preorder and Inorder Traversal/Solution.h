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
    template <class T>
    static TreeNode *buildTreeHelper(T preorderFirst, T preorderLast, T inorderFirst, T inorderLast)
    {
        if (preorderFirst == preorderLast)
        {
            return nullptr;
        }

        TreeNode *root = new TreeNode(*preorderFirst);

        auto k = inorderFirst;

        while (*k != *preorderFirst)
        {
            ++k;
        }

        root->left = buildTreeHelper(preorderFirst + 1, preorderFirst + 1 + (k - inorderFirst), inorderFirst, k);
        root->right = buildTreeHelper(preorderFirst + 1 + (k - inorderFirst), preorderLast, k + 1, inorderLast);

        return root;
    }

public:
    TreeNode *buildTree(vector<int> &preorder, vector<int> &inorder)
    {
        return buildTreeHelper(preorder.cbegin(), preorder.cend(), inorder.cbegin(), inorder.cend());
    }
};
