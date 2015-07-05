#pragma once

/**
 * Definition for binary tree with next pointer.
 * struct TreeLinkNode {
 *  int val;
 *  TreeLinkNode *left, *right, *next;
 *  TreeLinkNode(int x) : val(x), left(NULL), right(NULL), next(NULL) {}
 * };
 */
class Solution
{
public:
    void connect(TreeLinkNode *root)
    {
        TreeLinkNode *current = root;

        while (current != nullptr)
        {
            TreeLinkNode *previous = nullptr;
            TreeLinkNode *nextLineFirst = nullptr;

            for (TreeLinkNode *node = current; node != nullptr; node = node->next)
            {
                if (node->left != nullptr)
                {
                    if (previous != nullptr)
                    {
                        previous->next = node->left;
                    }
                    else
                    {
                        nextLineFirst = node->left;
                    }

                    previous = node->left;
                }
                if (node->right != nullptr)
                {
                    if (previous != nullptr)
                    {
                        previous->next = node->right;
                    }
                    else
                    {
                        nextLineFirst = node->right;
                    }

                    previous = node->right;
                }
            }

            current = nextLineFirst;
        }
    }
};
