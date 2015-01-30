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
    vector<int> preorderTraversal(TreeNode *root)
    {
        vector<int> result;

        if (root != nullptr)
        {
            stack<TreeNode *> s;
            s.emplace(root);

            while (!s.empty())
            {
                TreeNode *node = s.top();
                result.emplace_back(node->val);
                s.pop();

                if (node->right != nullptr)
                {
                    s.emplace(node->right);
                }

                if (node->left != nullptr)
                {
                    s.emplace(node->left);
                }
            }
        }

        return result;
    }
};
