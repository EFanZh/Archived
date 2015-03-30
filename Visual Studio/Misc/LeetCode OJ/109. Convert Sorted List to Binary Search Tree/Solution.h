#pragma once

/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     ListNode *next;
 *     ListNode(int x) : val(x), next(NULL) {}
 * };
 */
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
    static size_t getListLength(const ListNode *list)
    {
        size_t result = 0;

        while (list != nullptr)
        {
            ++result;
            list = list->next;
        }

        return result;
    }

    static TreeNode *createTree(size_t n)
    {
        if (n == 0)
        {
            return nullptr;
        }
        else
        {
            TreeNode *root = new TreeNode(0);
            size_t left = (n - 1) / 2;

            root->left = createTree(left);
            root->right = createTree(n - 1 - left);

            return root;
        }
    }

    static const ListNode *fillTree(TreeNode *tree, const ListNode *list)
    {
        if (tree != nullptr)
        {
            list = fillTree(tree->left, list);
            tree->val = list->val;
            list = fillTree(tree->right, list->next);
        }

        return list;
    }

public:
    TreeNode *sortedListToBST(ListNode *head)
    {
        TreeNode *root = createTree(getListLength(head));

        fillTree(root, head);

        return root;
    }
};
