#pragma once

class Solution
{
public:
    string countAndSay(int n)
    {
        string s = "1";

        for (; n > 1; --n)
        {
            string t;

            for (size_t i = 0; i < s.length();)
            {
                size_t j = i + 1;

                for (; j < s.length() && s[j] == s[i]; ++j)
                {
                    continue;
                }
                t += static_cast<char>('0' + (j - i));
                t += s[i];

                i = j;
            }

            s = move(t);
        }

        return s;
    }
};
