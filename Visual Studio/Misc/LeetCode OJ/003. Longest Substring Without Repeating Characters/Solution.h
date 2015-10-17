#pragma once

class Solution
{
public:
    int lengthOfLongestSubstring(const string &s)
    {
        if (s.length() <= 1)
        {
            return static_cast<int>(s.length());
        }

        const int length = static_cast<int>(s.length());
        array<int, 256> window;
        int windowBegin = 0;
        int result = 0;

        fill(window.begin(), window.end(), -1);

        for (int i = 0; i < length; ++i)
        {
            auto &current = window[s[i]];

            if (current < windowBegin) // If current == -1 or current is not in the window.
            {
                result = max(result, i - windowBegin + 1);
            }
            else
            {
                windowBegin = current + 1;
            }

            current = i;
        }

        return result;
    }
};
