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
    static vector<vector<int>> pathSumHelper(TreeNode *root, int sum)
    {
        vector<vector<int>> result;

        if (root->left == nullptr && root->right == nullptr && root->val == sum)
        {
            if (root->val == sum)
            {
                return { { root->val } };
            }
            else
            {
                return {};
            }
        }

        if (root->left != nullptr)
        {
            auto r1 = pathSumHelper(root->left, sum - root->val);

            for (auto &item : r1)
            {
                item.insert(item.begin(), root->val);
            }
            move(r1.begin(), r1.end(), back_inserter(result));
        }

        if (root->right != nullptr)
        {
            auto r2 = pathSumHelper(root->right, sum - root->val);

            for (auto &item : r2)
            {
                item.insert(item.begin(), root->val);
            }
            move(r2.begin(), r2.end(), back_inserter(result));
        }

        return result;
    }

public:
    vector<vector<int>> pathSum(TreeNode *root, int sum)
    {
        if (root == nullptr)
        {
            return {};
        }
        else
        {
            return pathSumHelper(root, sum);
        }
    }
};
