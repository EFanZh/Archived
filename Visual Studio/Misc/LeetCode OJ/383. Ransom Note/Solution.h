#pragma once

class Solution
{
public:
    bool canConstruct(const string &ransomNote, const string &magazine)
    {
        auto stockLetters = array<size_t, 26>();

        for (auto c : magazine)
        {
            ++stockLetters[c - 'a'];
        }

        for (auto c : ransomNote)
        {
            if (stockLetters[c - 'a'] == 0)
            {
                return false;
            }
            else
            {
                --stockLetters[c - 'a'];
            }
        }

        return true;
    }
};
