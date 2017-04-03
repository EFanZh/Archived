#pragma once

class Solution
{
public:
    bool isPalindrome(const string &s)
    {
        if (s.length() <= 1)
        {
            return true;
        }

        int i = 0;
        int j = static_cast<int>(s.length() - 1);

        while (i < j)
        {
            while (i < j && !isalnum(s[i]))
            {
                ++i;
            }
            while (i < j && !isalnum(s[j]))
            {
                --j;
            }

            if (toupper(s[i]) != toupper(s[j]))
            {
                return false;
            }
            ++i;
            --j;
        }

        return true;
    }
};
