#pragma once

class Solution
{
    static int parseInt(const char *&s)
    {
        int n = 0;

        for (; isdigit(*s); ++s)
        {
            n = n * 10 + (*s - '0');
        }

        return n;
    }

    static int compareVersionHelper(const char *v1, const char *v2)
    {
        int m = parseInt(v1), n = parseInt(v2);

        if (m < n)
        {
            return -1;
        }
        else if (m == n)
        {
            if (*v1 == '.')
            {
                return compareVersionHelper(v1 + 1, *v2 == '.' ? v2 + 1 : "0");
            }
            else
            {
                return *v2 == '.' ? compareVersionHelper("0", v2 + 1) : 0;
            }
        }
        else
        {
            return 1;
        }
    }

public:
    int compareVersion(string version1, string version2)
    {
        return compareVersionHelper(version1.data(), version2.data());
    }
};
