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
    int minDepthNonNull(TreeNode *root)
    {
        if (root->left == nullptr)
        {
            if (root->right == nullptr)
            {
                return 1;
            }
            else
            {
                return minDepthNonNull(root->right) + 1;
            }
        }
        else
        {
            if (root->right == nullptr)
            {
                return minDepthNonNull(root->left) + 1;
            }
            else
            {
                return min(minDepthNonNull(root->left), minDepthNonNull(root->right)) + 1;
            }
        }
    }

public:
    int minDepth(TreeNode *root)
    {
        return root == nullptr ? 0 : minDepthNonNull(root);
    }
};
