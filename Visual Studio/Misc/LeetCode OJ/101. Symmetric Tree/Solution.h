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
    static bool isSymmetric(TreeNode *tree1, TreeNode *tree2)
    {
        if (tree1 == tree2)
        {
            return true;
        }
        else
        {
            if (tree1 == nullptr || tree2 == nullptr)
            {
                return false;
            }
            else
            {
                return tree1->val == tree2->val && isSymmetric(tree1->left, tree2->right) &&
                       isSymmetric(tree1->right, tree2->left);
            }
        }
    }

public:
    bool isSymmetric(TreeNode *root)
    {
        return root == nullptr ? true : isSymmetric(root->left, root->right);
    }
};
