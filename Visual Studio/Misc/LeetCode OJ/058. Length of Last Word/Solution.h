#pragma once

class Solution
{
public:
    int lengthOfLastWord(string s)
    {
        size_t i = s.length() - 1;

        while (i < s.length())
        {
            if (isspace(s[i]))
            {
                --i;
            }
            else
            {
                --i;

                int result = 1;

                while (i < s.length() && !isspace(s[i]))
                {
                    ++result;
                    --i;
                }

                return result;
            }
        }

        return 0;
    }
};
