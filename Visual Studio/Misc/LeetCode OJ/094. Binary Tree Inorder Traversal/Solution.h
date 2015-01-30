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
    vector<int> inorderTraversal(TreeNode *root)
    {
        vector<int> result;
        stack<TreeNode *> s;

        while (root != nullptr)
        {
            s.emplace(root);
            root = root->left;
        }

        while (!s.empty())
        {
            root = s.top();
            s.pop();
            result.emplace_back(root->val);

            root = root->right;
            while (root != nullptr)
            {
                s.emplace(root);
                root = root->left;
            }
        }

        return result;
    }
};
