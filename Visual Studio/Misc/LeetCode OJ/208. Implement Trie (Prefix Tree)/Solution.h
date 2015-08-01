#pragma once

class TrieNode
{
    bool hasValue = false;
    unordered_map<char, TrieNode> children;

    friend class Trie;

    // Initialize your data structure here.
};

class Trie
{
    TrieNode root;

public:
    // Inserts a word into the trie.
    void insert(const string &word)
    {
        TrieNode *current = &root;

        for (size_t i = 0; i < word.length(); ++i)
        {
            current = &(current->children[word[i]]);
        }

        current->hasValue = true;
    }

    // Returns if the word is in the trie.
    bool search(const string &word)
    {
        TrieNode *current = &root;

        for (size_t i = 0; i < word.length(); ++i)
        {
            auto it = current->children.find(word[i]);

            if (it == current->children.end())
            {
                return false;
            }
            else
            {
                current = &it->second;
            }
        }

        return current->hasValue;
    }

    // Returns if there is any word in the trie
    // that starts with the given prefix.
    bool startsWith(const string &prefix)
    {
        TrieNode *current = &root;

        for (size_t i = 0; i < prefix.length(); ++i)
        {
            auto it = current->children.find(prefix[i]);

            if (it == current->children.end())
            {
                return false;
            }
            else
            {
                current = &it->second;
            }
        }

        return true;
    }
};

// Your Trie object will be instantiated and called as such:
// Trie trie;
// trie.insert("somestring");
// trie.search("key");
