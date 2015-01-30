#pragma once

class Solution
{
    static size_t getMaxLength(const string &s, size_t i, size_t j)
    {
        while (i < s.length() && j < s.length() && s[i] == s[j])
        {
            --i;
            ++j;
        }

        return j - i - 1;
    }

public:
    string longestPalindrome(string s)
    {
        size_t maxFrom = 0;
        size_t maxLength = 0;
        size_t middle = s.length() / 2;

        if (s.length() % 2 != 0)
        {
            maxLength = getMaxLength(s, middle - 1, middle + 1);
            maxFrom = middle - maxLength / 2;
        }

        for (size_t i = middle - 1; (i + 1) * 2 > maxLength; --i)
        {
            size_t length = getMaxLength(s, i - 1, i + 1);

            if (length > maxLength)
            {
                maxLength = length;
                maxFrom = i - length / 2;
            }

            length = getMaxLength(s, i, i + 1);

            if (length > maxLength)
            {
                maxLength = length;
                maxFrom = i - length / 2 + 1;
            }

            length = getMaxLength(s, s.length() - i - 2, s.length() - i);

            if (length > maxLength)
            {
                maxLength = length;
                maxFrom = s.length() - i - length / 2 - 1;
            }

            length = getMaxLength(s, s.length() - i - 2, s.length() - i - 1);

            if (length > maxLength)
            {
                maxLength = length;
                maxFrom = s.length() - i - length / 2 - 1;
            }
        }

        return s.substr(maxFrom, maxLength);
    }
};
