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
    static size_t getLeftDepth(const TreeNode *root)
    {
        size_t result = 0;

        while (root != nullptr)
        {
            ++result;
            root = root->left;
        }

        return result;
    }

    static size_t getRightDepth(const TreeNode *root)
    {
        size_t result = 0;

        while (root != nullptr)
        {
            ++result;
            root = root->right;
        }

        return result;
    }

    static size_t countNodesHelper(const TreeNode *root)
    {
        size_t result = 0;
        size_t leftDepth = getLeftDepth(root);
        size_t rightDepth = getRightDepth(root);

        while (root != nullptr)
        {
            if (leftDepth == rightDepth)
            {
                result += (1u << leftDepth) - 1;

                break;
            }
            else
            {
                size_t leftRightDepth = getRightDepth(root->left);

                result += 1u << leftRightDepth;

                if (leftRightDepth == leftDepth - 1)
                {
                    root = root->right;
                    leftDepth = getLeftDepth(root);
                    --rightDepth;
                }
                else
                {
                    root = root->left;
                    --leftDepth;
                    rightDepth = getRightDepth(root);
                }
            }
        }

        return result;
    }

public:
    int countNodes(const TreeNode *root)
    {
        return static_cast<int>(countNodesHelper(root));
    }
};
s