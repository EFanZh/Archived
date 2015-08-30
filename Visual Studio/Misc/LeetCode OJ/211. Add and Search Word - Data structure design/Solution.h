#pragma once

class WordDictionary
{
    struct Node
    {
        bool hasValue;
        map<char, Node> children;
    };

    Node root;

    template <class T>
    static void addHelper(Node &node, T first, T last)
    {
        if (first == last)
        {
            node.hasValue = true;
        }
        else
        {
            addHelper(node.children[*first], first + 1, last);
        }
    }

    template <class T>
    static bool searchHelper(const Node &node, T first, T last)
    {
        if (first == last)
        {
            return node.hasValue;
        }
        else
        {
            if (*first == '.')
            {
                for (const auto &p : node.children)
                {
                    if (searchHelper(p.second, first + 1, last))
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                auto it = node.children.find(*first);

                if (it == node.children.cend())
                {
                    return false;
                }
                else
                {
                    return searchHelper(it->second, first + 1, last);
                }
            }
        }
    }

public:
    // Adds a word into the data structure.
    void addWord(const string &word)
    {
        addHelper(root, word.cbegin(), word.cend());
    }

    // Returns if the word is in the data structure. A word could
    // contain the dot character '.' to represent any one letter.
    bool search(const string &word) const
    {
        return searchHelper(root, word.cbegin(), word.cend());
    }
};

// Your WordDictionary object will be instantiated and called as such:
// WordDictionary wordDictionary;
// wordDictionary.addWord("word");
// wordDictionary.search("pattern");
