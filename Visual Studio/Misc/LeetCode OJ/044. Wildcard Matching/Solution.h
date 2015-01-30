#pragma once

class Solution
{
public:
    bool isMatch(const char *s, const char *p)
    {
        const char *retryString;
        const char *retryPattern = nullptr;

        while (*s != '\0')
        {
            if (*p == '*')
            {
                retryString = s + 1;
                retryPattern = ++p;
            }
            else if (*p == '?' || *s == *p)
            {
                ++s;
                ++p;
            }
            else
            {
                if (retryPattern != nullptr)
                {
                    s = retryString++;
                    p = retryPattern;
                }
                else
                {
                    return false;
                }
            }
        }

        while (*p == '*')
        {
            ++p;
        }

        return *p == '\0';
    }
};
