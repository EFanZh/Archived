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
    template <class T1, class T2>
    static TreeNode *buildTreeHelper(T1 preorderFirst, T1 preorderLast, T2 inorderFirst, T2 inorderLast)
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

        root->left = buildTreeHelper(preorderFirst + (inorderLast - k),
                                     preorderLast,
                                     inorderFirst,
                                     k);
        root->right = buildTreeHelper(preorderFirst + 1,
                                      preorderFirst + (inorderLast - k),
                                      k + 1,
                                      inorderLast);

        return root;
    }

public:
    TreeNode *buildTree(vector<int> &inorder, vector<int> &postorder)
    {
        return buildTreeHelper(postorder.crbegin(), postorder.crend(), inorder.cbegin(), inorder.cend());
    }
};
