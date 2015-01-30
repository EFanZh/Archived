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
    static bool isValid(TreeNode *node, int minVal, int maxVal)
    {
        if (node == nullptr)
        {
            return true;
        }
        else
        {
            if (node->val <= minVal || node->val >= maxVal)
            {
                return false;
            }
            else
            {
                return isValid(node->left, minVal, node->val) && isValid(node->right, node->val, maxVal);
            }
        }
    }

public:
    bool isValidBST(TreeNode *root)
    {
        if (root == nullptr)
        {
            return true;
        }
        else
        {
            return isValid(root->left, numeric_limits<int>::min(), root->val) &&
                   isValid(root->right, root->val, numeric_limits<int>::max());
        }
    }
};
