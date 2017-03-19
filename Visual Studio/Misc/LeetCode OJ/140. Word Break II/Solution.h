#pragma once

class Solution
{
public:
    vector<string> wordBreak(string s, unordered_set<string> &dict)
    {
        if (s.length() == 0)
        {
            return { s };
        }
        else if (dict.empty())
        {
            return {};
        }
        else
        {
            const auto &wordLengths =
                minmax_element(dict.cbegin(), dict.cend(), [](const string &lhs, const string &rhs) {
                    return lhs.size() < rhs.size();
                });

            size_t minWordLength = wordLengths.first->length();
            size_t maxWordLength = wordLengths.second->length();

            deque<vector<vector<size_t>>> cache;

            cache.resize(min<size_t>(s.length(), maxWordLength));

            for (size_t i = s.size() - 1; i < s.size(); --i)
            {
                vector<vector<size_t>> newBreaks;

                size_t maxAvailableLength = min(maxWordLength, s.size() - i);

                // First j letters as word.
                for (size_t j = minWordLength; j <= maxAvailableLength; ++j)
                {
                    if (dict.count(s.substr(i, j)))
                    {
                        if (i + j < s.length())
                        {
                            for (const auto &restResult : cache[j - 1])
                            {
                                vector<size_t> split = { i };

                                split.insert(split.end(), restResult.cbegin(), restResult.cend());
                                newBreaks.emplace_back(move(split));
                            }
                        }
                        else
                        {
                            newBreaks.push_back({ i });
                        }
                    }
                }

                cache.pop_back();
                cache.emplace_front(move(newBreaks));
            }

            vector<string> result;

            for (auto &item : cache[0])
            {
                string breakedString;

                for (size_t i = 0; i != item.size() - 1; ++i)
                {
                    breakedString.insert(breakedString.end(), s.cbegin() + item[i], s.cbegin() + item[i + 1]);
                    breakedString += ' ';
                }

                breakedString.insert(breakedString.end(), s.cbegin() + item.back(), s.cend());
                result.emplace_back(move(breakedString));
            }

            return result;
        }
    }
};
