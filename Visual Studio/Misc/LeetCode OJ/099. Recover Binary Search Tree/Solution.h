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
public:
    void recoverTree(TreeNode *root)
    {
        TreeNode *previous = nullptr;
        vector<pair<TreeNode *, TreeNode *>> targets;

        while (root != nullptr)
        {
            TreeNode *p = root->left;

            // If has a left sub tree.
            if (p != nullptr)
            {
                while (p->right != nullptr && p->right != root)
                {
                    p = p->right;
                }

                // And left sub tree's right most node is null.
                if (p->right == nullptr)
                {
                    p->right = root;
                    root = root->left;

                    continue;
                }
                else
                {
                    p->right = nullptr;
                }
            }

            if (previous != nullptr && root->val < previous->val)
            {
                targets.emplace_back(previous, root);
            }

            previous = root;
            root = root->right;
        }

        swap(targets.front().first->val, targets.back().second->val);
    }
};
