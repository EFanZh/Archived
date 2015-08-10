#pragma once

class Solution
{
    class Node
    {
        template <class T>
        void removeHelper(T first, T last)
        {
            if (first == last)
            {
                value = nullptr;
            }
            else
            {
                Node &next = children.at(*first);

                next.removeHelper(first + 1, last);

                if (next.value == nullptr && next.children.empty())
                {
                    children.erase(*first);
                }
            }
        }

    public:
        const string *value = nullptr;
        map<char, Node> children;

        void add(const string &s)
        {
            Node *current = this;

            for (auto c : s)
            {
                current = &current->children[c];
            }

            current->value = &s;
        }

        void remove(const string &s)
        {
            removeHelper(s.cbegin(), s.cend());
        }
    };

public:
    vector<string> findWords(const vector<vector<char>> &board, const vector<string> &words)
    {
        if (board.size() == 0 || board.front().size() == 0)
        {
            if (any_of(words.cbegin(), words.cend(), [](const string &s) { return s.empty(); }))
            {
                return{ "" };
            }
            else
            {
                return{};
            }
        }

        vector<string> result;
        Node trie;
        const size_t rows = board.size();
        const size_t columns = board.front().size();
        vector<char> visited(columns * rows);

        for (const auto &word : words)
        {
            if (word.length() <= columns * rows)
            {
                trie.add(word);
            }
        }

        function<void(size_t, size_t, Node &)> dfs = [&](size_t i, size_t j, Node &node)
        {
            if (node.value != nullptr)
            {
                result.emplace_back(*node.value);
                trie.remove(*node.value);
            }

            visited[columns * i + j] = 'G';

            for (const auto &offset : { make_pair(-1, 0),
                make_pair(0, -1),
                make_pair(1, 0),
                make_pair(0, 1) })
            {
                size_t nextRow = i + offset.first;
                size_t nextColumn = j + offset.second;

                if (nextRow < rows &&
                    nextColumn < columns &&
                    visited[columns * nextRow + nextColumn] == '\0')
                {
                    auto it = node.children.find(board[nextRow][nextColumn]);

                    if (it != node.children.cend())
                    {
                        dfs(nextRow, nextColumn, it->second);
                    }
                }
            }

            visited[columns * i + j] = '\0';
        };

        for (size_t i = 0; i < rows; ++i)
        {
            for (size_t j = 0; j < columns; ++j)
            {
                auto it = trie.children.find(board[i][j]);

                if (it != trie.children.cend())
                {
                    dfs(i, j, it->second);
                }
            }
        }

        return result;
    }
};
