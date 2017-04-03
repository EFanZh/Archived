#pragma once

class Solution
{
    static vector<const char *> buildKmpJumpTable(const char *pattern)
    {
        if (*pattern == '\0')
        {
            return { pattern };
        }
        else if (pattern[1] == '\0')
        {
            return { pattern, pattern };
        }

        vector<const char *> result = { pattern, pattern };

        for (const char *p = pattern + 2; *p != '\0'; ++p)
        {
            const char *k = result.back();

            for (;;)
            {
                if (*k == p[-1])
                {
                    result.emplace_back(k + 1);
                    break;
                }
                else
                {
                    if (k == pattern)
                    {
                        result.emplace_back(pattern);
                        break;
                    }
                    else
                    {
                        k = result[k - pattern];
                    }
                }
            }
        }

        return result;
    }

public:
    int strStr(char *haystack, char *needle)
    {
        if (*needle == '\0')
        {
            return 0;
        }

        const char *p = needle;
        vector<const char *> jump = buildKmpJumpTable(needle);

        for (const char *s = haystack; *s != '\0';)
        {
            if (*s == *p)
            {
                ++p;
                if (*p == '\0')
                {
                    return static_cast<int>((s - haystack) - (p - needle) + 1);
                }
                ++s;
            }
            else
            {
                if (p != needle)
                {
                    p = jump[p - needle];
                }
                else
                {
                    ++s;
                }
            }
        }

        return -1;
    }
};
