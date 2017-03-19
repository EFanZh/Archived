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
    vector<vector<int>> levelOrderBottom(TreeNode *root)
    {
        vector<vector<int>> result;

        if (root == nullptr)
        {
            return result;
        }

        vector<const TreeNode *> current = { root };

        while (!current.empty())
        {
            vector<const TreeNode *> nextLevel;

            result.emplace_back();
            for (auto *node : current)
            {
                result.back().emplace_back(node->val);
                if (node->left != nullptr)
                {
                    nextLevel.emplace_back(node->left);
                }
                if (node->right != nullptr)
                {
                    nextLevel.emplace_back(node->right);
                }
            }

            current = move(nextLevel);
        }

        reverse(result.begin(), result.end());

        return result;
    }
};
