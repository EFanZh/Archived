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
    vector<int> postorderTraversal(TreeNode *root)
    {
        vector<int> result;
        stack<TreeNode *> s;
        TreeNode *previous = nullptr;

        for (TreeNode *node = root; node != nullptr; node = node->right)
        {
            s.emplace(node);
            while (node->left != nullptr)
            {
                node = node->left;
                s.emplace(node);
            }
        }

        while (!s.empty())
        {
            if (previous == s.top()->left && s.top()->right != nullptr)
            {
                for (TreeNode *node = s.top()->right; node != nullptr; node = node->right)
                {
                    s.emplace(node);
                    while (node->left != nullptr)
                    {
                        node = node->left;
                        s.emplace(node);
                    }
                }
            }
            else
            {
                result.emplace_back(s.top()->val);
                previous = s.top();
                s.pop();
            }
        }

        return result;
    }
};
