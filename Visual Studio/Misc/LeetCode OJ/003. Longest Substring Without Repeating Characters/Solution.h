#pragma once

class Solution
{
public:
    int lengthOfLongestSubstring(string s)
    {
        if (s.empty())
        {
            return 0;
        }

        array<int, 256> current;

        fill(current.begin(), current.end(), -1);

        size_t i = 0;
        size_t maxLength = 0;

        for (size_t j = 0; j < s.length(); ++j)
        {
            if (current[s[j]] != -1)
            {
                maxLength = max(maxLength, j - i);

                size_t newBegin = current[s[j]];

                for (; i < newBegin; ++i)
                {
                    current[s[i]] = -1;
                }

                ++i;
            }

            current[s[j]] = j;
        }

        return max(maxLength, s.length() - i);
    }
};
