#pragma once

class Solution
{
    static bool isPalindrome(string::const_iterator first, string::const_iterator last)
    {
        if (last - first < 2)
        {
            return true;
        }
        else
        {
            --last;

            while (first < last)
            {
                if (*first != *last)
                {
                    return false;
                }
                else
                {
                    ++first;
                    --last;
                }
            }

            return true;
        }
    }

public:
    vector<vector<int>> palindromePairs(const vector<string> &words)
    {
        const auto wordsCount = words.size();
        auto indexes = unordered_map<string, string::size_type>();

        for (auto i = decltype(wordsCount)(0); i < wordsCount; ++i)
        {
            indexes.emplace(words[i], i);
        }

        auto reversedWord = string();
        auto tempWord = string();
        auto emptyIt = indexes.find(tempWord);
        auto result = vector<vector<int>>();

        for (auto i = decltype(wordsCount)(0); i < wordsCount; ++i)
        {
            const auto &word = words[i];
            const auto wordLength = word.length();

            reversedWord.assign(word.crbegin(), word.crend());

            for (auto j = decltype(wordLength)(1); j < wordLength; ++j)
            {
                if (isPalindrome(word.cbegin(), word.cbegin() + j))
                {
                    tempWord.assign(reversedWord.cbegin(), reversedWord.cend() - j);

                    auto it = indexes.find(tempWord);

                    if (it != indexes.cend())
                    {
                        result.push_back({ static_cast<int>(it->second), static_cast<int>(i) });
                    }
                }

                if (isPalindrome(word.cbegin() + j, word.cend()))
                {
                    tempWord.assign(reversedWord.cend() - j, reversedWord.cend());

                    auto it = indexes.find(tempWord);

                    if (it != indexes.cend())
                    {
                        result.push_back({ static_cast<int>(i), static_cast<int>(it->second) });
                    }
                }
            }

            auto it = indexes.find(reversedWord);

            if (it != indexes.cend())
            {
                if (it->second == i)
                {
                    if (!word.empty() && emptyIt != indexes.cend())
                    {
                        result.push_back({ static_cast<int>(i), static_cast<int>(emptyIt->second) });
                        result.push_back({ static_cast<int>(emptyIt->second), static_cast<int>(i) });
                    }
                }
                else
                {
                    result.push_back({ static_cast<int>(i), static_cast<int>(it->second) });
                }
            }
        }

        return result;
    }
};
