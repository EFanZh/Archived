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
    static pair<bool, int> isBalancedHelper(const TreeNode *node)
    {
        if (node == nullptr)
        {
            return { true, 0 };
        }
        else
        {
            auto r1 = isBalancedHelper(node->left);

            if (r1.first)
            {
                auto r2 = isBalancedHelper(node->right);

                if (r2.first && abs(r1.second - r2.second) <= 1)
                {
                    return { true, max(r1.second, r2.second) + 1 };
                }
            }

            return { false, 0 };
        }
    }

public:
    bool isBalanced(TreeNode *root)
    {
        return isBalancedHelper(root).first;
    }
};
