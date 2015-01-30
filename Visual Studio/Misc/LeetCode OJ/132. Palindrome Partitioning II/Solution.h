#pragma once

class Solution
{
public:
    int minCut(string s)
    {
        vector<int> cache(s.length() + 1);

        for (size_t i = 0; i < cache.size(); ++i)
        {
            cache[i] = static_cast<int>(i) - 1;
        }

        int *cache2 = cache.data() + 1;

        for (size_t i = 0; i < s.length(); ++i)
        {
            for (size_t j = 0; i - j <= i && i + j + 1 < s.length() && s[i - j] == s[i + j + 1]; ++j)
            {
                cache2[i + j + 1] = min(cache2[i + j + 1], cache2[i - j - 1] + 1);
            }
            for (size_t j = 0; i - j <= i && i + j < s.length() && s[i - j] == s[i + j]; ++j)
            {
                cache2[i + j] = min(cache2[i + j], cache2[i - j - 1] + 1);
            }
        }

        return cache.back();
    }
};
