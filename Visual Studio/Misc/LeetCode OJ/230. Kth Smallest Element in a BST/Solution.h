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
    int kthSmallest(const TreeNode *root, int k)
    {
        stack<const TreeNode *> s;
        size_t i = 1;

        for (auto node = root; node != nullptr; node = node->left)
        {
            s.emplace(node);
        }

        while (!s.empty())
        {
            if (i == k)
            {
                return s.top()->val;
            }

            auto current = s.top();

            s.pop();

            for (auto node = current->right; node != nullptr; node = node->left)
            {
                s.emplace(node);
            }

            ++i;
        }

        return 0;
    }
};
