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
    vector<vector<int>> levelOrder(TreeNode *root)
    {
        if (root == nullptr)
        {
            return {};
        }

        vector<vector<int>> result = { { root->val } };
        vector<TreeNode *> v = { root };

        for (;;)
        {
            vector<TreeNode *> currentLevel;

            currentLevel.reserve(v.size() * 2);
            for (auto *node : v)
            {
                if (node->left != nullptr)
                {
                    currentLevel.emplace_back(node->left);
                }
                if (node->right != nullptr)
                {
                    currentLevel.emplace_back(node->right);
                }
            }

            if (currentLevel.empty())
            {
                break;
            }
            else
            {
                result.emplace_back();
                for (auto *node : currentLevel)
                {
                    result.back().emplace_back(node->val);
                }
                v = move(currentLevel);
            }
        }

        return result;
    }
};
