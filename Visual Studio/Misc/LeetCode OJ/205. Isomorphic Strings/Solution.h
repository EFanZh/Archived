#pragma once

class Solution
{
public:
    bool isIsomorphic(string s, string t)
    {
        // Assume s and t doesn't contain '\0';
        array<char, 256> m;
        array<bool, 256> tSeen;

        fill(m.begin(), m.end(), '\0');
        fill(tSeen.begin(), tSeen.end(), false);

        for (size_t i = 0; i < s.length(); ++i)
        {
            char c1 = s[i];
            char c2 = t[i];
            char k = m[c1];

            if (k == '\0')
            {
                if (tSeen[c2])
                {
                    return false;
                }
                else
                {
                    m[c1] = c2;
                    tSeen[c2] = true;
                }
            }
            else
            {
                if (k != c2)
                {
                    return false;
                }
            }
        }

        return true;
    }
};
