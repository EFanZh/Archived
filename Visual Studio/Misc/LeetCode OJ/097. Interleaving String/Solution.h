#pragma once

class Solution
{
public:
    bool isInterleave(string s1, string s2, string s3)
    {
        if (s1.length() + s2.length() != s3.length())
        {
            return false;
        }

        if (s1.length() > s2.length())
        {
            swap(s1, s2);
        }

        if (s1.length() == 0)
        {
            return s2 == s3;
        }

        vector<char> cache(s1.length() + 1, false);

        cache.back() = true;

        auto updateCache = [&](size_t first, size_t last, size_t k)
        {
            for (size_t i = first; i <= last; ++i)
            {
                bool iOK = s1[i] == s3[k] && i < s1.length() && cache[i + 1];
                bool jOK = s2[k - i] == s3[k] && k - i < s2.length() && cache[i];

                cache[i] = iOK || jOK;
            }
        };

        size_t k = s3.length() - 1;

        for (; k >= s2.length(); --k)
        {
            updateCache(k - s2.length(), s1.length(), k);
        }

        for (; k >= s1.length(); --k)
        {
            updateCache(0, s1.length(), k);
        }

        for (; k < s1.length(); --k)
        {
            updateCache(0, k, k);
        }

        return cache.front();
    }
};
