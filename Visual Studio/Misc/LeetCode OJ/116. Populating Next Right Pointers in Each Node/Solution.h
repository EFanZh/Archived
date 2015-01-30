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
        if (root != nullptr)
        {
            TreeLinkNode *first = root;
            TreeLinkNode *current = root->left;

            while (current != nullptr)
            {
                if (current == root->left)
                {
                    current->next = root->right;
                    current = root->right;
                }
                else
                {
                    if (root->next != nullptr)
                    {
                        root = root->next;
                        current->next = root->left;
                        current = root->left;
                    }
                    else
                    {
                        root = first->left;
                        first = root;
                        current = first->left;
                    }
                }
            }
        }
    }
};
