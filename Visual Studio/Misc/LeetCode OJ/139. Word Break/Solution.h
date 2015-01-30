#pragma once

class Solution
{
public:
    bool wordBreak(string s, unordered_set<string> &dict)
    {
        if (s.empty())
        {
            return true;
        }

        if (dict.empty())
        {
            return false;
        }

        const auto &wordLengths = minmax_element(dict.cbegin(), dict.cend(), [](const string &lhs, const string &rhs)
                                                                             {
                                                                                 return lhs.size() < rhs.size();
                                                                             });

        size_t minWordLength = wordLengths.first->length();
        size_t maxWordLength = wordLengths.second->length();
        vector<uint8_t> cache(s.length() + 1, false);

        cache.back() = true;
        for (size_t i = s.length() - 1; i < s.length(); --i)
        {
            for (size_t j = minWordLength; j <= maxWordLength && i + j <= s.length(); ++j)
            {
                if (cache[i + j] && dict.count(s.substr(i, j)) > 0)
                {
                    cache[i] = true;

                    break;
                }
            }
        }

        return cache.front();
    }
};
