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
class BSTIterator
{
    stack<const TreeNode *> s;

public:
    BSTIterator(TreeNode *root)
    {
        for (; root != nullptr; root = root->left)
        {
            s.emplace(root);
        }
    }

    /** @return whether we have a next smallest number */
    bool hasNext()
    {
        return !s.empty();
    }

    /** @return the next smallest number */
    int next()
    {
        auto current = s.top();

        s.pop();
        for (auto node = current->right; node != nullptr; node = node->left)
        {
            s.emplace(node);
        }

        return current->val;
    }
};

/**
 * Your BSTIterator will be called like this:
 * BSTIterator i = BSTIterator(root);
 * while (i.hasNext()) cout << i.next();
 */
