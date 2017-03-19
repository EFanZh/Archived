#pragma once

class Solution
{
public:
    int minDistance(string word1, string word2)
    {
        if (word1.length() > word2.length())
        {
            word1.swap(word2);
        }

        // +---+---+---+---+---+
        // | 0 | 1 | 2 | 3 | 4 |
        // +---+---+---+---+---+
        //   1   2   3   4 | 5 |
        //                 +---+
        //   2   3   4   5 | 6 |
        //                 +---+
        //   3   4   5   6 | 7 |
        //                 +---+
        //   4   5   6   7 | 8 |
        //                 +---+
        //   5   6   7   8 | 9 |
        //                 +---+
        vector<int> cache;

        cache.resize(word1.length() + word2.length() + 1);
        for (size_t i = 0; i <= word1.length(); ++i)
        {
            cache[i] = word1.length() - i;
        }
        for (size_t i = word1.length() + 1; i <= word1.length() + word2.length(); ++i)
        {
            cache[i] = i - word1.length();
        }

        auto getIndex = [&](int i, int j) { return i - j + word2.length(); };

        auto solve = [&](int i, int j) {
            if (word1[i] != word2[j])
            {
                cache[getIndex(i, j)] =
                    min({ cache[getIndex(i, j + 1)], cache[getIndex(i + 1, j + 1)], cache[getIndex(i + 1, j)] }) + 1;
            }
        };

        for (size_t level = word1.length() - 1; level < word1.length(); --level)
        {
            size_t k = level + (word2.length() - word1.length());

            solve(level, k);
            for (size_t i = level - 1; i < level; --i)
            {
                solve(i, k);
            }
            for (size_t j = k - 1; j < k; --j)
            {
                solve(level, j);
            }
        }

        return cache[getIndex(0, 0)];
    }
};
