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
    TreeNode *createBST(vector<int>::const_iterator it_begin, vector<int>::const_iterator it_end)
    {
        if (it_begin == it_end)
        {
            return nullptr;
        }
        else
        {
            auto it_middle = it_begin + (it_end - it_begin) / 2;
            TreeNode *node = new TreeNode(*it_middle);

            node->left = createBST(it_begin, it_middle);
            node->right = createBST(it_middle + 1, it_end);
        }
    }

public:
    TreeNode *sortedArrayToBST(vector<int> &num)
    {
        return createBST(num.cbegin(), num.cend());
    }
};
