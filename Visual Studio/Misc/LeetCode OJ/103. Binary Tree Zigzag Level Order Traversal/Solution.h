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
    vector<vector<int>> zigzagLevelOrder(TreeNode *root)
    {
        if (root == nullptr)
        {
            return {};
        }

        vector<vector<int>> result = { { root->val } };
        vector<TreeNode *> current = { { root } };
        bool ltr = false;

        for (;;)
        {
            vector<TreeNode *> newNodes;

            if (ltr)
            {
                for (auto it = current.crbegin(); it != current.crend(); ++it)
                {
                    if ((*it)->left != nullptr)
                    {
                        newNodes.emplace_back((*it)->left);
                    }
                    if ((*it)->right != nullptr)
                    {
                        newNodes.emplace_back((*it)->right);
                    }
                }
            }
            else
            {
                for (auto it = current.crbegin(); it != current.crend(); ++it)
                {
                    if ((*it)->right != nullptr)
                    {
                        newNodes.emplace_back((*it)->right);
                    }
                    if ((*it)->left != nullptr)
                    {
                        newNodes.emplace_back((*it)->left);
                    }
                }
            }

            if (newNodes.empty())
            {
                break;
            }
            else
            {
                result.emplace_back();
                transform(newNodes.cbegin(), newNodes.cend(), back_inserter(result.back()), [](const TreeNode *node) { return node->val; });
                current = move(newNodes);

                ltr = !ltr;
            }
        }

        return result;
    }
};
