#pragma once

class Solution
{
public:
    vector<int> findSubstring(string S, vector<string> &L)
    {
        vector<int> result;
        size_t wordLength = L.front().length();
        size_t totalWordLength = wordLength * L.size();

        if (S.length() >= totalWordLength)
        {
            unordered_map<string, size_t> dict;

            for (auto &word : L)
            {
                ++dict[word];
            }

            for (size_t i = 0; i < wordLength; ++i)
            {
                unordered_map<string, size_t> currentDict;

                for (size_t j = i; j < i + totalWordLength; j += wordLength)
                {
                    ++currentDict[S.substr(j, wordLength)];
                }

                if (currentDict == dict)
                {
                    result.emplace_back(i);
                }

                for (size_t j = i + wordLength; j <= S.length() - totalWordLength; j += wordLength)
                {
                    string oldWord = S.substr(j - wordLength, wordLength);

                    if (--currentDict[oldWord] == 0)
                    {
                        currentDict.erase(oldWord);
                    }

                    ++currentDict[S.substr(j + totalWordLength - wordLength, wordLength)];

                    if (currentDict == dict)
                    {
                        result.emplace_back(j);
                    }
                }
            }
        }

        return result;
    }
};
