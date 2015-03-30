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
    static TreeNode *cloneTree(TreeNode *tree)
    {
        if (tree == nullptr)
        {
            return nullptr;
        }
        else
        {
            TreeNode *root = new TreeNode(tree->val);

            root->left = cloneTree(tree->left);
            root->right = cloneTree(tree->right);

            return root;
        }
    }

    static void destroyTree(TreeNode *tree)
    {
        if (tree != nullptr)
        {
            destroyTree(tree->left);
            destroyTree(tree->right);

            delete tree;
        }
    }

    static vector<TreeNode *> generateTreesHelper(int first, int n)
    {
        if (n == 0)
        {
            return { nullptr };
        }

        vector<TreeNode *> result;

        for (int left = 0; left < n; ++left)
        {
            auto r1 = generateTreesHelper(first, left);
            auto r2 = generateTreesHelper(first + left + 1, n - (left + 1));

            for (auto k1 : r1)
            {
                for (auto k2 : r2)
                {
                    TreeNode *root = new TreeNode(first + left);

                    root->left = cloneTree(k1);
                    root->right = cloneTree(k2);

                    result.emplace_back(root);
                }
            }

            for (auto k : r1)
            {
                destroyTree(k);
            }

            for (auto k : r2)
            {
                destroyTree(k);
            }
        }

        return result;
    }

public:
    vector<TreeNode *> generateTrees(int n)
    {
        return generateTreesHelper(1, n);
    }
};
