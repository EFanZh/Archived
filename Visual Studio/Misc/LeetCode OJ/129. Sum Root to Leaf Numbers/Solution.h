#pragma once

/**
 * Definition for a binary tree node.
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
    int sumNumbers(TreeNode *root)
    {
        int result = 0;
        stack<pair<const TreeNode *, int>> s;

        if (root != nullptr)
        {
            s.emplace(root, root->val);
        }

        while (!s.empty())
        {
            auto current = s.top();

            s.pop();

            if (current.first->left != nullptr)
            {
                s.emplace(current.first->left, current.second * 10 + current.first->left->val);
                if (current.first->right != nullptr)
                {
                    s.emplace(current.first->right, current.second * 10 + current.first->right->val);
                }
            }
            else
            {
                if (current.first->right != nullptr)
                {
                    s.emplace(current.first->right, current.second * 10 + current.first->right->val);
                }
                else
                {
                    result += current.second;
                }
            }
        }

        return result;
    }
};
