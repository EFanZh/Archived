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
class Codec
{
    int parseInteger(const char *&p)
    {
        return static_cast<int>(strtol(p, const_cast<char **>(&p), 10));
    }

public:
    // Encodes a tree to a single string.
    string serialize(const TreeNode *root)
    {
        string result = "[";
        queue<const TreeNode *> q;

        if (root != nullptr)
        {
            result += to_string(root->val);

            q.emplace(root->left);
            q.emplace(root->right);
        }

        while (!q.empty())
        {
            const TreeNode *current = q.front();

            q.pop();

            if (current == nullptr)
            {
                result += ",null";
            }
            else
            {
                result += ',';
                result += to_string(current->val);
                q.emplace(current->left);
                q.emplace(current->right);
            }
        }

        result += ']';

        return result;
    }

    // Decodes your encoded data to tree.
    TreeNode *deserialize(const string &data)
    {
        if (data.length() == 2)
        {
            return nullptr;
        }

        const char *p = data.data() + 1, *last = data.data() + (data.length() - 1);
        TreeNode *root = new TreeNode(parseInteger(p));

        if (p < last)
        {
            ++p;

            queue<TreeNode **> q;

            q.emplace(&root->left);
            q.emplace(&root->right);

            while (p < last)
            {
                TreeNode **current = q.front();

                q.pop();

                if (isdigit(*p) || *p == '-')
                {
                    *current = new TreeNode(parseInteger(p));
                    q.emplace(&(*current)->left);
                    q.emplace(&(*current)->right);

                    ++p;
                }
                else
                {
                    p += 5;
                }
            }
        }

        return root;
    }
};

// Your Codec object will be instantiated and called as such:
// Codec codec;
// codec.deserialize(codec.serialize(root));
