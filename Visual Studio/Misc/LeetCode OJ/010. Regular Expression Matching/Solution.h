#pragma once

class Solution
{
public:
    bool isMatch(const char *s, const char *p)
    {
        if (*p == '\0')
        {
            return *s == '\0';
        }
        else if (p[1] == '*')
        {
            return isMatch(s, p + 2) || (((*p == '.' && *s != '\0') || *p == *s) && isMatch(s + 1, p));
        }
        else
        {
            return ((*p == '.' && *s != '\0') || *p == *s) && isMatch(s + 1, p + 1);
        }
    }
};
