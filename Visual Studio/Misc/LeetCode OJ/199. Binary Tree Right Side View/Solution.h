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
    vector<int> rightSideView(TreeNode *root)
    {
        vector<int> result;
        queue<pair<const TreeNode *, size_t>> q;

        if (root != nullptr)
        {
            q.emplace(root, 1);
        }

        while (!q.empty())
        {
            auto current = q.front();

            q.pop();
            if (current.second > result.size())
            {
                result.emplace_back(current.first->val);
            }

            if (current.first->right != nullptr)
            {
                q.emplace(current.first->right, current.second + 1);
            }

            if (current.first->left != nullptr)
            {
                q.emplace(current.first->left, current.second + 1);
            }
        }

        return result;
    }
};
