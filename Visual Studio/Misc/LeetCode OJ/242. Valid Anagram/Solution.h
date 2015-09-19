#pragma once

class Solution
{
public:
    bool isAnagram(string s, string t)
    {
        if (s.length() != t.length())
        {
            return false;
        }

        array<size_t, 26> count = { 0 };

        for (size_t i = 0; i < s.length(); ++i)
        {
            ++count[s[i] - 'a'];
            --count[t[i] - 'a'];
        }

        for (size_t n : count)
        {
            if (n != 0)
            {
                return false;
            }
        }

        return true;
    }
};
