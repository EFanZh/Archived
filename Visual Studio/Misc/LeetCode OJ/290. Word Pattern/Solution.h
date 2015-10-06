#pragma once

class Solution
{
public:
    bool wordPattern(const string &pattern, const string &str)
    {
        if (pattern.empty())
        {
            return str.empty();
        }
        else if (str.empty())
        {
            return false;
        }

        array<string, 26> mapping;
        set<string> usedWord;
        size_t j = 0;

        for (size_t i = 0; i < pattern.length(); ++i)
        {
            if (j == str.length())
            {
                return false;
            }
            else if (str[j] == ' ')
            {
                ++j;
            }

            auto &currentMapping = mapping[pattern[i] - 'a'];

            if (currentMapping.empty())
            {
                string newWord;

                for (; j < str.length() && str[j] != ' '; ++j)
                {
                    newWord += str[j];
                }

                if (usedWord.count(newWord) > 0)
                {
                    return false;
                }
                else
                {
                    usedWord.emplace(newWord);
                    currentMapping = move(newWord);
                }
            }
            else
            {
                if (str.length() - j < currentMapping.length())
                {
                    return false;
                }

                for (char k : currentMapping)
                {
                    if (k != str[j])
                    {
                        return false;
                    }
                    ++j;
                }
            }
        }

        return j == str.length();
    }
};
