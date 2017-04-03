#pragma once

class Solution
{
public:
    int lengthOfLongestSubstring(const string &s)
    {
        using SizeType = decltype(s.length());

        if (s.length() <= 1)
        {
            return static_cast<int>(s.length());
        }

        const auto length = s.length();
        auto window = array<SizeType, 256>();
        auto windowBegin = SizeType(0);
        auto result = SizeType(0);

        fill(window.begin(), window.end(), -1);

        for (auto i = SizeType(0); i < length; ++i)
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

        return static_cast<int>(result);
    }
};
